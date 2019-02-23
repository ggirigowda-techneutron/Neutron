#region Copyright TechNeutron © 2019

//
// NAME:			UserPocoDtoMapper.cs
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
    ///     Represents the <see cref="UserPocoDtoMapper" /> class.
    /// </summary>
    public class UserPocoDtoMapper
    {
        #region POCO

        /// <summary>
        ///     Converts UserDto(s) to User(s).
        /// </summary>
        public class UsersConverter : ITypeConverter<IEnumerable<UserDto>, IEnumerable<User>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserDto} to IEnumerable{User}
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{User}.</returns>
            public IEnumerable<User> Convert(IEnumerable<UserDto> source, IEnumerable<User> destination,
                ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserDto, User>).ToList();
            }
        }

        /// <summary>
        ///     Converts UserDto to User.
        /// </summary>
        public class UserConverter : ITypeConverter<UserDto, User>
        {
            /// <summary>
            ///     Convert UserDto to User.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>User.</returns>
            public User Convert(UserDto source, User destination, ResolutionContext context)
            {
                var from = source;
                var item = new User
                {
                    Id = from.Id,
                    UserName = from.UserName,
                    Email = from.Email,
                    EmailConfirmed = from.EmailConfirmed,
                    PhoneNumber = from.PhoneNumber,
                    PhoneNumberConfirmed = from.PhoneNumberConfirmed,
                    MobileNumber = from.MobileNumber,
                    TwoFactorEnabled = from.TwoFactorEnabled,
                    LockoutEndDateUtc = from.LockoutEndDateUtc,
                    LockoutEnabled = from.LockoutEnabled,
                    AccessFailedCount = from.AccessFailedCount,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn,
                    DeletedOn = from.DeletedOn,
                    DeactivatedDate = from.DeactivatedDate,
                    Udf1 = from.Udf1,
                    Udf2 = from.Udf2,
                    Udf3 = from.Udf3,
                    Profile = from.Profile != null ? Mapper.Map<UserProfileDto, UserProfile>(from.Profile) : null,
                    Claims = from.Claims != null && from.Claims.Any() ? Mapper.Map<IEnumerable<UserClaimDto>, IEnumerable<UserClaim>>(from.Claims).ToHashSet() : new HashSet<UserClaim>()
                };
                return item;
            }
        }

        #endregion

        #region DTO

        /// <summary>
        ///     Converts User(s) to UserDto(s).
        /// </summary>
        public class UserDtosConverter : ITypeConverter<IEnumerable<User>, IEnumerable<UserDto>>
        {
            /// <summary>
            ///     Convert IEnumerable{User} to IEnumerable{UserDto}.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <returns>IEnumerable{UserDto}.</returns>
            public IEnumerable<UserDto> Convert(IEnumerable<User> source, IEnumerable<UserDto> destination,
                ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<User, UserDto>).ToList();
            }
        }

        /// <summary>
        ///     Converts User to UserDto.
        /// </summary>
        public class UserDtoConverter : ITypeConverter<User, UserDto>
        {
            /// <summary>
            ///     Convert User to UserDto.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserDto.</returns>
            public UserDto Convert(User source, UserDto destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserDto
                {
                    Id = from.Id,
                    UserName = from.UserName,
                    Email = from.Email,
                    EmailConfirmed = from.EmailConfirmed,
                    PhoneNumber = from.PhoneNumber,
                    PhoneNumberConfirmed = from.PhoneNumberConfirmed,
                    MobileNumber = from.MobileNumber,
                    TwoFactorEnabled = from.TwoFactorEnabled,
                    LockoutEndDateUtc = from.LockoutEndDateUtc,
                    LockoutEnabled = from.LockoutEnabled,
                    AccessFailedCount = from.AccessFailedCount,
                    CreatedOn = from.CreatedOn,
                    ChangedOn = from.ChangedOn,
                    DeletedOn = from.DeletedOn,
                    DeactivatedDate = from.DeactivatedDate,
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