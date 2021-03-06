﻿#region Copyright TechNeutron © 2019

//
// NAME:			AdministrationManager.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Administration manager interface
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Classlibrary.Dao.Linq2Db;
using LinqToDB;

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="AdministrationManager" /> class.
    /// </summary>
    public class AdministrationManager : IAdministrationManager
    {
        #region Implementation of IAdministrationManager

        /// <summary>
        ///     Get user.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="User" />.</returns>
        public async Task<User> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid id", nameof(id));
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.User>().Where(x => x.Id == id)
                    .LeftJoin(db.GetTable<AdministrationSchema.UserProfile>(),
                        (user, userProfile) => user.Id == userProfile.UserId, (user, userProfile) => new { user, userProfile })
                    .GroupJoin(db.GetTable<AdministrationSchema.UserClaim>(), user => user.user.Id, userClaim => userClaim.UserId, (parent, claims) => Build(parent.user, parent.userProfile, claims.ToList()))
                    .ToListAsync();
                return items.FirstOrDefault();
            }
        }

        /// <summary>
        ///     Get user by user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        public async Task<User> Get(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Invalid user name", nameof(userName));
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.User>().Where(x => x.UserName == userName)
                    .LeftJoin(db.GetTable<AdministrationSchema.UserProfile>(),
                        (user, userProfile) => user.Id == userProfile.UserId, (user, userProfile) => new { user, userProfile })
                    .GroupJoin(db.GetTable<AdministrationSchema.UserClaim>(), user => user.user.Id, userClaim => userClaim.UserId, (parent, claims) => Build(parent.user, parent.userProfile, claims.ToList()))
                    .ToListAsync();
                return items.FirstOrDefault();
            }
        }

        /// <summary>
        ///     All users.
        /// </summary>
        /// <returns><see cref="IEnumerable{User}" />.</returns>
        public async Task<IEnumerable<User>> All()
        {
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.User>().Take(1000)
                    .LeftJoin(db.GetTable<AdministrationSchema.UserProfile>(),
                        (user, userProfile) => user.Id == userProfile.UserId, (user, userProfile) => new { user, userProfile })
                    .GroupJoin(db.GetTable<AdministrationSchema.UserClaim>(), user => user.user.Id, userClaim => userClaim.UserId, (parent, claims) => Build(parent.user, parent.userProfile, claims.ToList()))
                    .ToListAsync();
                return items;
            }
        }

        /// <summary>
        ///     Create a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<Guid> Create(User user, DependentTransaction transaction = null)
        {
            if(user.Id != Guid.Empty)
                throw new InvalidOperationException("Id has to be empty guid");

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var id = Guid.NewGuid();
                    var result = await db.Administration.Users
                        .Value(c => c.Id, id)
                        .Value(c => c.UserName, user.UserName)
                        .Value(c => c.Email, user.Email)
                        .Value(c => c.PasswordHash, user.PasswordHash)
                        .Value(c => c.SecurityStamp, Guid.NewGuid().ToString())
                        .Value(c => c.PhoneNumber, user.PhoneNumber)
                        .Value(c => c.MobileNumber, user.MobileNumber)
                        .Value(c => c.NationalId, user.NationalId)
                        .Value(c => c.EmailConfirmed, true)
                        .Value(c => c.PhoneNumberConfirmed, true)
                        .Value(c => c.MobileNumberConfirmed, true)
                        .Value(c => c.LockoutEnabled, false)
                        .Value(c => c.AccessFailedCount, 0)
                        .Value(c => c.TwoFactorEnabled, false)
                        .Value(c => c.CreatedOn, DateTime.UtcNow)
                        .InsertAsync();

                    // Create user profile
                    if(user.Profile != null)
                        await Create(id, user.Profile, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                    // Create user claim
                    if(user.Claims != null && user.Claims.Any())
                        await Create(id, user.Claims, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1 ? id : Guid.Empty;
                }
            }
        }

        /// <summary>
        ///     Update a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">Thr transaction.</param>
        /// <returns></returns>
        public async Task<bool> Update(User user, DependentTransaction transaction = null)
        {
            if(user.Id == Guid.Empty)
                throw new InvalidOperationException("Invalid user Id");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.Users.Where(x => x.Id == user.Id)
                        .Set(u => u.Email, user.Email)
                        .Set(u => u.PhoneNumber, user.PhoneNumber)
                        .Set(u => u.MobileNumber, user.MobileNumber)
                        .Set(u => u.ChangedOn, DateTime.UtcNow)
                        .UpdateAsync();

                    // Update user profile
                    if (user.Profile != null && user.Profile.UserId != Guid.Empty)
                        await Update(user.Profile, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        /// <summary>
        ///     Update password.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="transaction">The transaction.</param>
        public async Task<bool> UpdatePassword(Guid userId, string passwordHash, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));
            if(string.IsNullOrEmpty(passwordHash))
                throw new ArgumentException("Invalid password hash", nameof(passwordHash));
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.Users.Where(x => x.Id == userId)
                        .Set(u => u.PasswordHash, passwordHash)
                        .Set(u => u.ChangedOn, DateTime.UtcNow)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }
        /// <summary>
        ///     Update national Id.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="nationalId">The national Id.</param>
        /// <param name="nationalIdVerificationDateUtc">The national Id verification date.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> UpdateNationalId(Guid userId, string nationalId, DateTime? nationalIdVerificationDateUtc, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));
            if (string.IsNullOrEmpty(nationalId))
                throw new ArgumentException("Invalid national Id", nameof(nationalId));
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.Users.Where(x => x.Id == userId)
                        .Set(u => u.NationalId, nationalId)
                        .Set(u => u.NationalIdVerificationDateUtc, nationalIdVerificationDateUtc)
                        .Set(u => u.ChangedOn, DateTime.UtcNow)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        /// <summary>
        ///     Create a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<Guid> Create(Guid userId, UserProfile userProfile, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    await db.Administration.UserProfiles
                        .Value(c => c.UserId, userId)
                        .Value(c => c.LastName, userProfile.LastName)
                        .Value(c => c.FirstName, userProfile.FirstName)
                        .Value(c => c.UserTypeId, userProfile.UserTypeId)
                        .Value(c => c.GenderId, userProfile.GenderId)
                        .Value(c => c.CountryId, userProfile.CountryId)
                        .Value(u => u.Title, userProfile.Title)
                        .Value(u => u.Suffix, userProfile.Suffix)
                        .Value(u => u.Prefix, userProfile.Prefix)
                        .Value(u => u.PrefferedName, userProfile.PrefferedName)
                        .Value(u => u.Dob, userProfile.Dob)
                        .Value(u => u.Organization, userProfile.Organization)
                        .Value(u => u.Department, userProfile.Department)
                        .Value(c => c.PictureUrl, userProfile.PictureUrl)
                        .InsertAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return userId;
                }
            }
        }

        /// <summary>
        ///     Update a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> Update(UserProfile userProfile, DependentTransaction transaction = null)
        {
            if (userProfile.UserId == Guid.Empty)
                throw new InvalidOperationException("Invalid user Id");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.UserProfiles.Where(x => x.UserId == userProfile.UserId)
                        .Set(u => u.LastName, userProfile.LastName)
                        .Set(u => u.FirstName, userProfile.FirstName)
                        .Set(u => u.UserTypeId, userProfile.UserTypeId)
                        .Set(u => u.GenderId, userProfile.GenderId)
                        .Set(u => u.CountryId, userProfile.CountryId)
                        .Set(u => u.Title, userProfile.Title)
                        .Set(u => u.Suffix, userProfile.Suffix)
                        .Set(u => u.Prefix, userProfile.Prefix)
                        .Set(u => u.PrefferedName, userProfile.PrefferedName)
                        .Set(u => u.Dob, userProfile.Dob)
                        .Set(u => u.Organization, userProfile.Organization)
                        .Set(u => u.Department, userProfile.Department)
                        .Set(u => u.PictureUrl, userProfile.PictureUrl)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }


        /// <summary>
        ///     Create a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task Create(Guid userId, IEnumerable<UserClaim> claims, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    foreach (var userClaim in claims)
                    {
                        await db.Administration.UserClaims
                            .Value(c => c.UserId, userId)
                            .Value(c => c.ClaimType, userClaim.ClaimType)
                            .Value(c => c.ClaimValue, userClaim.ClaimValue)
                            .InsertAsync();
                    }

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();
                }
            }
        }

        /// <summary>
        ///     Delete a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> Delete(UserClaim userClaim, DependentTransaction transaction = null)
        {
            if (userClaim.UserId == Guid.Empty || string.IsNullOrEmpty(userClaim.ClaimType) || string.IsNullOrEmpty(userClaim.ClaimValue))
                throw new InvalidOperationException("Invalid claim");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.UserClaims.Where(x =>
                        x.UserId == userClaim.UserId && x.ClaimType == userClaim.ClaimType &&
                        x.ClaimValue == userClaim.ClaimValue).DeleteAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        /// <summary>
        ///     UserAddresses.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <returns><see cref="IEnumerable{UserAddress}" />.</returns>
        public async Task<IEnumerable<UserAddress>> UserAddresses(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user id", nameof(userId));
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.UserAddress>().Where(x => x.UserId == userId)
                    .LeftJoin(db.GetTable<UtilitySchema.Address>(),
                        (userAddress, address) => userAddress.AddressId == address.Id, (userAddress, address) => Build(userAddress, address))
                    .ToListAsync();
                return items;
            }
        }

        /// <summary>
        ///     UserAddress.
        /// </summary>
        /// <param name="id">The user address Id.</param>
        /// <returns></returns>
        public async Task<UserAddress> UserAddress(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid id", nameof(id));
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.UserAddress>().Where(x => x.Id == id)
                    .LeftJoin(db.GetTable<UtilitySchema.Address>(),
                        (userAddress, address) => userAddress.AddressId == address.Id, (userAddress, address) => Build(userAddress, address))
                    .ToListAsync();
                return items.FirstOrDefault();
            }
        }

        /// <summary>
        ///     Create a <see cref="Administration.UserAddress"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="userAddress"></param>
        /// <param name="transaction">The transaction.</param>
        public async Task<Guid> Create(Guid userId, UserAddress userAddress, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));
            if (userAddress.Id != Guid.Empty)
                throw new InvalidOperationException("Id has to be empty guid");
            if(userAddress.Address == null)
                throw new InvalidOperationException("UserAddress is required");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var id = Guid.NewGuid();
                    var userAddressId = Guid.NewGuid();
                    var result = await db.Utility.Addresses
                        .Value(c => c.Id, id)
                        .Value(c => c.Address1, userAddress.Address.Address1)
                        .Value(c => c.Address2, userAddress.Address.Address2)
                        .Value(c => c.AddressTypeId, userAddress.Address.AddressTypeId)
                        .Value(c => c.City, userAddress.Address.City)
                        .Value(c => c.County, userAddress.Address.County)
                        .Value(c => c.CountryId, userAddress.Address.CountryId)
                        .Value(u => u.State, userAddress.Address.State)
                        .Value(u => u.Zip, userAddress.Address.Zip)
                        .Value(u => u.CreatedOn, DateTime.UtcNow)
                        .Value(u => u.ChangedOn, DateTime.UtcNow)
                        .InsertAsync();
                    if (result == 1)
                    {
                        await db.Administration.UserAddresses
                            .Value(c => c.Id, userAddressId)
                            .Value(c => c.UserId , userId)
                            .Value(c => c.AddressId, id)
                            .Value(c => c.Preffered, userAddress.Preffered)
                            .InsertAsync();
                    }

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return userAddressId;
                }
            }
        }

        /// <summary>
        ///     Update a <see cref="Administration.UserAddress"/>.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> Update(UserAddress userAddress, DependentTransaction transaction = null)
        {
            if (userAddress.Id == Guid.Empty)
                throw new InvalidOperationException("Invalid Id");
            if(userAddress.Address == null)
                throw new ArgumentException("Invalid address");
            if(userAddress.Address.Id == Guid.Empty)
                throw new ArgumentException("Invalid address Id");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.UserAddresses.Where(x => x.Id == userAddress.Id)
                        .Set(u => u.Preffered, userAddress.Preffered)
                        .UpdateAsync();

                    // Update the address
                    await db.Utility.Addresses.Where(x => x.Id == userAddress.Address.Id)
                        .Set(u => u.Address1, userAddress.Address.Address1)
                        .Set(u => u.Address2, userAddress.Address.Address2)
                        .Set(u => u.AddressTypeId, userAddress.Address.AddressTypeId)
                        .Set(u => u.City, userAddress.Address.City)
                        .Set(u => u.County, userAddress.Address.County)
                        .Set(u => u.CountryId, userAddress.Address.CountryId)
                        .Set(u => u.State, userAddress.Address.State)
                        .Set(u => u.Zip, userAddress.Address.Zip)
                        .Set(u => u.ChangedOn, DateTime.UtcNow)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Build user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        internal static User Build(AdministrationSchema.User user, AdministrationSchema.UserProfile profile)
        {
            if (user != null)
                user.UserProfile = profile;
            return Mapper.Map<AdministrationSchema.User, User>(user);
        }

        /// <summary>
        ///     Build User.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="profile">The profile.</param>
        /// <param name="claims">The claim.</param>
        /// <returns></returns>
        internal static User Build(AdministrationSchema.User user, AdministrationSchema.UserProfile profile, IEnumerable<AdministrationSchema.UserClaim> claims)
        {
            var item = Build(user, profile);
            if (item != null)
                item.Claims = Mapper.Map<IEnumerable<AdministrationSchema.UserClaim>, IEnumerable<UserClaim>>(claims).ToHashSet();
            return item;
        }

        /// <summary>
        ///     Build user address.
        /// </summary>
        /// <param name="userAddress">The user address.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        internal static UserAddress Build(AdministrationSchema.UserAddress userAddress, UtilitySchema.Address address)
        {
            if (userAddress != null)
                userAddress.Address = address;
            return Mapper.Map<AdministrationSchema.UserAddress, UserAddress>(userAddress);
        }

        #endregion
    }
}