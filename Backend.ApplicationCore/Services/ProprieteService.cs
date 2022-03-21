using AutoMapper;
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.ApplicationCore.Interfaces.IServices;
using Backend.Domain;
using Backend.Domain.Entities;
using Backend.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Services
{
    public class ProprieteService: IProprieteService
    {
        private readonly IProprieteRepository _ProprieteRepository;
        private readonly ILogger<ProprieteService> _log;
        private readonly IMapper _mapper;
        public ProprieteService(ILogger<ProprieteService> log,
                                    IMapper mapper,
                                    IProprieteRepository ProprieteRepository)
        {
            _log = log;
            _mapper = mapper;
            _ProprieteRepository = ProprieteRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProprieteResponse>> GetAllAsync() 
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Get all propriete");
            if (_ProprieteRepository != null)
            {
                using Task<IEnumerable<Propriete>> task = _ProprieteRepository.FindAllAsync();
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return _mapper.Map<IEnumerable<ProprieteResponse>>(task.Result);
                }
                else
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR +": propriete"}");
                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR +": propriete");
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG}");
                throw new ArgumentNullException(ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProprieteDto"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdateAsync(ProprieteDto ProprieteDto)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Create or Update propriete");
            if (_ProprieteRepository != null && ProprieteDto != null)
            {
                if (ProprieteDto.Id != null)
                {
                    using Task<Propriete> taskFind = _ProprieteRepository.FindAsync(ProprieteDto.Id);
                    await taskFind;
                    if (taskFind.IsCompleted && taskFind.IsCompletedSuccessfully)
                    {
                        if (taskFind.Result != null)
                        {
                            _ProprieteRepository.Entry(_mapper.Map<Propriete>(ProprieteDto), taskFind.Result);
                            using Task<Propriete> result = _ProprieteRepository.UpdateAsync(_mapper.Map<Propriete>(ProprieteDto));
                            await result;
                            if (result.IsCompleted && result.IsCompletedSuccessfully)
                            {
                                using Task<int> taskUpdate = _ProprieteRepository.SaveChangesAsync();
                                await taskUpdate;
                                if (taskUpdate.IsCompleted && taskUpdate.IsCompletedSuccessfully)
                                {
                                    return taskUpdate.Result;
                                }
                                else
                                {
                                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete"}");
                                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete");
                                }
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Propriete"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Propriete");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": Propriete"}");
                            throw new ArgumentException(ErrorsConstants.S_NOT_EXIST_MSG_ERROR);
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR + ": TypeProriete"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR + ": TypeProprite");
                    }
                }
                else
                {
                    ProprieteDto.Id = new Guid();
                    using Task<Propriete> result = _ProprieteRepository.AddAsync(_mapper.Map<Propriete>(ProprieteDto));
                    await result;
                    if (result.IsCompleted && result.IsCompletedSuccessfully)
                    {
                        using Task<int> taskAdd = _ProprieteRepository.SaveChangesAsync();
                        await taskAdd;
                        if (taskAdd.IsCompleted && taskAdd.IsCompletedSuccessfully)
                        {
                            return taskAdd.Result;
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Propriete"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Propriete");
                    }
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG}");
                throw new ArgumentNullException(ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<int> DeleteByIdAsync(Guid Id)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to delete Type");
            if (_ProprieteRepository != null && Id != null)
            {
                using Task<Propriete> task = _ProprieteRepository.FindAsync(Id);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    if (task.Result != null)
                    {
                        using Task taskDelete = _ProprieteRepository.DeleteAsync(task.Result);
                        await taskDelete;
                        if (taskDelete.IsCompleted && taskDelete.IsCompletedSuccessfully)
                        {
                            using Task<int> taskSave = _ProprieteRepository.SaveChangesAsync();
                            await taskSave;
                            if (taskSave.IsCompleted && taskSave.IsCompletedSuccessfully)
                            {
                                return taskSave.Result;
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Propriete");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Propriete"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Propriete");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": Type"}");
                        throw new ArgumentException(ErrorsConstants.S_NOT_EXIST_MSG_ERROR);
                    }
                }
                else
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR + ": Type"}");
                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR + ": Type");
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
