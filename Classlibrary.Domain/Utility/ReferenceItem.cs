#region Copyright TechNeutron © 2019

//
// NAME:			ReferenceItem.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			POCO
//

#endregion


#region using

using System;
using Classlibrary.Infrastructure;

#endregion

namespace Classlibrary.Domain.Utility
{
    /// <summary>
    ///     Represents the <see cref="ReferenceItem" /> class.
    /// </summary> 
    [Serializable]
    public sealed class ReferenceItem : ValueObject<ReferenceItem>
    {
   
        /// <summary>
        ///     Creates an instance of <see cref="ReferenceItem" /> class.
        /// </summary>
        public ReferenceItem()
        {
        }

        
        /// <summary>
        ///     Creates an instance of <see cref="ReferenceItem" /> class.
        /// </summary>
		/// <param name="id">The Id.</param>
		/// <param name="referenceId">The ReferenceId.</param>
		/// <param name="code">The Code.</param>
		/// <param name="description">The Description.</param>
		/// <param name="createdOn">The CreatedOn.</param>
		/// <param name="changedOn">The ChangedOn.</param>
        public ReferenceItem(Guid id, Guid referenceId, string code, string description, DateTime createdOn, DateTime changedOn)
        {
            Id = id;
            ReferenceId = referenceId;
            Code = code;
            Description = description;
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
        ///     The ReferenceId.
        /// </summary>
        public Guid ReferenceId { get; set; }
        
        
        /// <summary>
        ///     The Code.
        /// </summary>
        public string Code { get; set; }
        
        
        /// <summary>
        ///     The Description.
        /// </summary>
        public string Description { get; set; }
        
        
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

