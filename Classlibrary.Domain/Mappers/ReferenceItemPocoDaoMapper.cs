#region Copyright TechNeutron © 2019

//
// NAME:			ReferenceItemPocoDaoMapper.cs
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
    ///     Represents the <see cref="ReferenceItemPocoDaoMapper" /> class.
    /// </summary> 
    public class ReferenceItemPocoDaoMapper
    {
        #region POCO

        /// <summary>
        ///     Converts ReferenceItemDao(s) to ReferenceItem(s). 
        /// </summary>
        public class ReferenceItemsConverter : ITypeConverter<IEnumerable<ReferenceItemDao>, IEnumerable<ReferenceItem>>
        {
            /// <summary>
            ///     Convert IEnumerable<ReferenceItemDao> to IEnumerable<ReferenceItem>.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<ReferenceItem>.</returns>
            public IEnumerable<ReferenceItem> Convert(IEnumerable<ReferenceItemDao> source, IEnumerable<ReferenceItem> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<ReferenceItemDao, ReferenceItem>).ToList();
            }
        }

        /// <summary>
        ///     Converts ReferenceItemDao to ReferenceItem.
        /// </summary>
        public class ReferenceItemConverter : ITypeConverter<ReferenceItemDao, ReferenceItem>
        {
            /// <summary>
            ///     Convert ReferenceItemDao to ReferenceItem.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>ReferenceItem.</returns>
            public ReferenceItem Convert(ReferenceItemDao source, ReferenceItem destination, ResolutionContext context)
            {
                var from = source;
                var item = new ReferenceItem
                {
                    Ci = from.Ci,
                    Id = from.Id,
                    ReferenceId = from.ReferenceId,
                    Code = from.Code,
                    Description = from.Description,
                    Archived = from.Archived,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn,
                    Udf1 = from.Udf1,
                    Udf2 = from.Udf2,
                    Udf3 = from.Udf3,
                };
                return item;
            }
        }

        #endregion

        #region DAO

        /// <summary>
        ///     Converts ReferenceItem(s) to ReferenceItemDao(s). 
        /// </summary>
        public class ReferenceItemDaosConverter : ITypeConverter<IEnumerable<ReferenceItem>, IEnumerable<ReferenceItemDao>>
        {
            /// <summary>
            ///     Convert IEnumerable<ReferenceItem> to IEnumerable<ReferenceItemDao>
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable<ReferenceItemDao>.</returns>
            public IEnumerable<ReferenceItemDao> Convert(IEnumerable<ReferenceItem> source, IEnumerable<ReferenceItemDao> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<ReferenceItem, ReferenceItemDao>).ToList();
            }
        }

        /// <summary>
        ///     Converts ReferenceItem to ReferenceItemDao.
        /// </summary>
        public class ReferenceItemDaoConverter : ITypeConverter<ReferenceItem, ReferenceItemDao>
        {
            /// <summary>
            ///     Convert ReferenceItem to ReferenceItemDao.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>ReferenceItemDao.</returns>
            public ReferenceItemDao Convert(ReferenceItem source, ReferenceItemDao destination, ResolutionContext context)
            {
                var from = source;
                var item = new ReferenceItemDao
                {
                    Ci = from.Ci,
                    Id = from.Id,
                    ReferenceId = from.ReferenceId,
                    Code = from.Code,
                    Description = from.Description,
                    Archived = from.Archived,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn,
                    Udf1 = from.Udf1,
                    Udf2 = from.Udf2,
                    Udf3 = from.Udf3,
                };
                return item;
            }
        }

        #endregion
    }
}

