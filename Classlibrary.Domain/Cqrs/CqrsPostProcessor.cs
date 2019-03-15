#region Copyright Neutron © 2019

//
// NAME:			CqrsPostProcessor.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			CQRS 
//

#endregion

using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Classlibrary.Domain.Cqrs
{
    /// <summary>
    ///     Represents an instance of <see cref="CqrsPostProcessor{TRequest,TResponse}" /> class.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class CqrsPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        /// <summary>
        ///     Logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Creates an instance of <see cref="CqrsPostProcessor{TRequest,TResponse}" />.
        /// </summary>
        /// <param name="logger"></param>
        public CqrsPostProcessor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }


        #region Implementation of IRequestPostProcessor<in TRequest,in TResponse>

        /// <summary>
        ///     Process.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task Process(TRequest request, TResponse response)
        {
            var name = typeof(TRequest).Name;
            //_logger.LogInformation("Post Process: {Name} {@Request} {@Response}", name, request, response);
            _logger.LogInformation("Post Process: {Name}", name);
        }

        #endregion
    }
}