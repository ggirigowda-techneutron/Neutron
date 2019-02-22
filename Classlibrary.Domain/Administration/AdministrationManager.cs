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
using System.Transactions;
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
        ///     Get user by user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        public async Task<User> Get(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Invalid user name", nameof(userName));
            using (var db = new PRACTISEV1DB())
            {
                var user = await db.Administration.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
                user.UserProfile = await db.Administration.UserProfiles.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                user.UserClaims = await db.Administration.UserClaims.Where(x => x.UserId == user.Id).AsQueryable().ToListAsync();
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

        /// <summary>
        ///     Create a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<Guid> Create(User user, DependentTransaction transaction = null)
        {
            if(user.Id != Guid.Empty)
                throw new InvalidOperationException("Id has to be empty guid");

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var id = Guid.NewGuid();
                    var result = await db.Administration.Users
                        .Value(c => c.Id, id)
                        .Value(c => c.UserName, user.UserName)
                        .Value(c => c.Email, user.Email)
                        .Value(c => c.PasswordHash, user.PasswordHash)
                        .Value(c => c.SecurityStamp, Guid.NewGuid().ToString())
                        .Value(c => c.PhoneNumber, user.PhoneNumber)
                        .Value(c => c.MobileNumber, user.MobileNumber)
                        .Value(c => c.EmailConfirmed, true)
                        .Value(c => c.PhoneNumberConfirmed, true)
                        .Value(c => c.LockoutEnabled, false)
                        .Value(c => c.AccessFailedCount, 0)
                        .Value(c => c.TwoFactorEnabled, false)
                        .Value(c => c.CreatedOn, DateTime.UtcNow)
                        .InsertAsync();

                    // Create user profile
                    if(user.Profile != null)
                        await Create(id, user.Profile, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                    // Create user claim
                    if(user.Claims != null && user.Claims.Any())
                        await Create(id, user.Claims, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1 ? id : Guid.Empty;
                }
            }
        }

        /// <summary>
        ///     Update a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">Thr transaction.</param>
        /// <returns></returns>
        public async Task<bool> Update(User user, DependentTransaction transaction = null)
        {
            if(user.Id == Guid.Empty)
                throw new InvalidOperationException("Invalid user Id");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.Users.Where(x => x.Id == user.Id)
                        .Set(u => u.Email, user.Email)
                        .Set(u => u.PhoneNumber, user.PhoneNumber)
                        .Set(u => u.MobileNumber, user.MobileNumber)
                        .Set(u => u.ChangedOn, DateTime.UtcNow)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        /// <summary>
        ///     Create a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<Guid> Create(Guid userId, UserProfile userProfile, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    await db.Administration.UserProfiles
                        .Value(c => c.UserId, userId)
                        .Value(c => c.LastName, userProfile.LastName)
                        .Value(c => c.FirstName, userProfile.FirstName)
                        .Value(c => c.UserTypeId, userProfile.UserTypeId)
                        .Value(c => c.GenderId, userProfile.GenderId)
                        .Value(c => c.CountryId, userProfile.CountryId)
                        .InsertAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return userId;
                }
            }
        }

        /// <summary>
        ///     Update a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> Update(UserProfile userProfile, DependentTransaction transaction = null)
        {
            if (userProfile.UserId == Guid.Empty)
                throw new InvalidOperationException("Invalid user Id");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.UserProfiles.Where(x => x.UserId == userProfile.UserId)
                        .Set(u => u.LastName, userProfile.LastName)
                        .Set(u => u.FirstName, userProfile.FirstName)
                        .Set(u => u.UserTypeId, userProfile.UserTypeId)
                        .Set(u => u.GenderId, userProfile.GenderId)
                        .Set(u => u.CountryId, userProfile.CountryId)
                        .UpdateAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }


        /// <summary>
        ///     Create a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task Create(Guid userId, IEnumerable<UserClaim> claims, DependentTransaction transaction = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user Id", nameof(userId));

            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    foreach (var userClaim in claims)
                    {
                        await db.Administration.UserClaims
                            .Value(c => c.UserId, userId)
                            .Value(c => c.ClaimType, userClaim.ClaimType)
                            .Value(c => c.ClaimValue, userClaim.ClaimValue)
                            .InsertAsync();
                    }

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();
                }
            }
        }

        /// <summary>
        ///     Delete a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public async Task<bool> Delete(UserClaim userClaim, DependentTransaction transaction = null)
        {
            if (userClaim.UserId == Guid.Empty || string.IsNullOrEmpty(userClaim.ClaimType) || string.IsNullOrEmpty(userClaim.ClaimValue))
                throw new InvalidOperationException("Invalid claim");
            using (var tx = transaction != null
                ? new TransactionScope(transaction, TransactionScopeAsyncFlowOption.Enabled)
                : new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new PRACTISEV1DB())
                {
                    var result = await db.Administration.UserClaims.Where(x =>
                        x.UserId == userClaim.UserId && x.ClaimType == userClaim.ClaimType &&
                        x.ClaimValue == userClaim.ClaimValue).DeleteAsync();

                    tx.Complete();

                    if (transaction != null)
                        transaction.Complete();

                    return result == 1;
                }
            }
        }

        #endregion
    }
}