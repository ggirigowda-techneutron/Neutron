#region Copyright TechNeutron © 2019

//
// NAME:			UserClaimPocoDaoMapper.cs
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
    ///     Represents the <see cref="UserClaimPocoDaoMapper" /> class.
    /// </summary> 
    public class UserClaimPocoDaoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserClaim(s) to UserClaim(s). 
        /// </summary>
        public class UserClaimsConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim>, IEnumerable<UserClaim>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserClaim(s) to UserClaim(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{UserClaim}.</returns>
            public IEnumerable<UserClaim> Convert(IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim> source, IEnumerable<UserClaim> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.AdministrationSchema.UserClaim, UserClaim>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.UserClaim to UserClaim.
        /// </summary>
        public class UserClaimConverter : ITypeConverter<Dao.Linq2Db.AdministrationSchema.UserClaim, UserClaim>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.UserClaim to UserClaim.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>UserClaim.</returns>
            public UserClaim Convert(Dao.Linq2Db.AdministrationSchema.UserClaim source, UserClaim destination, ResolutionContext context)
            {
                var from = source;
                var item = new UserClaim
                {
					Ci = from.Ci,
					UserId = from.UserId,
					ClaimType = from.ClaimType,
					ClaimValue = from.ClaimValue,
                };
                return item;
            }
        }
		
		#endregion
		
		#region DAO
		
		/// <summary>
        ///     Converts UserClaim(s) to Dao.Linq2Db.AdministrationSchema.UserClaim(s). 
        /// </summary>
        public class UserClaimDaosConverter : ITypeConverter<IEnumerable<UserClaim>, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim>>
        {
            /// <summary>
            ///     Convert UserClaim(s) to Dao.Linq2Db.AdministrationSchema.UserClaim(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.AdministrationSchema.UserClaim}.</returns>
            public IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim> Convert(IEnumerable<UserClaim> source, IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<UserClaim, Dao.Linq2Db.AdministrationSchema.UserClaim>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts UserClaim to Dao.Linq2Db.AdministrationSchema.UserClaim.
        /// </summary>
        public class UserClaimDaoConverter : ITypeConverter<UserClaim, Dao.Linq2Db.AdministrationSchema.UserClaim>
        {
            /// <summary>
            ///     Convert UserClaim to Dao.Linq2Db.AdministrationSchema.UserClaim.
            /// </summary>
             /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.AdministrationSchema.UserClaim.</returns>
            public Dao.Linq2Db.AdministrationSchema.UserClaim Convert(UserClaim source, Dao.Linq2Db.AdministrationSchema.UserClaim destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.AdministrationSchema.UserClaim
                {
					Ci = from.Ci,
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

