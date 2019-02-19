using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        ///     Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public AuthController(IOptions<TokenDetail> settings)
        {
            _settings = settings;
        }

        /// <summary>
        ///     Authorize.
        /// </summary>
        /// <param name="model">The <see cref="AuthenticationDto"/> model.</param>
        /// <returns>The token on success or empty result on failure.</returns>
        /// POST /api/auth/{AUTHENTICATIONDTO}
        [HttpPost]
        public IActionResult Authorize([FromBody] AuthenticationDto model)
        {
            if (model.UserName == "girish66" && model.Password == "testdb99!!")
            {
                var now = DateTime.UtcNow;

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                    new Claim("Role", "Admin"), 
                };

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