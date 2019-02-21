#region Copyright TechNeutron © 2019

//
// NAME:			TestCollectionDefinition.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			Test collection definition
//

#endregion

using Xunit;

namespace Classlibrary.Domain.Test
{
    /// <summary>
    ///     Represents the <see cref="TestCollectionDefinition" /> class.
    /// </summary>
    [CollectionDefinition("TestCollection")]
    public class TestCollectionDefinition : ICollectionFixture<TestFixture>
    {
        //Nothing needed here
    }
}