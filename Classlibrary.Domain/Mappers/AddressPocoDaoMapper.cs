#region Copyright TechNeutron © 2019

//
// NAME:			AddressPocoDaoMapper.cs
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
using Classlibrary.Domain.Utility;

#endregion

namespace Classlibrary.Domain.Mappers
{
    /// <summary>
    ///     Represents the <see cref="AddressPocoDaoMapper" /> class.
    /// </summary> 
    public class AddressPocoDaoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts Dao.Linq2Db.UtilitySchema.UserAddress(s) to UserAddress(s). 
        /// </summary>
        public class AddresssConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.UtilitySchema.Address>, IEnumerable<Address>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.UtilitySchema.UserAddress(s) to UserAddress(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserAddress}.</returns>
            public IEnumerable<Address> Convert(IEnumerable<Dao.Linq2Db.UtilitySchema.Address> source, IEnumerable<Address> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.UtilitySchema.Address, Address>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts Dao.Linq2Db.UtilitySchema.UserAddress to UserAddress.
        /// </summary>
        public class AddressConverter : ITypeConverter<Dao.Linq2Db.UtilitySchema.Address, Address>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.UtilitySchema.UserAddress to UserAddress.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserAddress.</returns>
            public Address Convert(Dao.Linq2Db.UtilitySchema.Address source, Address destination, ResolutionContext context)
            {
                var from = source;
                var item = new Address
                {
					Ci = from.Ci,
					Id = from.Id,
					Address1 = from.Address1,
					Address2 = from.Address2,
					City = from.City,
					County = from.County,
					State = from.State,
					Zip = from.Zip,
					CountryId = from.CountryId,
					AddressTypeId = from.AddressTypeId,
					Latitude = from.Latitude,
					Longitude = from.Longitude,
					CreatedOn = from.CreatedOn,
					ChangedOn = from.ChangedOn,
					Udf1 = from.Udf1,
					Udf2 = from.Udf2,
					Udf3 = from.Udf3,
                };
                return item;
            }
        }
		
		#endregion
		
		#region DAO
		
		/// <summary>
        ///     Converts UserAddress(s) to Dao.Linq2Db.UtilitySchema.UserAddress(s). 
        /// </summary>
        public class AddressDaosConverter : ITypeConverter<IEnumerable<Address>, IEnumerable<Dao.Linq2Db.UtilitySchema.Address>>
        {
            /// <summary>
            ///     Convert UserAddress(s) to Dao.Linq2Db.UtilitySchema.UserAddress(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.UtilitySchema.UserAddress}.</returns>
            public IEnumerable<Dao.Linq2Db.UtilitySchema.Address> Convert(IEnumerable<Address> source, IEnumerable<Dao.Linq2Db.UtilitySchema.Address> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Address, Dao.Linq2Db.UtilitySchema.Address>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserAddress to Dao.Linq2Db.UtilitySchema.UserAddress.
        /// </summary>
        public class AddressDaoConverter : ITypeConverter<Address, Dao.Linq2Db.UtilitySchema.Address>
        {
            /// <summary>
            ///     Convert UserAddress to Dao.Linq2Db.UtilitySchema.UserAddress.
            /// </summary>
             /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.UtilitySchema.UserAddress.</returns>
            public Dao.Linq2Db.UtilitySchema.Address Convert(Address source, Dao.Linq2Db.UtilitySchema.Address destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.UtilitySchema.Address
                {
					Ci = from.Ci,
					Id = from.Id,
					Address1 = from.Address1,
					Address2 = from.Address2,
					City = from.City,
					County = from.County,
					State = from.State,
					Zip = from.Zip,
					CountryId = from.CountryId,
					AddressTypeId = from.AddressTypeId,
					Latitude = from.Latitude,
					Longitude = from.Longitude,
					CreatedOn = from.CreatedOn,
					ChangedOn = from.ChangedOn,
					Udf1 = from.Udf1,
					Udf2 = from.Udf2,
					Udf3 = from.Udf3,
                };
                return item;
            }
        }
		
		#endregion
    }
}

