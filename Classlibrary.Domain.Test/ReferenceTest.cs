#region Copyright Neutron © 2019

//
// NAME:			ReferenceTest.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit Test
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Utility;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents the <see cref="ReferenceTest" /> class.
    /// </summary>
    [Collection("TestCollection")]
    public class ReferenceTest : TestBase
    {
        /// <summary>
        ///     The utility manager.
        /// </summary>
        private readonly IUtilityManager _utilityManager;

        /// <summary>
        ///     Creates an instance of <see cref="ReferenceTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public ReferenceTest(ITestOutputHelper output) : base(output)
        {
            _utilityManager = new UtilityManager();
        }

        /// <summary>
        ///     Can map.
        /// </summary>
        [Fact]
        public void CanMap()
        {
            // Map Reference to ReferenceDao
            var item = new Reference(Guid.NewGuid(), "Test", "US", DateTime.UtcNow, DateTime.UtcNow);
            Assert.True(item != null, "Failed to create a reference");
            var mapped = Mapper.Map<Reference, Dao.Linq2Db.UtilitySchema.Reference>(item);
            Assert.True(mapped != null && mapped.Id == item.Id, "Failed to map");
            Output.WriteLine($"{mapped.Id}");
            // Map References to ReferenceDaos
            var items = Enumerable.Range(0, 10).Select(x => new Reference(Guid.NewGuid(), "Test", "US", DateTime.UtcNow, DateTime.UtcNow));
            var mappeds = Mapper.Map<IEnumerable<Reference>, IEnumerable<Dao.Linq2Db.UtilitySchema.Reference>>(items);
            Assert.True(mappeds.Count() > 9, "Failed to map enumerable");
        }

        /// <summary>
        ///     Can get references.
        /// </summary>
        [Fact]
        public async void CanGetReferences()
        {
            var items = await _utilityManager.All();
            Assert.True(items.Any(), "Failed to find references");
            Output.WriteLine(JsonConvert.SerializeObject(items, Formatting.Indented));
        }
    }
}