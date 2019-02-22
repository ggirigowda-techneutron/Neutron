#region Copyright Neutron © 2019

//
// NAME:			UtilityController.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Controller
//

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Classlibrary.Domain.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Core.WebApi.V1.Controllers
{
    /// <summary>
    ///     Represents the <see cref="UtilityController" /> controller.
    /// </summary>
    [Authorize(Roles = "USER")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        /// <summary>
        ///     The utility manager.
        /// </summary>
        private readonly IUtilityManager _utilityManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref=" UtilityController" /> controller.
        /// </summary>
        public UtilityController()
        {
            _utilityManager = new UtilityManager();
        }

        /// <summary>
        ///     Retrieve a reference
        /// </summary>
        /// <returns>Collection of ProductModel instances</returns>
        [HttpGet("reference/{id}")]
        public async Task<Reference> Reference(Guid id)
        {
            return await _utilityManager.Get(id);
        }

        /// <summary>
        ///     Retrieve all the references
        /// </summary>
        /// <returns>Collection of ProductModel instances</returns>
        [HttpGet("references")]
        public async Task<IEnumerable<Reference>> References()
        {
            return await _utilityManager.All();
        }
    }
}