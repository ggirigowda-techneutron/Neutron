#region Copyright TechNeutron © 2019

//
// NAME:			UserClaim.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;
using Classlibrary.Infrastructure;

#endregion

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="UserClaim" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserClaim : ValueObject<UserClaim>
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="UserClaim" /> class.
        /// </summary>
        public UserClaim()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="UserClaim" /> class.
        /// </summary>
		/// <param name="userId">The UserId.</param>
		/// <param name="claimType">The ClaimType.</param>
		/// <param name="claimValue">The ClaimValue.</param>
        public UserClaim(Guid userId, string claimType, string claimValue)
        {
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        
    
        
        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }
        
        
        /// <summary>
        ///     The UserId.
        /// </summary>
        public Guid UserId { get; set; }
        
        
        /// <summary>
        ///     The ClaimType.
        /// </summary>
        public string ClaimType { get; set; }
        
        
        /// <summary>
        ///     The ClaimValue.
        /// </summary>
        public string ClaimValue { get; set; }
        
    }
}

