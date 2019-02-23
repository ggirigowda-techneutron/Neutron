#region Copyright TechNeutron © 2019

//
// NAME:			UserProfilePocoDtoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/22/2019
// PURPOSE:			POCO DTO Mapper
//

#endregion


#region using

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Administration;
using Middleware.Core.WebApi.V1.Models;

#endregion

namespace Middleware.Core.WebApi.V1.Mappers
{
    /// <summary>
    ///     Represents the <see cref="UserProfilePocoDtoMapper" /> class.
    /// </summary>
    public class UserProfilePocoDtoMapper
    {
        #region POCO

        /// <summary>
        ///     Converts UserProfileDto(s) to UserProfile(s).
        /// </summary>
        public class UserProfilesConverter : ITypeConverter<IEnumerable<UserProfileDto>, IEnumerable<UserProfile>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserProfileDto} to IEnumerable{UserProfile}
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserProfile}.</returns>
            public IEnumerable<UserProfile> Convert(IEnumerable<UserProfileDto> source,
                IEnumerable<UserProfile> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserProfileDto, UserProfile>).ToList();
            }
        }

        /// <summary>
        ///     Converts UserProfileDto to UserProfile.
        /// </summary>
        public class UserProfileConverter : ITypeConverter<UserProfileDto, UserProfile>
        {
            /// <summary>
            ///     Convert UserProfileDto to UserProfile.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserProfile.</returns>
            public UserProfile Convert(UserProfileDto source, UserProfile destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserProfile
                {
                    UserId = from.UserId,
                    FirstName = from.FirstName,
                    LastName = from.LastName,
                    UserTypeId = from.UserTypeId,
                    Title = from.Title,
                    Suffix = from.Suffix,
                    Prefix = from.Prefix,
                    PrefferedName = from.PrefferedName,
                    Dob = from.Dob,
                    GenderId = from.GenderId,
                    CountryId = from.CountryId,
                    Organization = from.Organization,
                    Department = from.Department,
                    PictureUrl = from.PictureUrl,
                    Udf1 = from.Udf1,
                    Udf2 = from.Udf2,
                    Udf3 = from.Udf3
                };
                return item;
            }
        }

        #endregion

        #region DTO

        /// <summary>
        ///     Converts UserProfile(s) to UserProfileDto(s).
        /// </summary>
        public class UserProfileDtosConverter : ITypeConverter<IEnumerable<UserProfile>, IEnumerable<UserProfileDto>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserProfile} to IEnumerable{UserProfileDto}.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <returns>IEnumerable{UserProfileDto}.</returns>
            public IEnumerable<UserProfileDto> Convert(IEnumerable<UserProfile> source,
                IEnumerable<UserProfileDto> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserProfile, UserProfileDto>).ToList();
            }
        }

        /// <summary>
        ///     Converts UserProfile to UserProfileDto.
        /// </summary>
        public class UserProfileDtoConverter : ITypeConverter<UserProfile, UserProfileDto>
        {
            /// <summary>
            ///     Convert UserProfile to UserProfileDto.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserProfileDto.</returns>
            public UserProfileDto Convert(UserProfile source, UserProfileDto destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserProfileDto
                {
                    UserId = from.UserId,
                    FirstName = from.FirstName,
                    LastName = from.LastName,
                    UserTypeId = from.UserTypeId,
                    Title = from.Title,
                    Suffix = from.Suffix,
                    Prefix = from.Prefix,
                    PrefferedName = from.PrefferedName,
                    Dob = from.Dob,
                    GenderId = from.GenderId,
                    CountryId = from.CountryId,
                    Organization = from.Organization,
                    Department = from.Department,
                    PictureUrl = from.PictureUrl,
                    Udf1 = from.Udf1,
                    Udf2 = from.Udf2,
                    Udf3 = from.Udf3
                };
                return item;
            }
        }

        #endregion
    }
}