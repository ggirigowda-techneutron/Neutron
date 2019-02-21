#region Copyright TechNeutron © 2019

//
// NAME:			IReferenceManager.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Reference manager interface
//

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classlibrary.Domain.Utility
{
    /// <summary>
    ///     Represents the <see cref="IReferenceManager" /> interface.
    /// </summary>
    public interface IReferenceManager
    {
        /// <summary>
        ///     Get reference.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="Reference" />.</returns>
        Task<Reference> Get(Guid id);

        /// <summary>
        ///     All references.
        /// </summary>
        /// <returns><see cref="IEnumerable{Reference}" />.</returns>
        Task<IEnumerable<Reference>> All();
    }
}