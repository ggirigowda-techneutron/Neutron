using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Classlibrary.Crosscutting.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.Core.WebApi.V1.Models;

namespace Middleware.Core.WebApi.V1.Controllers
{
    /// <summary>
    /// Sample version REST API
    /// </summary>
    [Authorize(Roles = Helper.ClaimUser)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
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
                    UserName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value
                },
                new ProductModel()
                {
                    Id= Guid.Parse("6648eb0f-0e54-4f6a-93a1-2825e3c8fc9d"),
                    Name = "Product 2",
                    Price = 3.4,
                    UserName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value
                }
            }.ToArray());
        }
    }
}