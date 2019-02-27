#region Copyright TechNeutron © 2019

//
// NAME:			AddressDto.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/27/2019
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
    ///     Represents the <see cref="UserAddressDto" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserAddressDto
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="UserAddressDto" /> class.
        /// </summary>
        public UserAddressDto()
        {
        }


        /// <summary>
        ///     Creates an instance of <see cref="UserAddressDto" /> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId">The user Id.</param>
        /// <param name="addressId">The address Id.</param>
        /// <param name="address1">The Address1.</param>
        /// <param name="city">The City.</param>
        /// <param name="state">The State.</param>
        /// <param name="zip">The Zip.</param>
        /// <param name="countryId">The CountryId.</param>
        /// <param name="addressTypeId">The AddressTypeId.</param>
        /// <param name="preferred">The preferred</param>
        public UserAddressDto(Guid id, Guid userId, Guid addressId, string address1, string city, string state, string zip, Guid countryId, Guid addressTypeId, bool preferred)
        {
            Id = id;
            UserId = userId;
            AddressId = addressId;
            Address1 = address1;
            City = city;
            State = state;
            Zip = zip;
            CountryId = countryId;
            AddressTypeId = addressTypeId;
            Preferred = preferred;
        }

        /// <summary>
        ///     The Id.
        /// </summary>
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     The UserId.
        /// </summary>
        [Display(Name = "User Id")]
		[Required(ErrorMessage = "User Id is required")]
		public Guid UserId { get; set; }

        /// <summary>
        ///     The AddressId.
        /// </summary>
        [Display(Name = "UserAddress Id")]
        public Guid AddressId { get; set; }

        /// <summary>
        ///     The Address1.
        /// </summary>
        [Display(Name = "Address1")]
		[Required(ErrorMessage = "Address1 is required")]
		public string Address1 { get; set; }
        
        
        /// <summary>
        ///     The Address2.
        /// </summary>
	    [Display(Name = "Address2")]
		public string Address2 { get; set; }
        
        
        /// <summary>
        ///     The City.
        /// </summary>
	    [Display(Name = "City")]
		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }
        
        
        /// <summary>
        ///     The County.
        /// </summary>
	    [Display(Name = "County")]
		public string County { get; set; }
        
        
        /// <summary>
        ///     The State.
        /// </summary>
	    [Display(Name = "State")]
		[Required(ErrorMessage = "State is required")]
		public string State { get; set; }
        
        
        /// <summary>
        ///     The Zip.
        /// </summary>
	    [Display(Name = "Zip")]
		[Required(ErrorMessage = "Zip is required")]
		public string Zip { get; set; }
        
        
        /// <summary>
        ///     The CountryId.
        /// </summary>
	    [Display(Name = "CountryId")]
		[Required(ErrorMessage = "CountryId is required")]
		public Guid CountryId { get; set; }
        
        
        /// <summary>
        ///     The AddressTypeId.
        /// </summary>
	    [Display(Name = "AddressTypeId")]
		[Required(ErrorMessage = "AddressTypeId is required")]
		public Guid AddressTypeId { get; set; }

        /// <summary>
        ///     The Preferred.
        /// </summary>
        [Display(Name = "Preferred")]
        [Required(ErrorMessage = "Preferred is required")]
        public bool Preferred { get; set; }


        /// <summary>
        ///     The Latitude.
        /// </summary>
        [Display(Name = "Latitude")]
		public decimal? Latitude { get; set; }
        
        
        /// <summary>
        ///     The Longitude.
        /// </summary>
	    [Display(Name = "Longitude")]
		public decimal? Longitude { get; set; }
        
        
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
		[Required(ErrorMessage = "ChangedOn is required")]
		public DateTime ChangedOn { get; set; }
        
        
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

