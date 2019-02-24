#region Copyright TechNeutron © 2019

//
// NAME:			UserUpdatePasswordDto.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			DTO
//

#endregion

using System;
using System.ComponentModel.DataAnnotations;
using Classlibrary.Crosscutting.General;

namespace Middleware.Core.WebApi.V1.Models
{
    /// <summary>
    ///     Represents the <see cref="UserUpdatePasswordDto" /> class.
    /// </summary>
    public class UserUpdatePasswordDto
    {
        /// <summary>
        ///     The user Id.
        /// </summary>
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        /// <summary>
        ///     The current password.
        /// </summary>
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        /// <summary>
        ///     The new password.
        /// </summary>
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(RegexPattern.STRONG_PASSWORD, ErrorMessage = "Invalid new password")]
        public string NewPassword { get; set; }
    }
}