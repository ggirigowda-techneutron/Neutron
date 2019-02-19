namespace Middleware.Core.WebApi.Auth.Dto
{
    /// <summary>
    ///     Represents the authentication DTO.
    /// </summary>
    public class AuthenticationDto
    {
        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}