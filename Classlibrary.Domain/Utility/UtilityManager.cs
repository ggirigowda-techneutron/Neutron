#region Copyright TechNeutron © 2019

//
// NAME:			UtilityManager.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Utility manager
//

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Classlibrary.Dao.Utility;

namespace Classlibrary.Domain.Utility
{
    /// <summary>
    ///     Represents the <see cref="UtilityManager" /> class.
    /// </summary>
    public class UtilityManager : IUtilityManager
    { 

        /// <summary>
        ///     The connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        ///     Creates an instance of <see cref="UtilityManager" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public UtilityManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Implementation of IReferenceManager

        /// <summary>
        ///     Get reference.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="Reference" />.</returns>
        public async Task<Reference> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid id", nameof(id));
            var reference = await ReferenceDaoExtension.GetAsync(id, _connectionString);
            var referenceItems = await ReferenceItemDaoExtension.GetByReferenceAsync(id, _connectionString);
            return Aggregate(reference, referenceItems);
        }

        /// <summary>
        ///     All references.
        /// </summary>
        /// <returns><see cref="IEnumerable{Reference}" />.</returns>
        public async Task<IEnumerable<Reference>> All()
        {
            IList<Reference> items = new List<Reference>();
            var references = await ReferenceDaoExtension.AllAsync(_connectionString);
            foreach (var item in references)
            {
                var referenceItems = await ReferenceItemDaoExtension.GetByReferenceAsync(item.Id, _connectionString);
                items.Add(Aggregate(item, referenceItems));
            }
            return items;
        }

        /// <summary>
        ///     Dao to aggregate aggregate.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child1">The child1</param>
        /// <returns></returns>
        private Reference Aggregate(ReferenceDao parent, IEnumerable<ReferenceItemDao> child1)
        {
            if(parent == null)
                throw new ArgumentException("Invalid parent", nameof(parent));
            if(child1 == null || !child1.Any())
                throw new ArgumentException("Invalid child1", nameof(child1));
            var reference = Mapper.Map<ReferenceDao, Reference>(parent);
            var referenceItems = Mapper.Map<IEnumerable<ReferenceItemDao>, IEnumerable<ReferenceItem>>(child1);
            reference.ReferenceItems = new HashSet<ReferenceItem>(referenceItems.Select(x => x));
            return reference;
        }

        #endregion
    }
}