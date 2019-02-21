#region Copyright TechNeutron © 2019

//
// NAME:			UserProfile.cs
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
    ///     Represents the <see cref="UserProfile" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserProfile : ValueObject<UserProfile>
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="UserProfile" /> class.
        /// </summary>
        public UserProfile()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="UserProfile" /> class.
        /// </summary>
		/// <param name="userId">The UserId.</param>
		/// <param name="firstName">The FirstName.</param>
		/// <param name="lastName">The LastName.</param>
		/// <param name="userTypeId">The UserTypeId.</param>
		/// <param name="genderId">The GenderId.</param>
		/// <param name="countryId">The CountryId.</param>
        public UserProfile(Guid userId, string firstName, string lastName, Guid userTypeId, Guid genderId, Guid countryId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserTypeId = userTypeId;
            GenderId = genderId;
            CountryId = countryId;
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
        ///     The FirstName.
        /// </summary>
        public string FirstName { get; set; }
        
        
        /// <summary>
        ///     The LastName.
        /// </summary>
        public string LastName { get; set; }
        
        
        /// <summary>
        ///     The UserTypeId.
        /// </summary>
        public Guid UserTypeId { get; set; }
        
        
        /// <summary>
        ///     The Title.
        /// </summary>
        public string Title { get; set; }
        
        
        /// <summary>
        ///     The Suffix.
        /// </summary>
        public string Suffix { get; set; }
        
        
        /// <summary>
        ///     The Prefix.
        /// </summary>
        public string Prefix { get; set; }
        
        
        /// <summary>
        ///     The PrefferedName.
        /// </summary>
        public string PrefferedName { get; set; }
        
        
        /// <summary>
        ///     The Dob.
        /// </summary>
        public DateTime? Dob { get; set; }
        
        
        /// <summary>
        ///     The GenderId.
        /// </summary>
        public Guid GenderId { get; set; }
        
        
        /// <summary>
        ///     The CountryId.
        /// </summary>
        public Guid CountryId { get; set; }
        
        
        /// <summary>
        ///     The Organization.
        /// </summary>
        public string Organization { get; set; }
        
        
        /// <summary>
        ///     The Department.
        /// </summary>
        public string Department { get; set; }
        
        
        /// <summary>
        ///     The PictureUrl.
        /// </summary>
        public string PictureUrl { get; set; }
        
        
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

