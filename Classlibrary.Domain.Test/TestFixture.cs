#region Copyright TechNeutron © 2019

//
// NAME:			TestFixture.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Test fixture
//

#endregion

using AutoMapper;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents a <see cref="TestFixture" /> class.
    /// </summary>
    public class TestFixture
    {
        /// <summary>
        ///     Creates an instance of <see cref="TestFixture" /> class.
        /// </summary>
        public TestFixture()
        {
            // Can also use multiple assembly names:
            Mapper.Initialize(cfg =>
                cfg.AddProfiles("Classlibrary.Domain")
            );
        }
    }
}