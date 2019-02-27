#region Copyright TechNeutron © 2019

//
// NAME:			Address.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/27/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;

#endregion

namespace Classlibrary.Domain.Utility
{
    /// <summary>
    ///     Represents the <see cref="Address" /> class.
    /// </summary> 
    [Serializable]
    public sealed class Address
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="Address" /> class.
        /// </summary>
        public Address()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="Address" /> class.
        /// </summary>
		/// <param name="id">The Id.</param>
		/// <param name="address1">The Address1.</param>
		/// <param name="city">The City.</param>
		/// <param name="state">The State.</param>
		/// <param name="zip">The Zip.</param>
		/// <param name="countryId">The CountryId.</param>
		/// <param name="addressTypeId">The AddressTypeId.</param>
		/// <param name="createdOn">The CreatedOn.</param>
		/// <param name="changedOn">The ChangedOn.</param>
        public Address(Guid id, string address1, string city, string state, string zip, Guid countryId, Guid addressTypeId, DateTime createdOn, DateTime changedOn)
        {
            Id = id;
            Address1 = address1;
            City = city;
            State = state;
            Zip = zip;
            CountryId = countryId;
            AddressTypeId = addressTypeId;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
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
        ///     The Address1.
        /// </summary>
        public string Address1 { get; set; }
        
        
        /// <summary>
        ///     The Address2.
        /// </summary>
        public string Address2 { get; set; }
        
        
        /// <summary>
        ///     The City.
        /// </summary>
        public string City { get; set; }
        
        
        /// <summary>
        ///     The County.
        /// </summary>
        public string County { get; set; }
        
        
        /// <summary>
        ///     The State.
        /// </summary>
        public string State { get; set; }
        
        
        /// <summary>
        ///     The Zip.
        /// </summary>
        public string Zip { get; set; }
        
        
        /// <summary>
        ///     The CountryId.
        /// </summary>
        public Guid CountryId { get; set; }
        
        
        /// <summary>
        ///     The AddressTypeId.
        /// </summary>
        public Guid AddressTypeId { get; set; }
        
        
        /// <summary>
        ///     The Latitude.
        /// </summary>
        public decimal? Latitude { get; set; }
        
        
        /// <summary>
        ///     The Longitude.
        /// </summary>
        public decimal? Longitude { get; set; }
        
        
        /// <summary>
        ///     The CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        
        /// <summary>
        ///     The ChangedOn.
        /// </summary>
        public DateTime ChangedOn { get; set; }
        
        
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

