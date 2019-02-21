#region Copyright TechNeutron © 2019

//
// NAME:			ReferencePocoDaoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			POCO DAO Mapper
//

#endregion


#region using

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        ///     Converts Dao.Linq2Db.Utility.Reference(s) to Reference(s). 
        /// </summary>
        public class ReferencesConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.Utility.Reference>, IEnumerable<Reference>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.Utility.Reference(s) to Reference(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Reference}.</returns>
            public IEnumerable<Reference> Convert(IEnumerable<Dao.Linq2Db.Utility.Reference> source, IEnumerable<Reference> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.Utility.Reference, Reference>).ToList();
            }
        }

        /// <summary>
        ///     Converts Dao.Linq2Db.Utility.Reference to Reference.
        /// </summary>
        public class ReferenceConverter : ITypeConverter<Dao.Linq2Db.Utility.Reference, Reference>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.Utility.Reference to Reference.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Reference.</returns>
            public Reference Convert(Dao.Linq2Db.Utility.Reference source, Reference destination, ResolutionContext context)
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
        ///     Converts Reference(s) to Dao.Linq2Db.Utility.Reference(s). 
        /// </summary>
        public class ReferenceDaosConverter : ITypeConverter<IEnumerable<Reference>, IEnumerable<Dao.Linq2Db.Utility.Reference>>
        {
            /// <summary>
            ///     Convert Reference(s) to Dao.Linq2Db.Utility.Reference(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.Utility.Reference}.</returns>
            public IEnumerable<Dao.Linq2Db.Utility.Reference> Convert(IEnumerable<Reference> source, IEnumerable<Dao.Linq2Db.Utility.Reference> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Reference, Dao.Linq2Db.Utility.Reference>).ToList();
            }
        }

        /// <summary>
        ///     Converts Reference to Dao.Linq2Db.Utility.Reference.
        /// </summary>
        public class ReferenceDaoConverter : ITypeConverter<Reference, Dao.Linq2Db.Utility.Reference>
        {
            /// <summary>
            ///     Convert Reference to Dao.Linq2Db.Utility.Reference.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.Utility.Reference.</returns>
            public Dao.Linq2Db.Utility.Reference Convert(Reference source, Dao.Linq2Db.Utility.Reference destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.Utility.Reference
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

