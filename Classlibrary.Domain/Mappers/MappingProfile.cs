#region Copyright TechNeutron © 2019

//
// NAME:			MappingProfile.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Mapping profile
//

#endregion

using System.Collections.Generic;
using AutoMapper;
using Classlibrary.Domain.Utility;

namespace Classlibrary.Domain.Mappers
{
    /// <inheritdoc />
    /// <summary>
    ///     The <see cref="T:Classlibrary.Domain.Mappers.MappingProfile" /> class.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        ///     Creates an instance of <see cref="MappingProfile" /> class.
        /// </summary>
        public MappingProfile()
        {
            // Reference
            CreateMap<Reference, Dao.Linq2Db.Utility.Reference>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaoConverter());
            CreateMap<IEnumerable<Reference>, IEnumerable<Dao.Linq2Db.Utility.Reference>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaosConverter());
            CreateMap<Dao.Linq2Db.Utility.Reference, Reference>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.Utility.Reference>, IEnumerable<Reference>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferencesConverter());

            // ReferenceItem
            CreateMap<ReferenceItem, Dao.Linq2Db.Utility.ReferenceItem>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaoConverter());
            CreateMap<IEnumerable<ReferenceItem>, IEnumerable<Dao.Linq2Db.Utility.ReferenceItem>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaosConverter());
            CreateMap<Dao.Linq2Db.Utility.ReferenceItem, ReferenceItem>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.Utility.ReferenceItem>, IEnumerable<ReferenceItem>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemsConverter());
        }
    }
}