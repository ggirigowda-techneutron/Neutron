#region Copyright Neutron © 2019

//
// NAME:			GetUsersQuery.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Query
//

#endregion

using System.Collections.Generic;
using MediatR;

namespace Classlibrary.Domain.Administration.Queries
{
    /// <summary>
    ///     Represents the <see cref="GetUsersQuery"/> class.
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}