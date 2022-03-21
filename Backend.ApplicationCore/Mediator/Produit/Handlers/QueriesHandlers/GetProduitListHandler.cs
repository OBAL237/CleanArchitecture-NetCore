
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
    public class GetProduitListHandler : IRequestHandler<GetProduitListQuery, IEnumerable<ProduitResponse>>
    {
        private readonly IProduitService _ProduitService;
        private readonly ILogger<GetProduitListHandler> _log;
        public GetProduitListHandler(ILogger<GetProduitListHandler> log, IProduitService ProduitService) 
        {
            _log = log;
            _ProduitService = ProduitService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProduitResponse>> Handle(GetProduitListQuery request, CancellationToken cancellationToken)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Handle of Type");
            if (_ProduitService != null) 
            {
                using Task<IEnumerable<ProduitResponse>> task = _ProduitService.GetAllAsync();
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
