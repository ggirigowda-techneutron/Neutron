#region Copyright Neutron © 2019

//
// NAME:			AdministrationTest.cs
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
using Classlibrary.Domain.Administration;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents the <see cref="AdministrationTest" /> class.
    /// </summary>
    [Collection("TestCollection")]
    public class AdministrationTest : TestBase
    {
        /// <summary>
        ///     Creates an instance of <see cref="AdministrationTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public AdministrationTest(ITestOutputHelper output) : base(output)
        {
            _administrationManager = new AdministrationManager();
            _passwordStorage = new PasswordStorage<User>();
        }

        /// <summary>
        ///     The administration manager.
        /// </summary>
        private readonly IAdministrationManager _administrationManager;

        /// <summary>
        ///     The password storage.
        /// </summary>
        private readonly PasswordStorage<User> _passwordStorage;

        /// <summary>
        ///     Can create user.
        /// </summary>
        [Fact]
        public async void CanCreateUser()
        {
            var random = DateTime.Now.ToString("MMddyyhhmmssfff");
            var user = new User(Guid.Empty
                , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault()?.Item1}-{random}"
                , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1}-{random}@testing.com"
                , true
                , _passwordStorage.HashPassword(new User(), "testdb99!!")
                , Guid.NewGuid().ToString()
                , true
                , true
                , false
                , false
                , 0
                , DateTime.UtcNow
                , DateTime.UtcNow);
            user.Profile = new UserProfile(user.Id
                , DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1
                , DataGenerator.GenerateRandomName(1).FirstOrDefault().Item2
                , Guid.Parse("5ebf5cca-df92-49c6-ae5f-f3c9670bf9d3")
                , Guid.Parse("2af6ff6c-8bb8-46f0-b27e-81def1b76b64")
                , Guid.Parse("8a29a4ab-62a7-4a06-b2fa-46a40f449a84"));
            user.Claims.Add(new UserClaim(user.Id, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "USER"));
            var id = await _administrationManager.Create(user);
            Assert.True(id != Guid.Empty, "Failed to create user");
        }

        /// <summary>
        ///     Can update user.
        /// </summary>
        [Fact]
        public async void CanUpdateUser()
        {
            var random = DateTime.Now.ToString("MMddyyhhmmssfff");
            // Create
            var user = new User(Guid.Empty
                , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault()?.Item1}-{random}"
                , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1}-{random}@testing.com"
                , true
                , _passwordStorage.HashPassword(new User(), "testdb99!!")
                , Guid.NewGuid().ToString()
                , true
                , true
                , false
                , false
                , 0
                , DateTime.UtcNow
                , DateTime.UtcNow);
            user.Profile = new UserProfile(user.Id
                , DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1
                , DataGenerator.GenerateRandomName(1).FirstOrDefault().Item2
                , Guid.Parse("5ebf5cca-df92-49c6-ae5f-f3c9670bf9d3")
                , Guid.Parse("2af6ff6c-8bb8-46f0-b27e-81def1b76b64")
                , Guid.Parse("8a29a4ab-62a7-4a06-b2fa-46a40f449a84"));
            user.PhoneNumber = "123-456-7890";
            var id = await _administrationManager.Create(user);
            Assert.True(id != Guid.Empty, "Failed to create user");
            // Find
            var found = await _administrationManager.Get(id);
            Assert.True(found != null, "Failed to find user");
            // Update
            found.PhoneNumber = "999-999-9999";
            var update = await _administrationManager.Update(found);
            Assert.True(update, "Failed to update user");
        }

        /// <summary>
        ///     Can get users.
        /// </summary>
        [Fact]
        public async void CanGetUsers()
        {
            var items = await _administrationManager.All();
            Assert.True(items.Any(), "Failed to find users");
            Output.WriteLine(JsonConvert.SerializeObject(items, Formatting.Indented));
        }

        /// <summary>
        ///     Can get user addresses.
        /// </summary>
        [Fact]
        public async void CanGetUserAddresses()
        {
            var users = await _administrationManager.All();
            Assert.True(users.Any(), "Failed to find users");
            foreach (var user in users)
            {
                var items = await _administrationManager.Addresses(user.Id);
                Assert.True(items != null, "Failed to find addresses");
            }
        }
    }
}