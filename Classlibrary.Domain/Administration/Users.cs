#region Copyright TechNeutron © 2019

//
// NAME:			User.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;
using System.Collections.Generic;
using Classlibrary.Infrastructure;

#endregion

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="User" /> class.
    /// </summary> 
    [Serializable]
    public sealed class User : Entity<Guid>, IAggregateRoot
    {

        /// <summary>
        ///     User claims.
        /// </summary>
        private HashSet<UserClaim> _claims;

        /// <summary>
        ///     Gets or sets the user claims.
        /// </summary>
        public ISet<UserClaim> Claims
        {
            get => _claims;
            set => _claims = (HashSet<UserClaim>)value;
        }

        /// <summary>
        ///     Gets or sets the user profile
        /// </summary>
        public UserProfile Profile { get; set; }

        /// <summary>
        ///     Creates an instance of <see cref="User" /> class.
        /// </summary>
        public User()
        {
            _claims = new HashSet<UserClaim>();
        }


        /// <summary>
        ///     Creates an instance of <see cref="User" /> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="userName">The UserName.</param>
        /// <param name="email">The Email.</param>
        /// <param name="emailConfirmed">The EmailConfirmed.</param>
        /// <param name="passwordHash">The PasswordHash.</param>
        /// <param name="securityStamp">The SecurityStamp.</param>
        /// <param name="phoneNumberConfirmed">The PhoneNumberConfirmed.</param>
        /// <param name="mobileNumberConfirmed">The MobileNumberConfirmed</param>
        /// <param name="twoFactorEnabled">The TwoFactorEnabled.</param>
        /// <param name="lockoutEnabled">The LockoutEnabled.</param>
        /// <param name="accessFailedCount">The AccessFailedCount.</param>
        /// <param name="createdOn">The CreatedOn.</param>
        /// <param name="changedOn">The ChangedOn.</param>
        public User(Guid id, string userName, string email, bool emailConfirmed, string passwordHash, string securityStamp, bool phoneNumberConfirmed, bool mobileNumberConfirmed, bool twoFactorEnabled, bool lockoutEnabled, int accessFailedCount, DateTime createdOn, DateTime changedOn) : this()
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            MobileNumberConfirmed = MobileNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
        }
        
    
        
        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }
       
        /// <summary>
        ///     The UserName.
        /// </summary>
        public string UserName { get; set; }
        
        
        /// <summary>
        ///     The Email.
        /// </summary>
        public string Email { get; set; }
        
        
        /// <summary>
        ///     The EmailConfirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; }
        
        
        /// <summary>
        ///     The PasswordHash.
        /// </summary>
        public string PasswordHash { get; set; }
        
        
        /// <summary>
        ///     The SecurityStamp.
        /// </summary>
        public string SecurityStamp { get; set; }
        
        
        /// <summary>
        ///     The PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }
        
        
        /// <summary>
        ///     The PhoneNumberConfirmed.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        
        
        /// <summary>
        ///     The MobileNumber.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        ///     The MobileNumberConfirmed.
        /// </summary>
        public bool MobileNumberConfirmed { get; set; }

        /// <summary>
        ///     The NationalId.
        /// </summary>
        public string NationalId { get; set; }

        /// <summary>
        ///     The NationalIdVerificationDateUtc.
        /// </summary>
        public DateTime? NationalIdVerificationDateUtc { get; set; }


        /// <summary>
        ///     The TwoFactorEnabled.
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
        
        
        /// <summary>
        ///     The LockoutEndDateUtc.
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }
        
        
        /// <summary>
        ///     The LockoutEnabled.
        /// </summary>
        public bool LockoutEnabled { get; set; }
        
        
        /// <summary>
        ///     The AccessFailedCount.
        /// </summary>
        public int AccessFailedCount { get; set; }
        
        
        /// <summary>
        ///     The CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        
        /// <summary>
        ///     The ChangedOn.
        /// </summary>
        public DateTime ChangedOn { get; set; }
        
        
        /// <summary>
        ///     The DeletedOn.
        /// </summary>
        public DateTime? DeletedOn { get; set; }
        
        
        /// <summary>
        ///     The DeactivatedDate.
        /// </summary>
        public DateTime? DeactivatedDate { get; set; }
        
        
        /// <summary>
        ///     The Udf1.
        /// </summary>
        public string Udf1 { get; set; }
        
        
        /// <summary>
        ///     The Udf2.
        /// </summary>
        public string Udf2 { get; set; }
        
        
        /// <summary>
        ///     The Udf3.
        /// </summary>
        public string Udf3 { get; set; }
        
    }
}

