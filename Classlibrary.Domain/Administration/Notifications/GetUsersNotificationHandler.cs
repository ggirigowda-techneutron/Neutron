#region Copyright Neutron © 2019

//
// NAME:			GetUsersNotificationHandler.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			CQRS 
//

#endregion

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Classlibrary.Domain.Administration.Notifications
{
    /// <summary>
    ///     Represents the <see cref="GetUsersNotificationHandler" /> class.
    /// </summary>
    public class GetUsersNotificationHandler : INotificationHandler<GetUsersNotification>
    {
        /// <summary>
        ///     Logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Creates an instance of <see cref="GetUsersNotificationHandler" />.
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersNotificationHandler(ILogger<GetUsersNotificationHandler> logger)
        {
            _logger = logger;
        }

        #region Implementation of INotificationHandler<in GetUsersNotification>

        /// <summary>
        ///     Handle.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(GetUsersNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsers Notification handled");
        }

        #endregion
    }
}