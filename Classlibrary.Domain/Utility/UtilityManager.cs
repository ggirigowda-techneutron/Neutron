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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Classlibrary.Dao.Linq2Db;
using LinqToDB;

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
            using (var db = new PRACTISEV1DB())
            {
                var reference = await db.Utility.References.Where(x => x.Id == id).FirstOrDefaultAsync();
                var referenceItems =
                    await db.Utility.ReferenceItems.Where(x => x.ReferenceId == id).AsQueryable().ToListAsync();
                return Aggregate(reference, referenceItems);
            }
        }

        /// <summary>
        ///     All references.
        /// </summary>
        /// <returns><see cref="IEnumerable{Reference}" />.</returns>
        public async Task<IEnumerable<Reference>> All()
        {
            IList<Reference> items = new List<Reference>();

            using (var db = new PRACTISEV1DB())
            {
                var references = await db.Utility.References.Where(x => x.Id != Guid.Empty).AsQueryable().ToListAsync();
                foreach (var reference in references)
                {
                    var referenceItems = await db.Utility.ReferenceItems.Where(x => x.ReferenceId == reference.Id)
                        .AsQueryable()
                        .ToListAsync();
                    items.Add(Aggregate(reference, referenceItems));
                }
            }

            return items;
        }

        /// <summary>
        ///     Dao to aggregate.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child1">The child1</param>
        /// <returns></returns>
        private Reference Aggregate(UtilitySchema.Reference parent,
            IEnumerable<UtilitySchema.ReferenceItem> child1)
        {
            if (parent == null)
                throw new ArgumentException("Invalid parent", nameof(parent));
            if (child1 == null || !child1.Any())
                throw new ArgumentException("Invalid child1", nameof(child1));
            var reference = Mapper.Map<UtilitySchema.Reference, Reference>(parent);
            var referenceItems =
                Mapper.Map<IEnumerable<UtilitySchema.ReferenceItem>, IEnumerable<ReferenceItem>>(child1);
            reference.ReferenceItems = new HashSet<ReferenceItem>(referenceItems.Select(x => x));
            return reference;
        }

        #endregion
    }
}