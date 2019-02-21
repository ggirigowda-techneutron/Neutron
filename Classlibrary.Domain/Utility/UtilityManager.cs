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
                var reference = await db.Utility.References.Where(x => x.Id == id).FirstOrDefaultAsync();
                reference.ReferenceItems = await db.Utility.ReferenceItems.Where(x => x.ReferenceId == id).AsQueryable().ToListAsync();
                return Mapper.Map<UtilitySchema.Reference, Reference>(reference);
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
                var references = await db.Utility.References.Where(x => x.Id != Guid.Empty).AsQueryable().ToListAsync();
                foreach (var reference in references)
                {
                    reference.ReferenceItems = await db.Utility.ReferenceItems.Where(x => x.ReferenceId == reference.Id)
                        .AsQueryable()
                        .ToListAsync();
                }
                return Mapper.Map<IEnumerable<UtilitySchema.Reference>, IEnumerable<Reference>>(references);
            }
        }

        #endregion
    }
}