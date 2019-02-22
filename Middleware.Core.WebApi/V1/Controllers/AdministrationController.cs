#region Copyright Neutron © 2019

//
// NAME:			AdministrationController.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Controller
//

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Classlibrary.Domain.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Middleware.Core.WebApi.V1.Models;
using AutoMapper;

namespace Middleware.Core.WebApi.V1.Controllers
{
    /// <summary>
    ///     Represents the <see cref="AdministrationController" /> controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        /// <summary>
        ///     The administration manager.
        /// </summary>
        private readonly IAdministrationManager _administrationManager;

        /// <summary>
        ///     The password storage.
        /// </summary>
        private readonly IPasswordHasher<User> _passwordStorage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdministrationController" /> controller.
        /// </summary>
        /// <param name="administrationManager"></param>
        /// <param name="passwordStorage"></param>
        public AdministrationController(IAdministrationManager administrationManager,
            IPasswordHasher<User> passwordStorage)
        {
            _administrationManager = administrationManager;
            _passwordStorage = passwordStorage;
        }

        /// <summary>
        ///     Get a user.
        /// </summary>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        [Authorize(Roles = "USER")]
        [HttpGet("user/{id}")]
        public async Task<User> User(Guid id)
        {
            return await _administrationManager.Get(id);
        }

        /// <summary>
        ///     Get all the users.
        /// </summary>
        /// <returns>
        ///     <see cref="IEnumerable{User}" />
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("users")]
        public async Task<IEnumerable<User>> Users()
        {
            return await _administrationManager.All();
        }

        /// <summary>
        ///     Create user.
        /// </summary>
        /// <returns>
        ///     <see cref="Guid" />
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("user/create")]
        public async Task<IActionResult> CreateUser(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = Mapper.Map<UserDto, User>(model);
            item.CreatedOn = DateTime.UtcNow;
            item.PasswordHash = _passwordStorage.HashPassword(item, model.Password);
            var result = await _administrationManager.Create(item);
            return new JsonResult(result);
        }
    }
}