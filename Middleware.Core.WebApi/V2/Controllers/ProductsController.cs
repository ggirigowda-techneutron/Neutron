﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Middleware.Core.WebApi.V2.Models;

namespace Middleware.Core.WebApi.V2.Controllers
{
    /// <summary>
    /// Sample versioning REST API
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Retrieve all the products
        /// </summary>
        /// <returns>Collection of ProductModel instances</returns>
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await Task.FromResult<IEnumerable<ProductModel>>(new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id= Guid.Parse("6fab57fb-0c61-4552-9490-9161c2466e62"),
                    Name = "Product 1",
                    Price = 2.3,
                    Description = "This is product 1 descritpion"
                },
                new ProductModel()
                {
                    Id= Guid.Parse("6648eb0f-0e54-4f6a-93a1-2825e3c8fc9d"),
                    Name = "Product 2",
                    Price = 3.4,
                    Description = "This is product 2 descritpion"
                }
            }.ToArray());
        }
    }
}