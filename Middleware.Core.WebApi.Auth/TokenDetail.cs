#region Copyright Neutron © 2019

//
// NAME:			TokenDetail.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			
//

#endregion

namespace Middleware.Core.WebApi.Auth
{
    /// <summary>
    ///     Represents the token details for authorization.
    /// </summary>
    public class TokenDetail
    {
        /// <summary>
        ///     Gets or sets the secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        ///     Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        ///     Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }
    }
}