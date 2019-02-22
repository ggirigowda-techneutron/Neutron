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
        ///     Initializes a new instance of the <see cref="UtilityController" /> controller.
        /// </summary>
        public UtilityController(IUtilityManager utilityManager)
        {
            _utilityManager = utilityManager;
        }

        /// <summary>
        ///     Get a reference,
        /// </summary>
        /// <returns><see cref="Reference"/></returns>
        [HttpGet("reference/{id}")]
        public async Task<Reference> Reference(Guid id)
        {
            return await _utilityManager.Get(id);
        }

        /// <summary>
        ///    Get all the references,
        /// </summary>
        /// <returns><see cref="IEnumerable{Reference}"/></returns>
        [HttpGet("references")]
        public async Task<IEnumerable<Reference>> References()
        {
            return await _utilityManager.All();
        }
    }
}