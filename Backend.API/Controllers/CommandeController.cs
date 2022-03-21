
using Backend.Domain;
using Backend.Models;
using Mediator;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandeController : ControllerBase
    {
        private const string c_getAllCommande = "getAll"; 
        private const string c_postCreateOrUpdate = "createOrUpdate";
        private const string c_deleteById = "deleteById/{id}";

        private readonly ILogger<CommandeController> _log;
        private readonly IMediator _mediator;

        public CommandeController(ILogger<CommandeController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        /// <summary>
        /// get all type propriete
        /// </summary>
        /// <returns></returns>
        [HttpGet(c_getAllCommande)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Request to get all Type");
            if (_mediator == null)
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
            }
            try
            {
                var query = new GetCommandeListQuery();
                using Task<IEnumerable<CommandeResponse>> typesTask = _mediator.Send(query);
                await typesTask;
                if (typesTask.IsCompleted && typesTask.IsCompletedSuccessfully)
                {
                    return Ok(typesTask.Result);
                }
                else
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Exception:{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// create or update type
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost(c_postCreateOrUpdate)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateAsync([FromBody] AddCommandeCommand cmd)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Request to create or update Type");
            if (_mediator == null)
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
            }
            if(cmd != null)
            {
                try
                {
                    using Task<int> typesTask = _mediator.Send(cmd);
                    await typesTask;
                    if (typesTask.IsCompleted && typesTask.IsCompletedSuccessfully)
                    {
                        return Ok(typesTask.Result);
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                        return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Exception:{ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_4XX}");
                return StatusCode(StatusCodes.Status400BadRequest, ErrorsConstants.S_HTTP_ERROR_4XX);
            }
           
        }

        /// <summary>
        /// delete type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(c_deleteById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Request to delete type by: {id}");
            if (_mediator == null)
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
            }
            if (id != null)
            {
                try
                {
                    RemoveCommandeCommand removeCommandeCommand = new RemoveCommandeCommand { Id = id };
                    using Task<int> typesTask = _mediator.Send(removeCommandeCommand);
                    await typesTask;
                    if (typesTask.IsCompleted && typesTask.IsCompletedSuccessfully)
                    {
                        return Ok(typesTask.Result);
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_5XX}");
                        return StatusCode(StatusCodes.Status500InternalServerError, ErrorsConstants.S_HTTP_ERROR_5XX);
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - Exception:{ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_HTTP_ERROR_4XX}");
                return StatusCode(StatusCodes.Status400BadRequest, ErrorsConstants.S_HTTP_ERROR_4XX);
            }

        }
    }
}
