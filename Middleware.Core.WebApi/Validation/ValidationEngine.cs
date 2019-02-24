#region Copyright TechNeutron © 2019

//
// NAME:			ValidationEngine.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			Validation Engine
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classlibrary.Crosscutting.General;
using Classlibrary.Domain.Administration;
using SpecExpress;
using SpecExpress.Util;

namespace Middleware.Core.WebApi.Validation
{
    /// <summary>
    ///     Represents the <see cref="ValidationEngine" /> class.
    /// </summary>
    public static class ValidationEngine
    {

        /// <summary>
        ///     The administration manager.
        /// </summary>
        public static IAdministrationManager AdministrationManager { set; get; }

        #region Administration

        /// <summary>
        ///     Represents the <see cref="UsersSpecification" /> class.
        /// </summary>
        public class UsersSpecification : Validates<User>
        {
            /// <summary>
            ///     Creates an instance of the <see cref="UsersSpecification" /> class.
            /// </summary>
            public UsersSpecification()
            {
                Check(i => i.UserName).Required("Invalid UserName").LengthBetween(4, 48)
                    .With(x => x.Message = "UserName should be between 6 and 48 characters");
                Check(i => i.PasswordHash).Required("Invalid PasswordHash").MinLength(8)
                    .With(x => x.Message = "Password should have more than 7 characters");
                Check(i => i.Email).Required("Invalid Email").Group(m => m.Matches(RegexPattern.EMAIL));
                Check(i => i.CreatedOn).Required("Invalid Created On").Group(s => s.IsNullOrDefault());
                // Validate username if the user is new
                Check(i => i.UserName)
                    .If(x => x.Id == Guid.Empty)
                    .Required("Invalid username")
                    .Expect((obj, c) =>
                    {
                        var found = Task.Run(async () => await AdministrationManager.Get(c)).Result;
                        if (found != null)
                            return false;
                        return true;
                    }, "Username taken");
                Check(i => i.Claims).Optional().ForEachSpecification();
                Check(i => i.Profile).Required("Invalid Profile").Specification();
            }
        }

        /// <summary>
        ///     Represents the <see cref="UserProfileSpecification"/> class.
        /// </summary>
        public class UserProfileSpecification : Validates<UserProfile>
        {
            /// <summary>
            ///     Creates an instance of <see cref="UserProfileSpecification"/> class.
            /// </summary>
            public UserProfileSpecification()
            {
                Check(i => i.FirstName).Required("Invalid First Name").LengthBetween(2, 48);
                Check(i => i.LastName).Required("Invalid Last Name").LengthBetween(1, 48);
                Check(i => i.UserTypeId).Required("Invalid User Type")
                    .IsInSet(new List<Guid>
                    {
                        Helper.UserTypeEmployee,
                        Helper.UserTypeCustomer
                    });
                Check(i => i.GenderId).Required("Invalid Gender Type")
                    .IsInSet(new List<Guid>()
                    {
                        Helper.GenderTypeFemale,
                        Helper.GenderTypeMale,
                        Helper.GenderTypeUndisclosed
                    });
                Check(i => i.CountryId).Required("Invalid Country")
                    .IsInSet(new List<Guid>
                    {
                        Helper.CountryTypeUnitedStatesofAmerica,
                        Helper.CountryTypeIndia,
                        Helper.CountryTypeGhana
                    });
                Check(i => i.Dob).Required()
                    .GreaterThan(DateTime.UtcNow.AddYears(-100))
                    .With(x => x.Message = $"Date of birth should be greater than {DateTime.UtcNow.AddYears(-100):yyyy MMMM dd}");
                Check(i => i.Dob)
                    .Optional()
                    .Expect((i, c) =>
                    {
                        if (i.UserTypeId == Helper.UserTypeEmployee)
                        {
                            if (i.Dob > DateTime.UtcNow.AddYears(-18))
                                return false;
                            return true;
                        }
                        return true;
                    }, $"User should be over 18 years for employees");
            }
        }

        /// <summary>
        ///    Represents the <see cref="UserClaimSpecification"/> class.
        /// </summary>
        public class UserClaimSpecification : Validates<UserClaim>
        {
            /// <summary>
            ///     Creates an instance of <see cref="UserClaimSpecification"/> class.
            /// </summary>
            public UserClaimSpecification()
            {
                Check(i => i.ClaimType).Required("Invalid Claim Type").Group(s => s.Matches(Helper.RoleClaimKey));
                Check(i => i.ClaimValue).Required("Invalid Claim Value").Group(s => s.Matches(Helper.ClaimAdmin)
                    .Or.Matches(Helper.ClaimUser));
                // Validate claim if the user exists so that there are no duplicates
                Check(i => i.UserId)
                    .If(x => x.UserId != Guid.Empty)
                    .Optional()
                    .Expect((obj, c) =>
                    {
                        var found = Task.Run(async () => await AdministrationManager.Get(c)).Result;
                        if (found != null && found.Claims.FirstOrDefault(x => x.ClaimType == obj.ClaimType && x.ClaimValue == obj.ClaimValue) != null)
                            return false;
                        return true;
                    }, "Claim already exists");
            }
        }

        #endregion
    }
}