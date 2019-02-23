#region Copyright TechNeutron © 2019

//
// NAME:			UserUpdatePocoDtoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/22/2019
// PURPOSE:			POCO DTO Mapper
//

#endregion


#region using

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Administration;
using Middleware.Core.WebApi.V1.Models;

#endregion

namespace Middleware.Core.WebApi.V1.Mappers
{
    /// <summary>
    ///     Represents the <see cref="UserUpdatePocoDtoMapper" /> class.
    /// </summary>
    public class UserUpdatePocoDtoMapper
    {
        #region POCO

        /// <summary>
        ///     Converts UserUpdateDto(s) to User(s).
        /// </summary>
        public class UsersConverter : ITypeConverter<IEnumerable<UserUpdateDto>, IEnumerable<User>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserUpdateDto} to IEnumerable{User}
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{User}.</returns>
            public IEnumerable<User> Convert(IEnumerable<UserUpdateDto> source,
                IEnumerable<User> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserUpdateDto, User>).ToList();
            }
        }

        /// <summary>
        ///     Converts UserUpdateDto to User.
        /// </summary>
        public class UserConverter : ITypeConverter<UserUpdateDto, User>
        {
            /// <summary>
            ///     Convert UserUpdateDto to User.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>User.</returns>
            public User Convert(UserUpdateDto source, User destination, ResolutionContext context)
            {
                var from = source;
                destination.Email = from.Email;
                destination.PhoneNumber = from.PhoneNumber;
                destination.MobileNumber = from.MobileNumber;
                destination.Profile.FirstName = from.FirstName;
                destination.Profile.LastName = from.LastName;
                destination.Profile.UserTypeId = from.UserTypeId;
                destination.Profile.Title = from.Title;
                destination.Profile.Suffix = from.Suffix;
                destination.Profile.Prefix = from.Prefix;
                destination.Profile.PrefferedName = from.PrefferedName;
                destination.Profile.Dob = from.Dob;
                destination.Profile.GenderId = from.GenderId;
                destination.Profile.CountryId = from.CountryId;
                destination.Profile.Organization = from.Organization;
                destination.Profile.Department = from.Department;
                destination.Profile.PictureUrl = from.PictureUrl;
                destination.Udf1 = from.Udf1;
                destination.Udf2 = from.Udf2;
                destination.Udf3 = from.Udf3;
                destination.ChangedOn = DateTime.UtcNow;
                return destination;
            }
        }

        #endregion
    }
}