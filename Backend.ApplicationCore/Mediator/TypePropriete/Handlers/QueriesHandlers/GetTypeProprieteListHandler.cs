
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
    public class GetTypeProprieteListHandler : IRequestHandler<GetTypeProprieteListQuery, IEnumerable<TypeProprieteResponse>>
    {
        private readonly ITypeProprieteService _typeProprieteService;
        private readonly ILogger<GetTypeProprieteListHandler> _log;
        public GetTypeProprieteListHandler(ILogger<GetTypeProprieteListHandler> log, ITypeProprieteService typeProprieteService) 
        {
            _log = log;
            _typeProprieteService = typeProprieteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TypeProprieteResponse>> Handle(GetTypeProprieteListQuery request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Handle of Type");
            if (_typeProprieteService != null) 
            {
                using Task<IEnumerable<TypeProprieteResponse>> task = _typeProprieteService.GetAllAsync();
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
