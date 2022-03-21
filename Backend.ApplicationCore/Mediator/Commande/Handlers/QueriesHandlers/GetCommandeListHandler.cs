
using Backend.ApplicationCore.Interfaces.IServices;
using Backend.Domain;
using Backend.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    public class GetCommandeListHandler : IRequestHandler<GetCommandeListQuery, IEnumerable<CommandeResponse>>
    {
        private readonly ICommandeService _CommandeService;
        private readonly ILogger<GetCommandeListHandler> _log;
        public GetCommandeListHandler(ILogger<GetCommandeListHandler> log, ICommandeService CommandeService) 
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
        public async Task<IEnumerable<CommandeResponse>> Handle(GetCommandeListQuery request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Handle of Type");
            if (_CommandeService != null) 
            {
                using Task<IEnumerable<CommandeResponse>> task = _CommandeService.GetAllAsync();
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
