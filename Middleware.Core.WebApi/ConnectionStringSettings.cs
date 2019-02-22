#region Copyright Neutron © 2019

//
// NAME:			ConnectionStringSettings.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			
//

#endregion

using LinqToDB.Configuration;

namespace Middleware.Core.WebApi
{
    /// <summary>
    ///     Represents the <see cref="ConnectionStringSettings" /> class.
    /// </summary>
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        /// <summary>
        ///     The connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     The Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The Provider.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        ///     The is global?
        /// </summary>
        public bool IsGlobal => false;
    }
}