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
                var items = await db.GetTable<UtilitySchema.Reference>().Where(x => x.Id == id)
                    .GroupJoin(db.GetTable<UtilitySchema.ReferenceItem>(), reference => reference.Id, referenceItem => referenceItem.ReferenceId, (parent, child) => Build(parent, child.ToList()))
                    .ToListAsync();
                return items.FirstOrDefault();
            }
        }

        /// <summary>
        ///     All references.
        /// </summary>
        /// <returns><see cref="IEnumerable{Reference}" />.</returns>
        public async Task<IEnumerable<Reference>> All()
        {
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<UtilitySchema.Reference>()
                    .GroupJoin(db.GetTable<UtilitySchema.ReferenceItem>(), reference => reference.Id, referenceItem => referenceItem.ReferenceId, (parent, child) => Build(parent, child.ToList()))
                    .ToListAsync();
                return items;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Build Reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="referenceItems">The reference items.</param>
        /// <returns></returns>
        private static Reference Build(UtilitySchema.Reference reference, IEnumerable<UtilitySchema.ReferenceItem> referenceItems)
        {
            if (reference != null)
                reference.ReferenceItems = referenceItems;
            return Mapper.Map<UtilitySchema.Reference, Reference>(reference);
        }

        #endregion

    }
}