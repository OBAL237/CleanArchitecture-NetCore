
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
    public class GetLigneCommandeListHandler : IRequestHandler<GetLigneCommandeListQuery, IEnumerable<LigneCommandeResponse>>
    {
        private readonly ILigneCommandeService _LigneCommandeService;
        private readonly ILogger<GetLigneCommandeListHandler> _log;
        public GetLigneCommandeListHandler(ILogger<GetLigneCommandeListHandler> log, ILigneCommandeService LigneCommandeService) 
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
        public async Task<IEnumerable<LigneCommandeResponse>> Handle(GetLigneCommandeListQuery request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Handle of Type");
            if (_LigneCommandeService != null) 
            {
                using Task<IEnumerable<LigneCommandeResponse>> task = _LigneCommandeService.GetAllAsync();
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
