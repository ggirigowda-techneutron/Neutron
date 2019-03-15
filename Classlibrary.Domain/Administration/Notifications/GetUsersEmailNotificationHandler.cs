#region Copyright Neutron © 2019

//
// NAME:			GetUsersEmailNotificationHandler.cs
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
    ///     Represents the <see cref="GetUsersEmailNotificationHandler" /> class.
    /// </summary>
    public class GetUsersEmailNotificationHandler : INotificationHandler<GetUsersNotification>
    {
        /// <summary>
        ///     Logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Creates an instance of <see cref="GetUsersEmailNotificationHandler" />.
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersEmailNotificationHandler(ILogger<GetUsersEmailNotificationHandler> logger)
        {
            _logger = logger;
        }

        #region Implementation of INotificationHandler<in GetUsersEmailNotification>

        /// <summary>
        ///     Handle.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(GetUsersNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersEmail Notification handled");
        }

        #endregion
    }
}