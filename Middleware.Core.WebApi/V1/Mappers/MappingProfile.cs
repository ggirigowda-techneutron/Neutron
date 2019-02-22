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
using Classlibrary.Domain.Administration;
using Middleware.Core.WebApi.V1.Models;

namespace Middleware.Core.WebApi.V1.Mappers
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
            // User
            CreateMap<User, UserDto>().ConvertUsing(new UserPocoDtoMapper.UserDtoConverter());
            CreateMap<IEnumerable<User>, IEnumerable<UserDto>>()
                .ConvertUsing(new UserPocoDtoMapper.UserDtosConverter());
            CreateMap<UserDto, User>().ConvertUsing(new UserPocoDtoMapper.UserConverter());
            CreateMap<IEnumerable<UserDto>, IEnumerable<User>>()
                .ConvertUsing(new UserPocoDtoMapper.UsersConverter());

            // UserProfile
            CreateMap<UserProfile, UserProfileDto>()
                .ConvertUsing(new UserProfilePocoDtoMapper.UserProfileDtoConverter());
            CreateMap<IEnumerable<UserProfile>, IEnumerable<UserProfileDto>>()
                .ConvertUsing(new UserProfilePocoDtoMapper.UserProfileDtosConverter());
            CreateMap<UserProfileDto, UserProfile>().ConvertUsing(new UserProfilePocoDtoMapper.UserProfileConverter());
            CreateMap<IEnumerable<UserProfileDto>, IEnumerable<UserProfile>>()
                .ConvertUsing(new UserProfilePocoDtoMapper.UserProfilesConverter());

            // UserClaim
            CreateMap<UserClaim, UserClaimDto>().ConvertUsing(new UserClaimPocoDtoMapper.UserClaimDtoConverter());
            CreateMap<IEnumerable<UserClaim>, IEnumerable<UserClaimDto>>()
                .ConvertUsing(new UserClaimPocoDtoMapper.UserClaimDtosConverter());
            CreateMap<UserClaimDto, UserClaim>().ConvertUsing(new UserClaimPocoDtoMapper.UserClaimConverter());
            CreateMap<IEnumerable<UserClaimDto>, IEnumerable<UserClaim>>()
                .ConvertUsing(new UserClaimPocoDtoMapper.UserClaimsConverter());
        }
    }
}