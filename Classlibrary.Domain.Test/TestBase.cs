#region Copyright Neutron © 2019

//
// NAME:			TestBase.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit test base class
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents the <see cref="TestBase" /> abstract class.
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        ///     Test runner output.
        /// </summary>
        protected readonly ITestOutputHelper Output;

        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        protected string ConnectionString { get; set; }

        /// <summary>
        ///     Creates an instance of <see cref="TestBase" /> class.
        /// </summary>
        /// <param name="output">The output</param>
        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
            // Set the connection string
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config["Data:DefaultConnection:ConnectionString"];
            DataConnection.DefaultSettings = new Linq2DbSettings(ConnectionString);
        }
    }

    /// <summary>
    ///     Represents the <see cref="ConnectionStringSettings"/> class.
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

    /// <summary>
    ///     Represents the <see cref="Linq2DbSettings"/> class.
    /// </summary>
    public class Linq2DbSettings : ILinqToDBSettings
    {
        /// <summary>
        ///     The connection string. 
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        ///     Creates an instance of <see cref="Linq2DbSettings"/> class.
        /// </summary>
        /// <param name="connectionString"></param>
        public Linq2DbSettings(string connectionString)
        {
            _connectionString = connectionString;
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
                        Name = "SqlServer",
                        ProviderName = "SqlServer",
                        ConnectionString = _connectionString 
                    };
            }
        }
    }
}