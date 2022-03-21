
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
    public class RemoveLigneCommandeHandler : IRequestHandler<RemoveLigneCommandeCommand, int>
    {
        public readonly ILigneCommandeService _LigneCommandeService;
        private readonly ILogger<RemoveLigneCommandeHandler> _log;
        public RemoveLigneCommandeHandler(ILogger<RemoveLigneCommandeHandler> log, ILigneCommandeService LigneCommandeService)
        {
            _log = log;
            _LigneCommandeService = LigneCommandeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(RemoveLigneCommandeCommand request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Command Handle of Type");
            if (_LigneCommandeService != null)
            {
                using Task<int> task = _LigneCommandeService.DeleteByIdAsync(request.Id);
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
