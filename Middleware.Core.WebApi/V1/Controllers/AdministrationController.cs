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
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Classlibrary.Domain.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Middleware.Core.WebApi.V1.Models;
using AutoMapper;
using Classlibrary.Crosscutting.General;
using Classlibrary.Domain.Administration.Notifications;
using Classlibrary.Domain.Administration.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
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
        /// <param name="logger"></param>
        public AdministrationController(IAdministrationManager administrationManager,
            IPasswordHasher<User> passwordStorage, ILogger<AdministrationController> logger, IMediator mediator)
        {
            _administrationManager = administrationManager;
            _passwordStorage = passwordStorage;
            Logger = logger;
            Mediator = mediator;
        }

        /// <summary>
        ///     Get a user.
        /// </summary>
        /// <returns>
        ///     <see cref="User" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpGet("user/{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<User> User(Guid id)
        {
            return await _administrationManager.Get(id);
        }

        /// <summary>
        ///     Get a user (CQRS).
        /// </summary>
        /// <returns>
        ///     <see cref="User" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpGet("cqrs/user/{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<User> CqrsUser(Guid id)
        {
            return await Mediator.Send(new GetUserQuery(id));
        }

        /// <summary>
        ///     Get all the users.
        /// </summary>
        /// <returns>
        ///     <see cref="IEnumerable{User}" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpGet("users")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<User>> Users()
        {
            return await _administrationManager.All();
        }

        /// <summary>
        ///     Get all the users (CQRS).
        /// </summary>
        /// <returns>
        ///     <see cref="IEnumerable{User}" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpGet("cqrs/users")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<User>> CqrsUsers()
        {
            var items = await Mediator.Send(new GetUsersQuery());
            //await Mediator.Publish(new GetUsersNotification());
            return items;
        }

        /// <summary>
        ///     Create user.
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>
        ///     <see cref="Guid" />
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpPost("user/create")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
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
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpPut("user/update")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateUser(UserUpdateDto model)
        {
            if (!ModelState.IsValid || model.Id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            // Ensure the user is either in the admin role or is the user itself
            if (HttpContext.User.HasClaim(
                    claim => claim.Type == Helper.RoleClaimKey && claim.Value == Helper.ClaimAdmin) ||
                HttpContext.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value == model.Id.ToString())
            {
                // Find user
                var found = await _administrationManager.Get(model.Id);
                if (found != null)
                {
                    var item = Mapper.Map<UserUpdateDto, User>(model, found);
                    // NOTE: Removing claims since validation will complain that there are duplicates.
                    item.Claims = new HashSet<UserClaim>();
                    var validation = ValidationCatalog.Validate(item);
                    if (validation.IsValid)
                    {
                        var result = await _administrationManager.Update(item);
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
            }

            return Unauthorized();
        }

        /// <summary>
        ///     Update password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpPut("user/updatepassword")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserPassword(UserUpdatePasswordDto model)
        {
            if (!ModelState.IsValid || model.Id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            // Ensure the user is either in the admin role or is the user itself
            if (HttpContext.User.HasClaim(
                    claim => claim.Type == Helper.RoleClaimKey && claim.Value == Helper.ClaimAdmin) ||
                HttpContext.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value == model.Id.ToString())
            {
                // Find user
                var found = await _administrationManager.Get(model.Id);
                if (found != null)
                {
                    // Ensure that the current password matches 
                    if (_passwordStorage.VerifyHashedPassword(new User(), found.PasswordHash, model.CurrentPassword) ==
                        PasswordVerificationResult.Success)
                    {
                        var result = await _administrationManager.UpdatePassword(model.Id,
                            _passwordStorage.HashPassword(found, model.NewPassword));
                        return new JsonResult(result);
                    }

                    ModelState.AddModelError("CurrentPassword", "Invalid current password");
                    return BadRequest(ModelState);
                }
            }

            return Unauthorized();
        }

        /// <summary>
        ///     Update national Id.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpPut("user/updatenationalid")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateUserNationalId(UserUpdateNationalIdDto model)
        {
            if (!ModelState.IsValid || model.Id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }
            // Find user
            var found = await _administrationManager.Get(model.Id);
            if (found != null)
            {
                // Update
                var result = await _administrationManager.UpdateNationalId(model.Id, model.NationalId, model.NationalIdVerificationDateTimeUtc);
                return new JsonResult(result);
            }
            return Unauthorized();
        }

        /// <summary>
        ///     Delete claim.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpDelete("user/claim/delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteClaim(UserClaimDto model)
        {
            if (!ModelState.IsValid || model.UserId == Guid.Empty)
            {
                return BadRequest(ModelState);
            }
            var item = Mapper.Map<UserClaimDto, UserClaim>(model);
            var result = await _administrationManager.Delete(item);
            return new JsonResult(result);
        }

        /// <summary>
        ///     Create claim.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimAdmin)]
        [HttpPost("user/claim/create")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateClaim(UserClaimDto model)
        {
            if (!ModelState.IsValid || model.UserId == Guid.Empty)
            {
                return BadRequest(ModelState);
            }
            var item = Mapper.Map<UserClaimDto, UserClaim>(model);
            var validation = ValidationCatalog.Validate(item);
            if (validation.IsValid)
            {
                await _administrationManager.Create(model.UserId, new List<UserClaim> { item });
                return new JsonResult(true);
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
        ///     Get user addresses.
        /// </summary>
        /// <param name="id">The user Id.</param>
        /// <returns>
        ///     <see cref="IEnumerable{UserAddressDto}" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpGet("user/addresses/{id}")]
        [ProducesResponseType(typeof(IEnumerable<UserAddressDto>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAddressDto>> UserAddresses(Guid id)
        {
            var items = await _administrationManager.UserAddresses(id);
            return Mapper.Map<IEnumerable<UserAddress>, IEnumerable<UserAddressDto>>(items);
        }

        /// <summary>
        ///     Get user address.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>
        ///     <see cref="UserAddressDto" />.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpGet("user/address/{id}")]
        [ProducesResponseType(typeof(UserAddressDto), (int)HttpStatusCode.OK)]
        public async Task<UserAddressDto> UserAddress(Guid id)
        {
            var item = await _administrationManager.UserAddress(id);
            return Mapper.Map<UserAddress, UserAddressDto>(item);
        }

        /// <summary>
        ///     Create user address.
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>
        ///     <see cref="Guid" />
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpPost("user/address/create")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUserAddress(UserAddressDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = Mapper.Map<UserAddressDto, UserAddress>(model);
            var validation = ValidationCatalog.Validate(item);
            if (validation.IsValid)
            {
                var result = await _administrationManager.Create(model.UserId, item);
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
        ///     Update user address.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///     <see cref="bool"/>.
        /// </returns>
        [Authorize(Roles = Helper.ClaimUser)]
        [HttpPut("user/address/update")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUserAddress(UserAddressDto model)
        {
            if (!ModelState.IsValid || model.Id == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            // Find user
            var found = await _administrationManager.UserAddress(model.Id);
            if (found != null)
            {
                var item = Mapper.Map<UserAddressDto, UserAddress>(model, found);
                var validation = ValidationCatalog.Validate(item);
                if (validation.IsValid)
                {
                    var result = await _administrationManager.Update(item);
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

            return BadRequest("User address not found");
        }
    }
}