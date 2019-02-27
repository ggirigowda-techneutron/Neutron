#region Copyright TechNeutron © 2019

//
// NAME:			UserAddressPocoDtoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/27/2019
// PURPOSE:			POCO DTO Mapper
//

#endregion


#region using

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Administration;
using Classlibrary.Domain.Utility;
using Middleware.Core.WebApi.V1.Models;

#endregion

namespace Middleware.Core.WebApi.V1.Mappers
{
    /// <summary>
    ///     Represents the <see cref="UserAddressPocoDtoMapper" /> class.
    /// </summary> 
    public class UserAddressPocoDtoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts UserAddressDto(s) to UserAddress(s). 
        /// </summary>
        public class UserAddresssConverter : ITypeConverter<IEnumerable<UserAddressDto>, IEnumerable<UserAddress>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserAddressDto} to IEnumerable{UserAddress}
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserAddress}.</returns>
            public IEnumerable<UserAddress> Convert(IEnumerable<UserAddressDto> source, IEnumerable<UserAddress> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserAddressDto, UserAddress>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserAddressDto to UserAddress.
        /// </summary>
        public class UserAddressConverter : ITypeConverter<UserAddressDto, UserAddress>
        {
            /// <summary>
            ///     Convert UserAddressDto to UserAddress.
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserAddress.</returns>
            public UserAddress Convert(UserAddressDto source, UserAddress destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserAddress
                {
                    Id = from.Id,
					UserId = from.UserId,
                    AddressId = from.AddressId,
					Preffered = from.Preferred,
                    Address = new Address(from.AddressId, from.Address1, from.City, from.State, from.Zip, from.CountryId, from.AddressTypeId, DateTime.UtcNow, DateTime.UtcNow)
                };
                return item;
            }
        }
		
		#endregion
		
		#region DTO
		
		/// <summary>
        ///     Converts UserAddress(s) to UserAddressDto(s). 
        /// </summary>
        public class UserAddressDtosConverter : ITypeConverter<IEnumerable<UserAddress>, IEnumerable<UserAddressDto>>
        {
            /// <summary>
            ///     Convert IEnumerable{UserAddress} to IEnumerable{UserAddressDto}.
            /// </summary>
            /// <param name="context">The context.</param>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <returns>IEnumerable{UserAddressDto}.</returns>
            public IEnumerable<UserAddressDto> Convert(IEnumerable<UserAddress> source, IEnumerable<UserAddressDto> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserAddress, UserAddressDto>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserAddress to UserAddressDto.
        /// </summary>
        public class UserAddressDtoConverter : ITypeConverter<UserAddress, UserAddressDto>
        {
            /// <summary>
            ///     Convert UserAddress to UserAddressDto.
            /// </summary>
			/// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserAddressDto.</returns>
            public UserAddressDto Convert(UserAddress source, UserAddressDto destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserAddressDto
                {
                    Id = from.Id,
					UserId = from.UserId,
                    AddressId = from.AddressId,
					Preferred = from.Preffered,
                    AddressTypeId = @from.Address?.AddressTypeId ?? Guid.Empty,
                    Address1 = @from.Address?.Address1 ?? string.Empty,
                    Address2 = @from.Address?.Address2 ?? string.Empty,
                    City = @from.Address?.City ?? string.Empty,
                    County = @from.Address?.County ?? string.Empty,
                    CountryId = @from.Address?.CountryId ?? Guid.Empty,
                    State = @from.Address?.State ?? string.Empty,
                    Zip = @from.Address?.Zip ?? string.Empty,
                };
                return item;
            }
        }
		
		#endregion
    }
}

