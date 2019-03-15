#region Copyright Neutron © 2019

//
// NAME:			CqrsPreProcessor.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			CQRS 
//

#endregion

using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Classlibrary.Domain.Cqrs
{
    /// <summary>
    ///     Represents the <see cref="CqrsPreProcessor{TRequest}" /> class.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class CqrsPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        /// <summary>
        ///     Logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Creates an instance of <see cref="CqrsPreProcessor{TRequest}" />.
        /// </summary>
        /// <param name="logger"></param>
        public CqrsPreProcessor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        #region Implementation of IRequestPreProcessor<in TRequest>

        /// <summary>
        ///     Process request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            //_logger.LogInformation("Pre Process: {Name} {@Request}", name, request);
            _logger.LogInformation("Pre Process: {Name}", name);
        }

        #endregion
    }
}