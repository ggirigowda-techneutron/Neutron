#region Copyright Neutron © 2019

//
// NAME:			Linq2DbSettings.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			
//

#endregion

using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using LinqToDB.Configuration;

namespace Middleware.Core.WebApi
{
    /// <summary>
    ///     Represents the <see cref="Linq2DbSettings"/> class.
    /// </summary>
    public class Linq2DbSettings : ILinqToDBSettings
    {
        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        private readonly IOptions<ConnectionStringSettings> _settings;

        /// <summary>
        ///     Creates an instance of <see cref="Linq2DbSettings"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public Linq2DbSettings(IOptions<ConnectionStringSettings> settings)
        {
            _settings = settings;
        }

        /// <summary>
        ///     The data providers.
        /// </summary>
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        /// <summary>
        ///     The default configuration.
        /// </summary>
        public string DefaultConfiguration => "SqlServer";

        /// <summary>
        ///     The default data provider.
        /// </summary>
        public string DefaultDataProvider => "SqlServer";

        /// <summary>
        ///     The  connection strings. 
        /// </summary>
        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = _settings.Value.Name,
                        ProviderName = _settings.Value.ProviderName,
                        ConnectionString = _settings.Value.ConnectionString
                    };
            }
        }
    }
}
