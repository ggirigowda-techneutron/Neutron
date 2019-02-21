#region Copyright TechNeutron © 2019

//
// NAME:			Reference.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;
using System.Collections.Generic;
using Classlibrary.Infrastructure;

#endregion

namespace Classlibrary.Domain.Utility
{
    /// <summary>
    ///     Represents the <see cref="Reference" /> class.
    /// </summary> 
    [Serializable]
    public sealed class Reference : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        ///     Reference items.
        /// </summary>
        private HashSet<ReferenceItem> _referenceItems;

        /// <summary>
        ///     Gets or sets the reference items.
        /// </summary>
        public ISet<ReferenceItem> ReferenceItems
        {
            get { return _referenceItems; }
            set { _referenceItems = (HashSet<ReferenceItem>)value; }
        }
        
        /// <summary>
        ///     Creates an instance of <see cref="Reference" /> class.
        /// </summary>
        public Reference()
        {
            _referenceItems = new HashSet<ReferenceItem>();
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="Reference" /> class.
        /// </summary>
		/// <param name="id">The Id.</param>
		/// <param name="name">The Name.</param>
		/// <param name="countryCode">The CountryCode.</param>
		/// <param name="createdOn">The CreatedOn.</param>
		/// <param name="changedOn">The ChangedOn.</param>
        public Reference(Guid id, string name, string countryCode, DateTime createdOn, DateTime changedOn) : this()
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
        }
        
        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }
        
        /// <summary>
        ///     The Name.
        /// </summary>
        public string Name { get; set; }
        
        
        /// <summary>
        ///     The Description.
        /// </summary>
        public string Description { get; set; }
        
        
        /// <summary>
        ///     The CountryCode.
        /// </summary>
        public string CountryCode { get; set; }
        
        
        /// <summary>
        ///     The Archived.
        /// </summary>
        public DateTime? Archived { get; set; }
        
        
        /// <summary>
        ///     The CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        
        /// <summary>
        ///     The ChangedOn.
        /// </summary>
        public DateTime ChangedOn { get; set; }
        
    }
}

