#region Copyright Neutron © 2019

//
// NAME:			GetUsersNotification.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			CQRS 
//

#endregion

using MediatR;

namespace Classlibrary.Domain.Administration.Notifications
{
    /// <summary>
    ///     Represents the <see cref="GetUsersNotification" /> class.
    /// </summary>
    public class GetUsersNotification : INotification
    {
        /// <summary>
        ///     FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Create an instance  of <see cref="GetUsersNotification"/>.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public GetUsersNotification(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}