#region Copyright TechNeutron © 2019

//
// NAME:			UserPocoDaoMapper.cs
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
    ///     Represents the <see cref="UserPocoDaoMapper" /> class.
    /// </summary> 
    public class UserPocoDaoMapper
    {
        #region POCO
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.User(s) to User(s). 
        /// </summary>
        public class UsersConverter : ITypeConverter<IEnumerable<Dao.Linq2Db.AdministrationSchema.User>, IEnumerable<User>>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.User(s) to User(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{User}.</returns>
            public IEnumerable<User> Convert(IEnumerable<Dao.Linq2Db.AdministrationSchema.User> source, IEnumerable<User> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<Dao.Linq2Db.AdministrationSchema.User, User>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts Dao.Linq2Db.AdministrationSchema.User to User.
        /// </summary>
        public class UserConverter : ITypeConverter<Dao.Linq2Db.AdministrationSchema.User, User>
        {
            /// <summary>
            ///     Convert Dao.Linq2Db.AdministrationSchema.User to User.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>User.</returns>
            public User Convert(Dao.Linq2Db.AdministrationSchema.User source, User destination, ResolutionContext context)
            {
                var from = source;
                var item = new User
                {
					Ci = from.Ci,
					Id = from.Id,
					UserName = from.UserName,
					Email = from.Email,
					EmailConfirmed = from.EmailConfirmed,
					PasswordHash = from.PasswordHash,
					SecurityStamp = from.SecurityStamp,
					PhoneNumber = from.PhoneNumber,
					PhoneNumberConfirmed = from.PhoneNumberConfirmed,
					MobileNumber = from.MobileNumber,
                    MobileNumberConfirmed = from.MobileNumberConfirmed,
                    NationalId = from.NationalId,
                    NationalIdVerificationDateUtc = from.NationalIdVerificationDateUtc,
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
                    Profile = from.UserProfile != null ? Mapper.Map<Dao.Linq2Db.AdministrationSchema.UserProfile, UserProfile>(from.UserProfile) : null,
                    Claims = from.UserClaims != null && from.UserClaims.Any() ? Mapper.Map<IEnumerable<Dao.Linq2Db.AdministrationSchema.UserClaim>, IEnumerable<UserClaim>>(from.UserClaims).ToHashSet() : new HashSet<UserClaim>()
                };
                return item;
            }
        }
		
		#endregion
		
		#region DAO
		
		/// <summary>
        ///     Converts User(s) to Dao.Linq2Db.AdministrationSchema.User(s). 
        /// </summary>
        public class UserDaosConverter : ITypeConverter<IEnumerable<User>, IEnumerable<Dao.Linq2Db.AdministrationSchema.User>>
        {
            /// <summary>
            ///     Convert User(s) to Dao.Linq2Db.AdministrationSchema.User(s).
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>IEnumerable{Dao.Linq2Db.AdministrationSchema.User}.</returns>
            public IEnumerable<Dao.Linq2Db.AdministrationSchema.User> Convert(IEnumerable<User> source, IEnumerable<Dao.Linq2Db.AdministrationSchema.User> destination, ResolutionContext context)
            {
                var from = source;
                return from.Select(Mapper.Map<User, Dao.Linq2Db.AdministrationSchema.User>).ToList();
            }
        }
		
		/// <summary>
        ///     Converts User to Dao.Linq2Db.AdministrationSchema.User.
        /// </summary>
        public class UserDaoConverter : ITypeConverter<User, Dao.Linq2Db.AdministrationSchema.User>
        {
            /// <summary>
            ///     Convert User to Dao.Linq2Db.AdministrationSchema.User.
            /// </summary>
             /// <param name="source">The source.</param>
            /// <param name="destination">The destination.</param>
            /// <param name="context">The context.</param>
            /// <returns>Dao.Linq2Db.AdministrationSchema.User.</returns>
            public Dao.Linq2Db.AdministrationSchema.User Convert(User source, Dao.Linq2Db.AdministrationSchema.User destination, ResolutionContext context)
            {
                var from = source;
                var item = new Dao.Linq2Db.AdministrationSchema.User
                {
					Ci = from.Ci,
					Id = from.Id,
					UserName = from.UserName,
					Email = from.Email,
					EmailConfirmed = from.EmailConfirmed,
					PasswordHash = from.PasswordHash,
					SecurityStamp = from.SecurityStamp,
					PhoneNumber = from.PhoneNumber,
					PhoneNumberConfirmed = from.PhoneNumberConfirmed,
					MobileNumber = from.MobileNumber,
                    MobileNumberConfirmed = from.MobileNumberConfirmed,
                    NationalId = from.NationalId,
                    NationalIdVerificationDateUtc = from.NationalIdVerificationDateUtc,
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
                };
                return item;
            }
        }
		
		#endregion
    }
}

