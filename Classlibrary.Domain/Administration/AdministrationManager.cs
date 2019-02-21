#region Copyright TechNeutron © 2019

//
// NAME:			AdministrationManager.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Administration manager interface
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Classlibrary.Dao.Linq2Db;
using LinqToDB;

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="AdministrationManager" /> class.
    /// </summary>
    public class AdministrationManager : IAdministrationManager
    {
        #region Implementation of IAdministrationManager

        /// <summary>
        ///     Get user.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="User" />.</returns>
        public async Task<User> Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid id", nameof(id));
            using (var db = new PRACTISEV1DB())
            {
                var user = await db.Administration.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                user.UserProfile =
                    await db.Administration.UserProfiles.Where(x => x.UserId == id).FirstOrDefaultAsync();
                user.UserClaims = await db.Administration.UserClaims.Where(x => x.UserId == id).AsQueryable()
                    .ToListAsync();
                return Mapper.Map<AdministrationSchema.User, User>(user);
            }
        }

        /// <summary>
        ///     All users.
        /// </summary>
        /// <returns><see cref="IEnumerable{User}" />.</returns>
        public async Task<IEnumerable<User>> All()
        {
            using (var db = new PRACTISEV1DB())
            {
                var users = await db.Administration.Users.Where(x => x.Id != Guid.Empty).AsQueryable().ToListAsync();
                foreach (var user in users)
                {
                    user.UserProfile =
                        await db.Administration.UserProfiles.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                    user.UserClaims = await db.Administration.UserClaims.Where(x => x.UserId == user.Id).AsQueryable()
                        .ToListAsync();
                }
                return Mapper.Map<IEnumerable<AdministrationSchema.User>, IEnumerable<User>>(users);
            }
        }

        #endregion
    }
}