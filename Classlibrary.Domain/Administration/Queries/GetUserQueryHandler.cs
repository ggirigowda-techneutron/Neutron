#region Copyright Neutron © 2019

//
// NAME:			GetUserQueryHandler.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Handler
//

#endregion

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Classlibrary.Dao.Linq2Db;
using LinqToDB;
using MediatR;

namespace Classlibrary.Domain.Administration.Queries
{
    /// <summary>
    ///     Represents the <see cref="GetUserQueryHandler" /> class.
    /// </summary>
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        #region Implementation of IRequestHandler<in GetUserQuery,List<User>>

        /// <summary>
        ///     Handles the request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new ArgumentException("Invalid id", nameof(request.Id));
            using (var db = new PRACTISEV1DB())
            {
                var items = await db.GetTable<AdministrationSchema.User>().Where(x => x.Id == request.Id)
                    .LeftJoin(db.GetTable<AdministrationSchema.UserProfile>(),
                        (user, userProfile) => user.Id == userProfile.UserId,
                        (user, userProfile) => new {user, userProfile})
                    .GroupJoin(db.GetTable<AdministrationSchema.UserClaim>(), user => user.user.Id,
                        userClaim => userClaim.UserId,
                        (parent, claims) =>
                            AdministrationManager.Build(parent.user, parent.userProfile, claims.ToList()))
                    .ToListAsync();
                return items.FirstOrDefault();
            }
        }

        #endregion
    }
}