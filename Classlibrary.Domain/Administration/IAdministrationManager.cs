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
    }
}