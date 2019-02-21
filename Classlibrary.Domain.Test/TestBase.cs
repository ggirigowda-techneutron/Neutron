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
            DataConnection.DefaultSettings = new Linq2DbSettings();
        }
    }

    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class Linq2DbSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "SqlServer",
                        ProviderName = "SqlServer",
                        ConnectionString = @"Server=tcp:healthneutron-dev.database.windows.net,1433;Initial Catalog=PRACTISEV1;Persist Security Info=False;User ID=ggirigowda;Password=testdb99!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    };
            }
        }
    }
}