#region Copyright Neutron © 2019

//
// NAME:			GetUsersQueryHandler.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Handler
//

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Classlibrary.Dao.Linq2Db;
using LinqToDB;
using MediatR;

namespace Classlibrary.Domain.Administration.Queries
{
    /// <summary>
    ///     Represents the <see cref="GetUsersQueryHandler" /> class.
    /// </summary>
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        #region Implementation of IRequestHandler<in GetUsersQuery,List<User>>

        /// <summary>
        ///     Handles the request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.User>().Where(x => x.Id != null)
                    .LeftJoin(db.GetTable<AdministrationSchema.UserProfile>(),
                        (user, userProfile) => user.Id == userProfile.UserId,
                        (user, userProfile) => new {user, userProfile})
                    .GroupJoin(db.GetTable<AdministrationSchema.UserClaim>(), user => user.user.Id,
                        userClaim => userClaim.UserId,
                        (parent, claims) =>
                            AdministrationManager.Build(parent.user, parent.userProfile, claims.ToList()))
                    .ToListAsync();
                return items;
            }
        }

        #endregion
    }
}