#region Copyright TechNeutron © 2019

//
// NAME:			UserUpdateNationalIdDto.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			DTO
//

#endregion

using System;
using System.ComponentModel.DataAnnotations;

namespace Middleware.Core.WebApi.V1.Models
{
    /// <summary>
    ///     Represents the <see cref="UserUpdateNationalIdDto" /> class.
    /// </summary>
    public class UserUpdateNationalIdDto
    {
        /// <summary>
        ///     The user Id.
        /// </summary>
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the national Id.
        /// </summary>
        [Display(Name = "National Id")]
        [Required(ErrorMessage = "National Id is required")]
        public string NationalId { get; set; }

        /// <summary>
        ///     Gets or sets the national Id verification date time UTC
        /// </summary>
        [Display(Name = "National Id Verification Date")]
        public DateTime? NationalIdVerificationDateTimeUtc { get; set; }
    }
}