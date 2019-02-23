#region Copyright TechNeutron © 2019

//
// NAME:			UserUpdateDto.cs
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
    ///     Represents the <see cref="UserUpdateDto" /> class.
    /// </summary>
    [Serializable]
    public sealed class UserUpdateDto
    {
        /// <summary>
        ///     The Id.
        /// </summary>
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        /// <summary>
        ///     The Email.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        ///     The PhoneNumber.
        /// </summary>
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     The MobileNumber.
        /// </summary>
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        ///     The FirstName.
        /// </summary>
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }


        /// <summary>
        ///     The LastName.
        /// </summary>
        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }


        /// <summary>
        ///     The UserTypeId.
        /// </summary>
        [Display(Name = "UserTypeId")]
        [Required(ErrorMessage = "UserTypeId is required")]
        public Guid UserTypeId { get; set; }


        /// <summary>
        ///     The Title.
        /// </summary>
        [Display(Name = "Title")]
        public string Title { get; set; }


        /// <summary>
        ///     The Suffix.
        /// </summary>
        [Display(Name = "Suffix")]
        public string Suffix { get; set; }


        /// <summary>
        ///     The Prefix.
        /// </summary>
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }


        /// <summary>
        ///     The PrefferedName.
        /// </summary>
        [Display(Name = "PrefferedName")]
        public string PrefferedName { get; set; }


        /// <summary>
        ///     The Dob.
        /// </summary>
        [Display(Name = "Dob")]
        public DateTime? Dob { get; set; }


        /// <summary>
        ///     The GenderId.
        /// </summary>
        [Display(Name = "GenderId")]
        [Required(ErrorMessage = "GenderId is required")]
        public Guid GenderId { get; set; }


        /// <summary>
        ///     The CountryId.
        /// </summary>
        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "CountryId is required")]
        public Guid CountryId { get; set; }


        /// <summary>
        ///     The Organization.
        /// </summary>
        [Display(Name = "Organization")]
        public string Organization { get; set; }


        /// <summary>
        ///     The Department.
        /// </summary>
        [Display(Name = "Department")]
        public string Department { get; set; }


        /// <summary>
        ///     The PictureUrl.
        /// </summary>
        [Display(Name = "PictureUrl")]
        public string PictureUrl { get; set; }

        /// <summary>
        ///     The Udf1.
        /// </summary>
        [Display(Name = "Udf1")]
        public string Udf1 { get; set; }

        /// <summary>
        ///     The Udf2.
        /// </summary>
        [Display(Name = "Udf2")]
        public string Udf2 { get; set; }

        /// <summary>
        ///     The Udf3.
        /// </summary>
        [Display(Name = "Udf3")]
        public string Udf3 { get; set; }
    }
}