#region Copyright TechNeutron © 2019

//
// NAME:			Helper.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			
//

#endregion

using System;

namespace Classlibrary.Crosscutting.General
{
    /// <summary>
    ///     Represents the <see cref="Helper" /> class.
    /// </summary>
    public class Helper
    {
        /// <summary>
        ///     Countries.
        /// </summary>
        public static readonly Guid CountryTypeUnitedStatesofAmerica = Guid.Parse("2af6ff6c-8bb8-46f0-b27e-81def1b76b64");
        public static readonly Guid CountryTypeIndia = Guid.Parse("c78227a5-ca89-4b9d-aa6a-e5d779b94b20");
        public static readonly Guid CountryTypeGhana = Guid.Parse("70efb18e-b531-4acd-a784-70e91ee89d4c");

        /// <summary>
        ///     Genders.
        /// </summary>
        public static readonly Guid GenderTypeMale = Guid.Parse("8a29a4ab-62a7-4a06-b2fa-46a40f449a84");
        public static readonly Guid GenderTypeFemale = Guid.Parse("4aa1d4e0-6162-470b-b0d9-a569e482c5c0");
        public static readonly Guid GenderTypeUndisclosed = Guid.Parse("734997e9-e0c4-4e0e-86ef-ff3471ec6b05");

        /// <summary>
        ///     User Types.
        /// </summary>
        public static readonly Guid UserTypeEmployee = Guid.Parse("e21dcb05-1f7b-4c95-9c29-0e583b120e44");
        public static readonly Guid UserTypeCustomer = Guid.Parse("5ebf5cca-df92-49c6-ae5f-f3c9670bf9d3");

        /// <summary>
        ///     Address Types
        /// </summary>
        public static readonly Guid AddressTypeMailing = Guid.Parse("e0e08fcd-a1e3-4810-ab49-7f49124b52d3");
        public static readonly Guid AddressTypeBilling = Guid.Parse("781469bf-8815-478c-b1ef-8baf06149f07");
        public static readonly Guid AddressTypeShipping = Guid.Parse("89917168-ff35-4619-a500-632410868499");
        public static readonly Guid AddressTypeHome = Guid.Parse("9f131320-420b-43cc-af22-0d60400fe8dd");

        /// <summary>
        ///     The role claim key.
        /// </summary>
        public const string RoleClaimKey = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

        /// <summary>
        ///     The claims types.
        /// </summary>
        public const string ClaimUser = "USER";
        public const string ClaimAdmin = "ADMIN";
    }
}