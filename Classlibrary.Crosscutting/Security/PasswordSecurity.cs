﻿#region Copyright Neutron © 2019

//
// NAME:			PasswordSecurity.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Security
//

#endregion

using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace Classlibrary.Crosscutting.Security
{
    /// <summary>
    ///     Exceptions
    /// </summary>
    internal class InvalidHashException : Exception
    {
        public InvalidHashException()
        {
        }

        public InvalidHashException(string message)
            : base(message)
        {
        }

        public InvalidHashException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Exceptions
    /// </summary>
    internal class CannotPerformOperationException : Exception
    {
        public CannotPerformOperationException()
        {
        }

        public CannotPerformOperationException(string message)
            : base(message)
        {
        }

        public CannotPerformOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     The Password Storage class
    /// </summary>
    public class PasswordStorage<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        // These constants may be changed without breaking existing hashes.
        public const int SALT_BYTES = 32;
        public const int HASH_BYTES = 24;
        public const int PBKDF2_ITERATIONS = 72000;

        // These constants define the encoding and may not be changed.
        public const int HASH_SECTIONS = 5;
        public const int HASH_ALGORITHM_INDEX = 0;
        public const int ITERATION_INDEX = 1;
        public const int HASH_SIZE_INDEX = 2;
        public const int SALT_INDEX = 3;
        public const int PBKDF2_INDEX = 4;

        /// <summary>
        ///     Create the hash given a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string CreateHash(string password)
        {
            // Generate a random salt
            var salt = new byte[SALT_BYTES];
            try
            {
                using (var csprng = new RNGCryptoServiceProvider())
                {
                    csprng.GetBytes(salt);
                }
            }
            catch (CryptographicException ex)
            {
                throw new CannotPerformOperationException(
                    "Random number generator not available.",
                    ex
                );
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                    "Invalid argument given to random number generator.",
                    ex
                );
            }

            var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

            // format: algorithm:iterations:hashSize:salt:hash
            var parts = "sha1:" +
                        PBKDF2_ITERATIONS +
                        ":" +
                        hash.Length +
                        ":" +
                        Convert.ToBase64String(salt) +
                        ":" +
                        Convert.ToBase64String(hash);
            return parts;
        }

        /// <summary>
        ///     Verify password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="goodHash"></param>
        /// <returns></returns>
        private static bool VerifyPassword(string password, string goodHash)
        {
            char[] delimiter = {':'};
            var split = goodHash.Split(delimiter);

            if (split.Length != HASH_SECTIONS)
                throw new InvalidHashException(
                    "Fields are missing from the password hash."
                );

            // We only support SHA1 with C#.
            if (split[HASH_ALGORITHM_INDEX] != "sha1")
                throw new CannotPerformOperationException(
                    "Unsupported hash type."
                );

            var iterations = 0;
            try
            {
                iterations = int.Parse(split[ITERATION_INDEX]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                    "Invalid argument given to Int32.Parse",
                    ex
                );
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException(
                    "Could not parse the iteration count as an integer.",
                    ex
                );
            }
            catch (OverflowException ex)
            {
                throw new InvalidHashException(
                    "The iteration count is too large to be represented.",
                    ex
                );
            }

            if (iterations < 1)
                throw new InvalidHashException(
                    "Invalid number of iterations. Must be >= 1."
                );

            byte[] salt = null;
            try
            {
                salt = Convert.FromBase64String(split[SALT_INDEX]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                    "Invalid argument given to Convert.FromBase64String",
                    ex
                );
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException(
                    "Base64 decoding of salt failed.",
                    ex
                );
            }

            byte[] hash = null;
            try
            {
                hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                    "Invalid argument given to Convert.FromBase64String",
                    ex
                );
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException(
                    "Base64 decoding of pbkdf2 output failed.",
                    ex
                );
            }

            var storedHashSize = 0;
            try
            {
                storedHashSize = int.Parse(split[HASH_SIZE_INDEX]);
            }
            catch (ArgumentNullException ex)
            {
                throw new CannotPerformOperationException(
                    "Invalid argument given to Int32.Parse",
                    ex
                );
            }
            catch (FormatException ex)
            {
                throw new InvalidHashException(
                    "Could not parse the hash size as an integer.",
                    ex
                );
            }
            catch (OverflowException ex)
            {
                throw new InvalidHashException(
                    "The hash size is too large to be represented.",
                    ex
                );
            }

            if (storedHashSize != hash.Length)
                throw new InvalidHashException(
                    "Hash length doesn't match stored hash length."
                );

            var testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        ///     Comparitor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint) a.Length ^ (uint) b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++) diff |= (uint) (a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        ///     PBKDF2 implementation
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="outputBytes"></param>
        /// <returns></returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        #region IPasswordHasher 

        /// <summary>
        ///     Hash password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Invalid password", password);
            return CreateHash(password);
        }

        /// <summary>
        ///     Verify password
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("Invalid hashed password", hashedPassword);
            if (string.IsNullOrEmpty(providedPassword))
                throw new ArgumentException("Invalid provided password", providedPassword);
            return VerifyPassword(providedPassword, hashedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }

        #endregion

        #region Implementation of IPasswordHasher<TUser>

        /// <summary>
        ///     Hash password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string HashPassword(TUser user, string password)
        {
            if (user == null)
                throw new ArgumentException("Invalid user");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Invalid password", password);
            return CreateHash(password);
        }

        /// <summary>
        ///     Verify hashed password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="providedPassword">The provided password.</param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword,
            string providedPassword)
        {
            if (user == null)
                throw new ArgumentException("Invalid user");
            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("Invalid hashed password", hashedPassword);
            if (string.IsNullOrEmpty(providedPassword))
                throw new ArgumentException("Invalid provided password", providedPassword);
            return VerifyPassword(providedPassword, hashedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }

        #endregion
    }
}