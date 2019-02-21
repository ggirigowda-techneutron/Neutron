#region Copyright TechNeutron © 2019

//
// NAME:			UserProfilePocoDaoMapper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			POCO DAO Mapper
//

#endregion


#region using

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Classlibrary.Domain.Administration;

#endregion

namespace Classlibrary.Domain.Mappers
{
    /// <summary>
    ///     Represents the <see cref="UserProfilePocoDaoMapper" /> class.
    /// </summary> 
    public class UserProfilePocoDaoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserProfile(s) to UserProfile(s). 
        /// </summary>
        public class UserProfilesConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile>, IEnumerable<UserProfile>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserProfile(s) to UserProfile(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserProfile}.</returns>
            public IEnumerable<UserProfile> Convert(IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile> source, IEnumerable<UserProfile> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.AdministrationSchema.UserProfile, UserProfile>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserProfile to UserProfile.
        /// </summary>
        public class UserProfileConverter : ITypeConverter<Dao.Linq2Db.AdministrationSchema.UserProfile, UserProfile>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserProfile to UserProfile.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserProfile.</returns>
            public UserProfile Convert(Dao.Linq2Db.AdministrationSchema.UserProfile source, UserProfile destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserProfile
                {
					Ci = from.Ci,
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
					Udf3 = from.Udf3,
                };
                return item;
            }
        }
		
		#endregion
		
		#region DAO
		
		/// <summary>
        ///     Converts UserProfile(s) to Dao.Linq2Db.AdministrationSchema.UserProfile(s). 
        /// </summary>
        public class UserProfileDaosConverter : ITypeConverter<IEnumerable<UserProfile>, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile>>
        {
            /// <summary>
            ///     Convert UserProfile(s) to Dao.Linq2Db.AdministrationSchema.UserProfile(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.AdministrationSchema.UserProfile}.</returns>
            public IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile> Convert(IEnumerable<UserProfile> source, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserProfile> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserProfile, Dao.Linq2Db.AdministrationSchema.UserProfile>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserProfile to Dao.Linq2Db.AdministrationSchema.UserProfile.
        /// </summary>
        public class UserProfileDaoConverter : ITypeConverter<UserProfile, Dao.Linq2Db.AdministrationSchema.UserProfile>
        {
            /// <summary>
            ///     Convert UserProfile to Dao.Linq2Db.AdministrationSchema.UserProfile.
            /// </summary>
             /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.AdministrationSchema.UserProfile.</returns>
            public Dao.Linq2Db.AdministrationSchema.UserProfile Convert(UserProfile source, Dao.Linq2Db.AdministrationSchema.UserProfile destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.AdministrationSchema.UserProfile
                {
					Ci = from.Ci,
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
					Udf3 = from.Udf3,
                };
                return item;
            }
        }
		
		#endregion
    }
}

