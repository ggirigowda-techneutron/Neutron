#region Copyright TechNeutron © 2019

//
// NAME:			ReferencePocoDaoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			POCO DAO Mapper
//

#endregion


#region using

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Dao.Linq2Db.Utility;

#endregion

namespace Classlibrary.Domain.Mappers
{
    /// <summary>
    ///     Represents the <see cref="ReferencePocoDaoMapper" /> class.
    /// </summary>
    public class ReferencePocoDaoMapper
    {
        #region POCO

        /// <summary>
        ///     Converts ReferenceDao(s) to Reference(s).
        /// </summary>
        public class ReferencesConverter : ITypeConverter<IEnumerable<Reference>, IEnumerable<Utility.Reference>>
        {
            /// <summary>
            ///     Convert IEnumerable<Classlibrary.Dao.Linq2Db.Utility.Reference> to IEnumerable<Reference>.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<Reference>.</returns>
            public IEnumerable<Utility.Reference> Convert(IEnumerable<Reference> source,
                IEnumerable<Utility.Reference> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Reference, Utility.Reference>).ToList();
            }
        }

        /// <summary>
        ///     Converts ReferenceDao to Reference.
        /// </summary>
        public class ReferenceConverter : ITypeConverter<Reference, Utility.Reference>
        {
            /// <summary>
            ///     Convert ReferenceDao to Reference.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Reference.</returns>
            public Utility.Reference Convert(Reference source, Utility.Reference destination, ResolutionContext context)
            {
                var from = source;
                var item = new Utility.Reference
                {
                    Ci = from.Ci,
                    Id = from.Id,
                    Name = from.Name,
                    Description = from.Description,
                    CountryCode = from.CountryCode,
                    Archived = from.Archived,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn
                };
                return item;
            }
        }

        #endregion

        #region DAO

        /// <summary>
        ///     Converts Reference(s) to ReferenceDao(s).
        /// </summary>
        public class ReferenceDaosConverter : ITypeConverter<IEnumerable<Utility.Reference>, IEnumerable<Reference>>
        {
            /// <summary>
            ///     Convert IEnumerable<Reference> to IEnumerable<Classlibrary.Dao.Linq2Db.Utility.Reference>
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<ReferenceDao>.</returns>
            public IEnumerable<Reference> Convert(IEnumerable<Utility.Reference> source,
                IEnumerable<Reference> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Utility.Reference, Reference>).ToList();
            }
        }

        /// <summary>
        ///     Converts Reference to ReferenceDao.
        /// </summary>
        public class ReferenceDaoConverter : ITypeConverter<Utility.Reference, Reference>
        {
            /// <summary>
            ///     Convert Reference to ReferenceDao.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>ReferenceDao.</returns>
            public Reference Convert(Utility.Reference source, Reference destination, ResolutionContext context)
            {
                var from = source;
                var item = new Reference
                {
                    Ci = from.Ci,
                    Id = from.Id,
                    Name = from.Name,
                    Description = from.Description,
                    CountryCode = from.CountryCode,
                    Archived = from.Archived,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn
                };
                return item;
            }
        }

        #endregion
    }
}