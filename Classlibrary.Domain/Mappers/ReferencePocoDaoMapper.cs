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
using Classlibrary.Dao.Utility;
using Classlibrary.Domain.Utility;

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
        public class ReferencesConverter : ITypeConverter<IEnumerable<ReferenceDao>, IEnumerable<Reference>>
        {
            /// <summary>
            ///     Convert IEnumerable<ReferenceDao> to IEnumerable<Reference>.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<Reference>.</returns>
            public IEnumerable<Reference> Convert(IEnumerable<ReferenceDao> source, IEnumerable<Reference> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<ReferenceDao, Reference>).ToList();
            }
        }

        /// <summary>
        ///     Converts ReferenceDao to Reference.
        /// </summary>
        public class ReferenceConverter : ITypeConverter<ReferenceDao, Reference>
        {
            /// <summary>
            ///     Convert ReferenceDao to Reference.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Reference.</returns>
            public Reference Convert(ReferenceDao source, Reference destination, ResolutionContext context)
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
                    ChangedOn = from.ChangedOn,
                };
                return item;
            }
        }

        #endregion

        #region DAO

        /// <summary>
        ///     Converts Reference(s) to ReferenceDao(s). 
        /// </summary>
        public class ReferenceDaosConverter : ITypeConverter<IEnumerable<Reference>, IEnumerable<ReferenceDao>>
        {
            /// <summary>
            ///     Convert IEnumerable<Reference> to IEnumerable<ReferenceDao>
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<ReferenceDao>.</returns>
            public IEnumerable<ReferenceDao> Convert(IEnumerable<Reference> source, IEnumerable<ReferenceDao> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Reference, ReferenceDao>).ToList();
            }
        }

        /// <summary>
        ///     Converts Reference to ReferenceDao.
        /// </summary>
        public class ReferenceDaoConverter : ITypeConverter<Reference, ReferenceDao>
        {
            /// <summary>
            ///     Convert Reference to ReferenceDao.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>ReferenceDao.</returns>
            public ReferenceDao Convert(Reference source, ReferenceDao destination, ResolutionContext context)
            {
                var from = source;
                var item = new ReferenceDao
                {
                    Ci = from.Ci,
                    Id = from.Id,
                    Name = from.Name,
                    Description = from.Description,
                    CountryCode = from.CountryCode,
                    Archived = from.Archived,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn,
                };
                return item;
            }
        }

        #endregion
    }
}

