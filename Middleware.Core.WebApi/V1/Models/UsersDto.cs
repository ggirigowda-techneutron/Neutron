#region Copyright TechNeutron © 2019

//
// NAME:			UserDto.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			DTO
//

#endregion


#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Middleware.Core.WebApi.V1.Models
{
    /// <summary>
    ///     Represents the <see cref="UserDto" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserDto
    {

        /// <summary>
        ///     The claims.
        /// </summary>
        public IList<UserClaimDto> Claims { get; set; }

        /// <summary>
        ///     The profile.
        /// </summary>
        public UserProfileDto Profile { get; set; }
   
        /// <summary>
        ///     Creates an instance of <see cref="UserDto" /> class.
        /// </summary>
        public UserDto()
        {
            Claims = new List<UserClaimDto>();
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="UserDto" /> class.
        /// </summary>
		/// <param name="id">The Id.</param>
		/// <param name="userName">The UserName.</param>
		/// <param name="email">The Email.</param>
		/// <param name="emailConfirmed">The EmailConfirmed.</param>
		/// <param name="passwordHash">The PasswordHash.</param>
		/// <param name="securityStamp">The SecurityStamp.</param>
		/// <param name="phoneNumberConfirmed">The PhoneNumberConfirmed.</param>
		/// <param name="twoFactorEnabled">The TwoFactorEnabled.</param>
		/// <param name="lockoutEnabled">The LockoutEnabled.</param>
		/// <param name="accessFailedCount">The AccessFailedCount.</param>
		/// <param name="createdOn">The CreatedOn.</param>
		/// <param name="changedOn">The ChangedOn.</param>
        public UserDto(Guid id, string userName, string email, bool emailConfirmed, string passwordHash, string securityStamp, bool phoneNumberConfirmed, bool twoFactorEnabled, bool lockoutEnabled, int accessFailedCount, DateTime createdOn, DateTime changedOn) : this()
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
        }
        
        /// <summary>
        ///     The Id.
        /// </summary>
	    [Display(Name = "Id")]
		[Required(ErrorMessage = "Id is required")]
		public Guid Id { get; set; }
        
        
        /// <summary>
        ///     The UserName.
        /// </summary>
	    [Display(Name = "UserName")]
		[Required(ErrorMessage = "UserName is required")]
		public string UserName { get; set; }
        
        
        /// <summary>
        ///     The Email.
        /// </summary>
	    [Display(Name = "Email")]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
        
        
        /// <summary>
        ///     The EmailConfirmed.
        /// </summary>
	    [Display(Name = "EmailConfirmed")]
		[Required(ErrorMessage = "EmailConfirmed is required")]
		public bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The Password.
        /// </summary>
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     The PasswordHash.
        /// </summary>
        [Display(Name = "PasswordHash")]
		public string PasswordHash { get; set; }
        
        
        /// <summary>
        ///     The SecurityStamp.
        /// </summary>
	    [Display(Name = "SecurityStamp")]
		public string SecurityStamp { get; set; }
        
        
        /// <summary>
        ///     The PhoneNumber.
        /// </summary>
	    [Display(Name = "PhoneNumber")]
		public string PhoneNumber { get; set; }
        
        
        /// <summary>
        ///     The PhoneNumberConfirmed.
        /// </summary>
	    [Display(Name = "PhoneNumberConfirmed")]
		[Required(ErrorMessage = "PhoneNumberConfirmed is required")]
		public bool PhoneNumberConfirmed { get; set; }
        
        
        /// <summary>
        ///     The MobileNumber.
        /// </summary>
	    [Display(Name = "MobileNumber")]
		public string MobileNumber { get; set; }
        
        
        /// <summary>
        ///     The TwoFactorEnabled.
        /// </summary>
	    [Display(Name = "TwoFactorEnabled")]
		[Required(ErrorMessage = "TwoFactorEnabled is required")]
		public bool TwoFactorEnabled { get; set; }
        
        
        /// <summary>
        ///     The LockoutEndDateUtc.
        /// </summary>
	    [Display(Name = "LockoutEndDateUtc")]
		public DateTime? LockoutEndDateUtc { get; set; }
        
        
        /// <summary>
        ///     The LockoutEnabled.
        /// </summary>
	    [Display(Name = "LockoutEnabled")]
		[Required(ErrorMessage = "LockoutEnabled is required")]
		public bool LockoutEnabled { get; set; }
        
        
        /// <summary>
        ///     The AccessFailedCount.
        /// </summary>
	    [Display(Name = "AccessFailedCount")]
		[Required(ErrorMessage = "AccessFailedCount is required")]
		public int AccessFailedCount { get; set; }
        
        
        /// <summary>
        ///     The CreatedOn.
        /// </summary>
	    [Display(Name = "CreatedOn")]
		[Required(ErrorMessage = "CreatedOn is required")]
		public DateTime CreatedOn { get; set; }
        
        
        /// <summary>
        ///     The ChangedOn.
        /// </summary>
	    [Display(Name = "ChangedOn")]
		public DateTime ChangedOn { get; set; }
        
        
        /// <summary>
        ///     The DeletedOn.
        /// </summary>
	    [Display(Name = "DeletedOn")]
		public DateTime? DeletedOn { get; set; }
        
        
        /// <summary>
        ///     The DeactivatedDate.
        /// </summary>
	    [Display(Name = "DeactivatedDate")]
		public DateTime? DeactivatedDate { get; set; }
        
        
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

