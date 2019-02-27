#region Copyright TechNeutron © 2019

//
// NAME:			UserAddressPocoDaoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/27/2019
// PURPOSE:			POCO DAO Mapper
//

#endregion


#region using

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Administration;
using Classlibrary.Domain.Utility;

#endregion

namespace Classlibrary.Domain.Mappers
{
    /// <summary>
    ///     Represents the <see cref="UserAddressPocoDaoMapper" /> class.
    /// </summary> 
    public class UserAddressPocoDaoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserAddress(s) to UserAddress(s). 
        /// </summary>
        public class UserAddresssConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserAddress>, IEnumerable<UserAddress>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserAddress(s) to UserAddress(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserAddress}.</returns>
            public IEnumerable<UserAddress> Convert(IEnumerable<Dao.Linq2Db.AdministrationSchema.UserAddress> source, IEnumerable<UserAddress> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.AdministrationSchema.UserAddress, UserAddress>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserAddress to UserAddress.
        /// </summary>
        public class UserAddressConverter : ITypeConverter<Dao.Linq2Db.AdministrationSchema.UserAddress, UserAddress>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserAddress to UserAddress.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserAddress.</returns>
            public UserAddress Convert(Dao.Linq2Db.AdministrationSchema.UserAddress source, UserAddress destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserAddress
                {
					Ci = from.Ci,
					Id = from.Id,
					UserId = from.UserId,
					AddressId = from.AddressId,
					Preffered = from.Preffered,
                    Address = from.Address != null ? Mapper.Map<Dao.Linq2Db.UtilitySchema.Address, Address>(from.Address) : null,
                };
                return item;
            }
        }
		
		#endregion
		
		#region DAO
		
		/// <summary>
        ///     Converts UserAddress(s) to Dao.Linq2Db.AdministrationSchema.UserAddress(s). 
        /// </summary>
        public class UserAddressDaosConverter : ITypeConverter<IEnumerable<UserAddress>, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserAddress>>
        {
            /// <summary>
            ///     Convert UserAddress(s) to Dao.Linq2Db.AdministrationSchema.UserAddress(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.AdministrationSchema.UserAddress}.</returns>
            public IEnumerable<Dao.Linq2Db.AdministrationSchema.UserAddress> Convert(IEnumerable<UserAddress> source, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserAddress> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserAddress, Dao.Linq2Db.AdministrationSchema.UserAddress>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserAddress to Dao.Linq2Db.AdministrationSchema.UserAddress.
        /// </summary>
        public class UserAddressDaoConverter : ITypeConverter<UserAddress, Dao.Linq2Db.AdministrationSchema.UserAddress>
        {
            /// <summary>
            ///     Convert UserAddress to Dao.Linq2Db.AdministrationSchema.UserAddress.
            /// </summary>
             /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.AdministrationSchema.UserAddress.</returns>
            public Dao.Linq2Db.AdministrationSchema.UserAddress Convert(UserAddress source, Dao.Linq2Db.AdministrationSchema.UserAddress destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.AdministrationSchema.UserAddress
                {
					Ci = from.Ci,
					Id = from.Id,
					UserId = from.UserId,
					AddressId = from.AddressId,
					Preffered = from.Preffered,
                };
                return item;
            }
        }
		
		#endregion
    }
}

