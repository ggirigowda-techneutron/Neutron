#region Copyright Neutron © 2019

//
// NAME:			GetUserQuery.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Query
//

#endregion

using System;
using MediatR;

namespace Classlibrary.Domain.Administration.Queries
{
    /// <summary>
    ///     Represents the <see cref="GetUserQuery" /> class.
    /// </summary>
    public class GetUserQuery : IRequest<User>
    {
        /// <summary>
        ///     Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Creates an instance of the <see cref="GetUserQuery"/>.
        /// </summary>
        /// <param name="id"></param>
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}