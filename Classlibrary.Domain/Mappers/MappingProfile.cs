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
using Classlibrary.Domain.Administration;

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
            CreateMap<Reference, Dao.Linq2Db.UtilitySchema.Reference>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaoConverter());
            CreateMap<IEnumerable<Reference>, IEnumerable<Dao.Linq2Db.UtilitySchema.Reference>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferenceDaosConverter());
            CreateMap<Dao.Linq2Db.UtilitySchema.Reference, Reference>().ConvertUsing(new ReferencePocoDaoMapper.ReferenceConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.UtilitySchema.Reference>, IEnumerable<Reference>>()
                .ConvertUsing(new ReferencePocoDaoMapper.ReferencesConverter());

            // ReferenceItem
            CreateMap<ReferenceItem, Dao.Linq2Db.UtilitySchema.ReferenceItem>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaoConverter());
            CreateMap<IEnumerable<ReferenceItem>, IEnumerable<Dao.Linq2Db.UtilitySchema.ReferenceItem>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemDaosConverter());
            CreateMap<Dao.Linq2Db.UtilitySchema.ReferenceItem, ReferenceItem>().ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.UtilitySchema.ReferenceItem>, IEnumerable<ReferenceItem>>()
                .ConvertUsing(new ReferenceItemPocoDaoMapper.ReferenceItemsConverter());

            // User
            CreateMap<User, Dao.Linq2Db.AdministrationSchema.User>().ConvertUsing(new UserPocoDaoMapper.UserDaoConverter());
            CreateMap<IEnumerable<User>, IEnumerable<Dao.Linq2Db.AdministrationSchema.User>>()
                .ConvertUsing(new UserPocoDaoMapper.UserDaosConverter());
            CreateMap<Dao.Linq2Db.AdministrationSchema.User, User>().ConvertUsing(new UserPocoDaoMapper.UserConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.AdministrationSchema.User>, IEnumerable<User>>()
                .ConvertUsing(new UserPocoDaoMapper.UsersConverter());

            // UserProfile
            CreateMap<UserProfile, Dao.Linq2Db.AdministrationSchema.UserProfile>().ConvertUsing(new UserProfilePocoDaoMapper.UserProfileDaoConverter());
            CreateMap<IEnumerable<UserProfile>, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile>>()
                .ConvertUsing(new UserProfilePocoDaoMapper.UserProfileDaosConverter());
            CreateMap<Dao.Linq2Db.AdministrationSchema.UserProfile, UserProfile>().ConvertUsing(new UserProfilePocoDaoMapper.UserProfileConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile>, IEnumerable<UserProfile>>()
                .ConvertUsing(new UserProfilePocoDaoMapper.UserProfilesConverter());

            // UserClaim
            CreateMap<UserClaim, Dao.Linq2Db.AdministrationSchema.UserClaim>().ConvertUsing(new UserClaimPocoDaoMapper.UserClaimDaoConverter());
            CreateMap<IEnumerable<UserClaim>, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim>>()
                .ConvertUsing(new UserClaimPocoDaoMapper.UserClaimDaosConverter());
            CreateMap<Dao.Linq2Db.AdministrationSchema.UserClaim, UserClaim>().ConvertUsing(new UserClaimPocoDaoMapper.UserClaimConverter());
            CreateMap<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim>, IEnumerable<UserClaim>>()
                .ConvertUsing(new UserClaimPocoDaoMapper.UserClaimsConverter());
        }
    }
}