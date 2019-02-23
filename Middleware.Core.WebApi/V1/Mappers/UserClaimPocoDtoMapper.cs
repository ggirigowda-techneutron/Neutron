#region Copyright TechNeutron © 2019

//
// NAME:			UserClaimPocoDtoMapper.cs
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
    ///     Represents the <see cref="UserClaimPocoDtoMapper" /> class.
    /// </summary> 
    public class UserClaimPocoDtoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts UserClaimDto(s) to UserClaim(s). 
        /// </summary>
        public class UserClaimsConverter : ITypeConverter<IEnumerable<UserClaimDto>, IEnumerable<UserClaim>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserClaimDto} to IEnumerable{UserClaim}
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserClaim}.</returns>
            public IEnumerable<UserClaim> Convert(IEnumerable<UserClaimDto> source, IEnumerable<UserClaim> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserClaimDto, UserClaim>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserClaimDto to UserClaim.
        /// </summary>
        public class UserClaimConverter : ITypeConverter<UserClaimDto, UserClaim>
        {
            /// <summary>
            ///     Convert UserClaimDto to UserClaim.
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserClaim.</returns>
            public UserClaim Convert(UserClaimDto source, UserClaim destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserClaim
                {
					UserId = from.UserId,
					ClaimType = from.ClaimType,
					ClaimValue = from.ClaimValue,
                };
                return item;
            }
        }
		
		#endregion
		
		#region DTO
		
		/// <summary>
        ///     Converts UserClaim(s) to UserClaimDto(s). 
        /// </summary>
        public class UserClaimDtosConverter : ITypeConverter<IEnumerable<UserClaim>, IEnumerable<UserClaimDto>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserClaim} to IEnumerable{UserClaimDto}.
            /// </summary>
            /// <param name="context">The context.</param>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <returns>IEnumerable{UserClaimDto}.</returns>
            public IEnumerable<UserClaimDto> Convert(IEnumerable<UserClaim> source, IEnumerable<UserClaimDto> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserClaim, UserClaimDto>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserClaim to UserClaimDto.
        /// </summary>
        public class UserClaimDtoConverter : ITypeConverter<UserClaim, UserClaimDto>
        {
            /// <summary>
            ///     Convert UserClaim to UserClaimDto.
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserClaimDto.</returns>
            public UserClaimDto Convert(UserClaim source, UserClaimDto destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserClaimDto
                {
					UserId = from.UserId,
					ClaimType = from.ClaimType,
					ClaimValue = from.ClaimValue,
                };
                return item;
            }
        }
		
		#endregion
    }
}

