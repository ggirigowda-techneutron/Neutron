#region Copyright Neutron © 2019

//
// NAME:			BaseController.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Controller
//

#endregion

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Middleware.Core.WebApi.V1.Controllers
{
    /// <summary>
    ///     Represents the <see cref="BaseController" /> class.
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        ///// <summary>
        /////     Mediator.
        ///// </summary>
        //private IMediator _mediator;

        ///// <summary>
        /////     Mediator.
        ///// </summary>
        //protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        /// <summary>
        ///     Mediator.
        /// </summary>
        protected IMediator Mediator;

        /// <summary>
        ///     Logger
        /// </summary>
        protected ILogger Logger;
    }
}