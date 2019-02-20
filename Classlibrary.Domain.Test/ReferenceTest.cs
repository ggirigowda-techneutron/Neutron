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
using Classlibrary.Dao.Utility;
using Classlibrary.Domain.Utility;
using Xunit;
using Xunit.Abstractions;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents the <see cref="ReferenceTest" /> class.
    /// </summary>
    public class ReferenceTest : TestBase
    {
        /// <summary>
        ///     Creates an instance of <see cref="ReferenceTest" /> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public ReferenceTest(ITestOutputHelper output) : base(output)
        {
            // Can also use assembly names:
            Mapper.Initialize(cfg =>
                cfg.AddProfiles("Classlibrary.Domain")
            );
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
            var mapped = Mapper.Map<Reference, ReferenceDao>(item);
            Assert.True(mapped != null && mapped.Id == item.Id, "Failed to map");
            Output.WriteLine($"{mapped.Id}");
            // Map References to ReferenceDaos
            var items = Enumerable.Range(0, 10).Select(x => new Reference(Guid.NewGuid(), "Test", "US", DateTime.UtcNow, DateTime.UtcNow));
            var mappeds = Mapper.Map<IEnumerable<Reference>, IEnumerable<ReferenceDao>>(items);
            Assert.True(mappeds.Count() > 9, "Failed to map enumerable");
        }
    }
}