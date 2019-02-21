#region Copyright Neutron © 2019

//
// NAME:			NeutronTest.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit Test
//

#endregion

using System;
using System.Linq;
using Classlibrary.Crosscutting.Security;
using Classlibrary.Crosscutting.Testing;
using LinqToDB;
using Xunit;
using Xunit.Abstractions;

namespace Classlibrary.Dao.Linq2Db.Test
{
    /// <summary>
    ///     Represent the <see cref="NeutronTest" /> class.
    /// </summary>
    public class NeutronTest : TestBase
    {
        /// <summary>
        ///     Creates an instance of <see cref="NeutronTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public NeutronTest(ITestOutputHelper output) : base(output)
        {
            _passwordStorage = new PasswordStorage<AdministrationSchema.User>();
        }

        /// <summary>
        ///     The password storage.
        /// </summary>
        private readonly PasswordStorage<AdministrationSchema.User> _passwordStorage;

        /// <summary>
        ///     Can create user.
        /// </summary>
        [Fact]
        public async void CanCreateUser()
        {
            var random = DateTime.Now.ToString("MMddyyhhmmssfff");
            using (var db = new PRACTISEV1DB())
            {
                try
                {
                    db.BeginTransaction();

                    var guid = Guid.NewGuid();
                    var result = await db.Administration.Users
                        .Value(c => c.Id, Guid.NewGuid())
                        .Value(c => c.UserName,
                            $"{DataGenerator.GenerateRandomName(1).FirstOrDefault()?.Item1}-{random}")
                        .Value(c => c.Email,
                            $"{DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1}-{random}@testing.com")
                        .Value(c => c.PasswordHash,
                            _passwordStorage.HashPassword(new AdministrationSchema.User(), "testdb99!!"))
                        .Value(c => c.SecurityStamp, Guid.NewGuid().ToString())
                        .Value(c => c.PhoneNumber, "123-456-7890")
                        .Value(c => c.EmailConfirmed, true)
                        .Value(c => c.PhoneNumberConfirmed, true)
                        .Value(c => c.LockoutEnabled, false)
                        .Value(c => c.AccessFailedCount, 0)
                        .Value(c => c.TwoFactorEnabled, false)
                        .Value(c => c.CreatedOn, DateTime.UtcNow)
                        .InsertAsync();

                    db.CommitTransaction();
                    Assert.True(result == 1, "Failed to create user");
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();
                    throw ex;
                }
            }
        }

        /// <summary>
        ///     Can get references.
        /// </summary>
        [Fact]
        public async void CanGetReferences()
        {
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.Utility.References.AsQueryable().ToListAsync();
                Assert.True(items.Any(), "Failed to find references");
            }
        }
    }
}