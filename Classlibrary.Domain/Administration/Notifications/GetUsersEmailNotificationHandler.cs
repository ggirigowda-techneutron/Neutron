#region Copyright Neutron © 2019

//
// NAME:			GetUsersEmailNotificationHandler.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			CQRS 
//

#endregion

using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
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
        ///     Email.
        /// </summary>
        private readonly Email _email;

        /// <summary>
        ///     Creates an instance of <see cref="GetUsersEmailNotificationHandler" />.
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersEmailNotificationHandler(ILogger<GetUsersEmailNotificationHandler> logger)
        {
            _logger = logger;
            _email = new Email(new RazorRenderer()
                , new SmtpSender(() => new SmtpClient("smtp.gmail.com")
                {
                    EnableSsl = true,
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("jbs.smtp@gmail.com", "testdb99!!")
                }));
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
            var model = new
            {
                FirstName = "John",
                Lastname = "Doe",
                Numbers = new[] {1, 2, 3}
            };
            var template =
                "Hi @Model.FirstName @Model.Lastname this is a razor template @(5 + 5)! <br /><ul>@foreach(var i in Model.Numbers) { <li>@i</li> }</ul>";

            await _email.SetFrom("jbs.smtp@gmail.com")
                .To("jbs.smtp@gmail.com")
                .Subject($"Get Users - {DateTime.UtcNow}")
                .UsingTemplate(template, model)
                .SendAsync();
        }

        #endregion
    }
}