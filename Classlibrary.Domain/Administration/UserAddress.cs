#region Copyright TechNeutron © 2019

//
// NAME:			UserAddress.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/27/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;
using Classlibrary.Domain.Utility;

#endregion

namespace Classlibrary.Domain.Administration
{
    /// <summary>
    ///     Represents the <see cref="UserAddress" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserAddress
    {

        /// <summary>
        ///     Gets or sets the address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     Creates an instance of <see cref="UserAddress" /> class.
        /// </summary>
        public UserAddress()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="UserAddress" /> class.
        /// </summary>
		/// <param name="id">The Id.</param>
		/// <param name="userId">The UserId.</param>
		/// <param name="addressId">The AddressId.</param>
		/// <param name="preffered">The Preffered.</param>
        public UserAddress(Guid id, Guid userId, Guid addressId, bool preffered)
        {
            Id = id;
            UserId = userId;
            AddressId = addressId;
            Preffered = preffered;
        }
        
    
        
        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }
        
        
        /// <summary>
        ///     The Id.
        /// </summary>
        public Guid Id { get; set; }
        
        
        /// <summary>
        ///     The UserId.
        /// </summary>
        public Guid UserId { get; set; }
        
        
        /// <summary>
        ///     The AddressId.
        /// </summary>
        public Guid AddressId { get; set; }
        
        
        /// <summary>
        ///     The Preffered.
        /// </summary>
        public bool Preffered { get; set; }
        
    }
}

