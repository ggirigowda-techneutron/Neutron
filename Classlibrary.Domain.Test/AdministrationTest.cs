#region Copyright Neutron © 2019

//
// NAME:			AdministrationTest.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit Test
//

#endregion

using System.Linq;
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
        ///     The administration manager.
        /// </summary>
        private readonly IAdministrationManager _administrationManager;

        /// <summary>
        ///     Creates an instance of <see cref="AdministrationTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public AdministrationTest(ITestOutputHelper output) : base(output)
        {
            _administrationManager = new AdministrationManager();
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
    }
}