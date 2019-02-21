﻿#region Copyright TechNeutron © 2019

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
using Classlibrary.Dao.Utility;
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
            CreateMap<Reference, ReferenceDao>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaoConverter());
            CreateMap<IEnumerable<Reference>, IEnumerable<ReferenceDao>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaosConverter());
            CreateMap<ReferenceDao, Reference>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceConverter());
            CreateMap<IEnumerable<ReferenceDao>, IEnumerable<Reference>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferencesConverter());

            // ReferenceItem
            CreateMap<ReferenceItem, ReferenceItemDao>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaoConverter());
            CreateMap<IEnumerable<ReferenceItem>, IEnumerable<ReferenceItemDao>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaosConverter());
            CreateMap<ReferenceItemDao, ReferenceItem>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemConverter());
            CreateMap<IEnumerable<ReferenceItemDao>, IEnumerable<ReferenceItem>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemsConverter());
        }
    }
}