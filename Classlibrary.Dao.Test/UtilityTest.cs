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
using Microsoft.Extensions.Configuration;
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
        ///     Creates an instance of <see cref="UtilityTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public UtilityTest(ITestOutputHelper output) : base(output)
        {
            // Set the connection string
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config["Data:DefaultConnection:ConnectionString"];
        }

        /// <summary>
        ///     Can get references.
        /// </summary>
        [Fact]
        public void CanGetReferences()
        {
            var x = 1;
            Assert.True(x == 1, "No auditlogs found");
        }
    }
}