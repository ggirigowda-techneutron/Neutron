#region Copyright Neutron © 2019

//
// NAME:			AuthController.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Controller
//

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Classlibrary.Crosscutting.Security;
using Classlibrary.Domain.Administration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Middleware.Core.WebApi.Auth.Dto;

namespace Middleware.Core.WebApi.Auth.Controllers
{
    /// <summary>
    ///     Represents the authorization controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        private readonly IOptions<TokenDetail> _settings;

        /// <summary>
        ///     The administration manager.
        /// </summary>
        private readonly IAdministrationManager _administrationManager;

        /// <summary>
        ///     The password storage.
        /// </summary>
        private readonly PasswordStorage<User> _passwordStorage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public AuthController(IOptions<TokenDetail> settings)
        {
            _settings = settings;
            _administrationManager = new AdministrationManager();
            _passwordStorage = new PasswordStorage<User>();
        }

        /// <summary>
        ///     Authorize.
        /// </summary>
        /// <param name="model">The <see cref="AuthenticationDto"/> model.</param>
        /// <returns>The token on success or empty result on failure.</returns>
        /// POST /api/auth/{AUTHENTICATIONDTO}
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] AuthenticationDto model)
        {
            var found = await _administrationManager.Get(model.UserName);
            if (found != null && found.DeactivatedDate == null &&
                _passwordStorage.VerifyHashedPassword(new User(), found.PasswordHash, model.Password) ==
                PasswordVerificationResult.Success)
            {
                var now = DateTime.UtcNow;

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, found.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                };
                claims.AddRange(found.Claims.Select(foundClaim => new Claim(foundClaim.ClaimType, foundClaim.ClaimValue)));

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));

                var jwt = new JwtSecurityToken(
                    _settings.Value.Issuer,
                    _settings.Value.Audience,
                    claims,
                    now,
                    now.Add(TimeSpan.FromMinutes(2)),
                    new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
                };
                return new JsonResult(responseJson);
            }
            return new JsonResult(string.Empty);
        }
    }
}