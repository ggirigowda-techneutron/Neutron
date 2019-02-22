#region Copyright TechNeutron © 2019

//
// NAME:			UserClaimDto.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			DTO
//

#endregion


#region using

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Middleware.Core.WebApi.V1.Models
{
    /// <summary>
    ///     Represents the <see cref="UserClaimDto" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserClaimDto
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="UserClaimDto" /> class.
        /// </summary>
        public UserClaimDto()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="UserClaimDto" /> class.
        /// </summary>
		/// <param name="userId">The UserId.</param>
		/// <param name="claimType">The ClaimType.</param>
		/// <param name="claimValue">The ClaimValue.</param>
        public UserClaimDto(Guid userId, string claimType, string claimValue)
        {
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        
    
        /// <summary>
        ///     The UserId.
        /// </summary>
	    [Display(Name = "UserId")]
		[Required(ErrorMessage = "UserId is required")]
		public Guid UserId { get; set; }
        
        
        /// <summary>
        ///     The ClaimType.
        /// </summary>
	    [Display(Name = "ClaimType")]
		[Required(ErrorMessage = "ClaimType is required")]
		public string ClaimType { get; set; }
        
        
        /// <summary>
        ///     The ClaimValue.
        /// </summary>
	    [Display(Name = "ClaimValue")]
		[Required(ErrorMessage = "ClaimValue is required")]
		public string ClaimValue { get; set; }
        
    }
}

