#region Copyright Neutron © 2019

//
// NAME:			UtilityTest.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit Test
//

#endregion

using System.Linq;
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
        ///     Creates an instance of <see cref="UtilityTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public UtilityTest(ITestOutputHelper output) : base(output)
        {
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
    }
}