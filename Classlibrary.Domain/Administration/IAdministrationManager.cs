#region Copyright TechNeutron © 2019

//
// NAME:			IAdministrationManager.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Administration manager interface
//

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="IAdministrationManager" /> interface.
    /// </summary>
    public interface IAdministrationManager
    {
        /// <summary>
        ///     Get user.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="User" />.</returns>
        Task<User> Get(Guid id);

        /// <summary>
        ///     Get user by user name.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        Task<User> Get(string userName);

        /// <summary>
        ///     All users.
        /// </summary>
        /// <returns><see cref="IEnumerable{User}" />.</returns>
        Task<IEnumerable<User>> All();

        /// <summary>
        ///     Create a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<Guid> Create(User user, DependentTransaction transaction = null);

        /// <summary>
        ///     Update a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<bool> Update(User user, DependentTransaction transaction = null);

        /// <summary>
        ///     Update password.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<bool> UpdatePassword(Guid userId, string passwordHash, DependentTransaction transaction = null);

        /// <summary>
        ///     Update national Id.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="nationalId">The national Id.</param>
        /// <param name="nationalIdVerificationDateUtc">The national Id verification date.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<bool> UpdateNationalId(Guid userId, string nationalId, DateTime? nationalIdVerificationDateUtc, DependentTransaction transaction = null);

        /// <summary>
        ///     Create a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<Guid> Create(Guid userId, UserProfile userProfile, DependentTransaction transaction = null);

        /// <summary>
        ///     Update a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<bool> Update(UserProfile userProfile, DependentTransaction transaction = null);

        /// <summary>
        ///     Create a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task Create(Guid userId, IEnumerable<UserClaim> claims, DependentTransaction transaction = null);

        /// <summary>
        ///     Delete a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<bool> Delete(UserClaim userClaim, DependentTransaction transaction = null);

        /// <summary>
        ///     Addresses.
        /// </summary>
        /// <param name="id">The user Id.</param>
        /// <returns><see cref="IEnumerable{Address}" />.</returns>
        Task<IEnumerable<UserAddress>> Addresses(Guid id);

        /// <summary>
        ///     Create a <see cref="Utility.Address"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="address">The address.</param>
        /// <param name="preffered">The preffered.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<Guid> Create(Guid userId, Utility.Address address, bool preffered = false, DependentTransaction transaction = null);
    }
}