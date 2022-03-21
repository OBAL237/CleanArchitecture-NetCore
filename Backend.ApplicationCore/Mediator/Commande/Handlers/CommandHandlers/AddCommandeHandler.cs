using Backend.ApplicationCore.Interfaces.IServices;
using Backend.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public class AddCommandeHandler : IRequestHandler<AddCommandeCommand, int>
    {
        public readonly ICommandeService _CommandeService;
        private readonly ILogger<AddCommandeHandler> _log;
        public AddCommandeHandler(ILogger<AddCommandeHandler> log, ICommandeService CommandeService)
        {
            _log = log;
            _CommandeService = CommandeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(AddCommandeCommand request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Command Handle of Type");
            if (_CommandeService != null)
            {
                using Task<int> task = _CommandeService.CreateOrUpdateAsync(request);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return task.Result;
                }
                else
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_SERVICE_MSG_ERROR + ": Type"}");
                    throw new Exception(ErrorsConstants.S_SERVICE_MSG_ERROR + ": Type");
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG}");
                throw new ArgumentNullException(ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG);
            }
        }

       
    }
}
