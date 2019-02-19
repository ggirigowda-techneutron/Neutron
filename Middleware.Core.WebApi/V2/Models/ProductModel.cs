﻿using System;

namespace Middleware.Core.WebApi.V2.Models
{
    /// <summary>
    /// Sample product model 
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descriptive name of the product
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Product brief description
        /// </summary>
        public String Description { get; set; }
    }
}
