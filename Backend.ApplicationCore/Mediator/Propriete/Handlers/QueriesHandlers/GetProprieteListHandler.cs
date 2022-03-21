
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
    public class GetProprieteListHandler : IRequestHandler<GetProprieteListQuery, IEnumerable<ProprieteResponse>>
    {
        private readonly IProprieteService _ProprieteService;
        private readonly ILogger<GetProprieteListHandler> _log;
        public GetProprieteListHandler(ILogger<GetProprieteListHandler> log, IProprieteService ProprieteService) 
        {
            _log = log;
            _ProprieteService = ProprieteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProprieteResponse>> Handle(GetProprieteListQuery request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Handle of Type");
            if (_ProprieteService != null) 
            {
                using Task<IEnumerable<ProprieteResponse>> task = _ProprieteService.GetAllAsync();
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
