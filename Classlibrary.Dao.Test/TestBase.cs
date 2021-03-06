﻿#region Copyright Neutron © 2019

//
// NAME:			TestBase.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Unit test base class
//

#endregion

using System;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Classlibrary.Dao.Test
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
        }
    }
}