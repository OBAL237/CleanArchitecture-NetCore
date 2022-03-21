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
    public class TypeProprieteService: ITypeProprieteService
    {
        private readonly ITypeProprieteRepository _typeProprieteRepository;
        private readonly ILogger<TypeProprieteService> _log;
        private readonly IMapper _mapper;
        public TypeProprieteService(ILogger<TypeProprieteService> log,
                                    IMapper mapper,
                                    ITypeProprieteRepository typeProprieteRepository)
        {
            _log = log;
            _mapper = mapper;
            _typeProprieteRepository = typeProprieteRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TypeProprieteResponse>> GetAllAsync() 
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Get all Type");
            if (_typeProprieteRepository != null)
            {
                using Task<IEnumerable<TypePropriete>> task = _typeProprieteRepository.FindAllAsync();
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return _mapper.Map<IEnumerable<TypeProprieteResponse>>(task.Result);
                }
                else
                {
                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR +": Type"}");
                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR +": Type");
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
        /// <param name="typeProprieteDto"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdateAsync(TypeProprieteDto typeProprieteDto)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Create or Update Type");
            if (_typeProprieteRepository != null && typeProprieteDto != null)
            {
                if (typeProprieteDto.Id != null)
                {
                    using Task<TypePropriete> taskFind = _typeProprieteRepository.FindAsync(typeProprieteDto.Id);
                    await taskFind;
                    if (taskFind.IsCompleted && taskFind.IsCompletedSuccessfully)
                    {
                        if (taskFind.Result != null)
                        {
                            _typeProprieteRepository.Entry(_mapper.Map<TypePropriete>(typeProprieteDto), taskFind.Result);
                            using Task<TypePropriete> result = _typeProprieteRepository.UpdateAsync(_mapper.Map<TypePropriete>(typeProprieteDto));
                            await result;
                            if (result.IsCompleted && result.IsCompletedSuccessfully)
                            {
                                using Task<int> taskUpdate = _typeProprieteRepository.SaveChangesAsync();
                                await taskUpdate;
                                if (taskUpdate.IsCompleted && taskUpdate.IsCompletedSuccessfully)
                                {
                                    return taskUpdate.Result;
                                }
                                else
                                {
                                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete"}");
                                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete");
                                }
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": TypePropriete"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": TypePropriete");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": TypePropriete"}");
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
                    typeProprieteDto.Id = new Guid();
                    using Task<TypePropriete> result = _typeProprieteRepository.AddAsync(_mapper.Map<TypePropriete>(typeProprieteDto));
                    await result;
                    if (result.IsCompleted && result.IsCompletedSuccessfully)
                    {
                        using Task<int> taskAdd = _typeProprieteRepository.SaveChangesAsync();
                        await taskAdd;
                        if (taskAdd.IsCompleted && taskAdd.IsCompletedSuccessfully)
                        {
                            return taskAdd.Result;
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": TypePropriete"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": TypePropriete");
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
            if (_typeProprieteRepository != null && Id != null)
            {
                using Task<TypePropriete> task = _typeProprieteRepository.FindAsync(Id);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    if (task.Result != null)
                    {
                        using Task taskDelete = _typeProprieteRepository.DeleteAsync(task.Result);
                        await taskDelete;
                        if (taskDelete.IsCompleted && taskDelete.IsCompletedSuccessfully)
                        {
                            using Task<int> taskSave = _typeProprieteRepository.SaveChangesAsync();
                            await taskSave;
                            if (taskSave.IsCompleted && taskSave.IsCompletedSuccessfully)
                            {
                                return taskSave.Result;
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": TypePropriete");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": TypePropriete"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": TypePropriete");
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
