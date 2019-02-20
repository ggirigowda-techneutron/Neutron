#region Copyright Neutron © 2019

//
// NAME:			UtilityTest.cs
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
using Classlibrary.Dao.Administration;
using Classlibrary.Dao.Utility;
using Xunit;
using Xunit.Abstractions;

namespace Classlibrary.Dao.Test
{
    /// <summary>
    ///     Represent the <see cref="UtilityTest" /> class.
    /// </summary>
    public class UtilityTest : TestBase
    {
        /// <summary>
        ///     The password storage.
        /// </summary>
        private readonly PasswordStorage<UserDao> _passwordStorage;

        /// <summary>
        ///     Creates an instance of <see cref="UtilityTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public UtilityTest(ITestOutputHelper output) : base(output)
        {
            _passwordStorage = new PasswordStorage<UserDao>();
        }

        /// <summary>
        ///     Can get references.
        /// </summary>
        [Fact]
        public async void CanGetReferences()
        {
            var items = await ReferenceDaoExtension.AllAsync(ConnectionString);
            Assert.True(items.Any(), "Failed to get references");
            if (items.Any())
                foreach (var item in items)
                    Output.WriteLine(item.Name);
        }

        /// <summary>
        ///     Can create user.
        /// </summary>
        [Fact]
        public async void CanCreateUser()
        {
            var random = DateTime.Now.ToString("MMddyyhhmmssfff");
            var item = new UserDao(Guid.NewGuid()
                    , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault()?.Item1}-{random}"
                    , $"{DataGenerator.GenerateRandomName(1).FirstOrDefault().Item1}-{random}@testing.com"
                    , true
                    , _passwordStorage.HashPassword(new UserDao(), "testdb99!!")
                    , Guid.NewGuid().ToString()
                    , true, false, false, 0
                    , DateTime.UtcNow,
                    DateTime.UtcNow);
            var result = await item.InsertAsync(ConnectionString);
            Assert.True(result != null && result.Id != Guid.Empty, "Failed to create user");
        }
    }
}