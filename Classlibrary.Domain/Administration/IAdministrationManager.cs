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
        ///     Create a <see cref="UserProfile"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task<Guid> Create(Guid userId, UserProfile userProfile, DependentTransaction transaction = null);

        /// <summary>
        ///     Create a <see cref="UserClaim"/>.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        Task Create(Guid userId, IEnumerable<UserClaim> claims, DependentTransaction transaction = null);
    }
}