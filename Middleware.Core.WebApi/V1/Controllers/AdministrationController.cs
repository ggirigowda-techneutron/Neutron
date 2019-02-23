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
using SpecExpress;

namespace Middleware.Core.WebApi.V1.Controllers
{
    /// <summary>
    ///     Represents the <see cref="AdministrationController" /> controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AdministrationController : BaseController
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
        /// <param name="model">The model</param>
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
            var validation = ValidationCatalog.Validate(item);
            if (validation.IsValid)
            {
                var result = await _administrationManager.Create(item);
                return new JsonResult(result);
            }
            // Add the errors
            foreach (var error in validation.Errors)
            {
                foreach (var allErrorMessage in error.AllErrorMessages())
                {
                    ModelState.AddModelError("Error(s): ", allErrorMessage);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///     Update user.
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>
        ///     <see cref="bool"/>
        /// </returns>
        [Authorize(Roles = "USER")]
        [HttpPut("user/update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto model)
        {
            if (!ModelState.IsValid || model.Id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            // Find user
            var found = await _administrationManager.Get(model.Id);
            if (found != null)
            {
                Mapper.Map<UserUpdateDto, User>(model, found);
                var validation = ValidationCatalog.Validate(found);
                if (validation.IsValid)
                {
                    var result = await _administrationManager.Update(found);
                    return new JsonResult(result);
                }
                // Add the errors
                foreach (var error in validation.Errors)
                {
                    foreach (var allErrorMessage in error.AllErrorMessages())
                    {
                        ModelState.AddModelError("Error(s): ", allErrorMessage);
                    }
                }
            }

            return BadRequest(ModelState);
        }
    }
}