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
    public class ProduitService: IProduitService
    {
        private readonly IProduitRepository _ProduitRepository;
        private readonly ILogger<ProduitService> _log;
        private readonly IMapper _mapper;
        public ProduitService(ILogger<ProduitService> log,
                                    IMapper mapper,
                                    IProduitRepository ProduitRepository)
        {
            _log = log;
            _mapper = mapper;
            _ProduitRepository = ProduitRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProduitResponse>> GetAllAsync() 
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Get all Type");
            if (_ProduitRepository != null)
            {
                using Task<IEnumerable<Produit>> task = _ProduitRepository.FindAllAsync();
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return _mapper.Map<IEnumerable<ProduitResponse>>(task.Result);
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
        /// <param name="ProduitDto"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdateAsync(ProduitDto ProduitDto)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Create or Update Type");
            if (_ProduitRepository != null && ProduitDto != null)
            {
                if (ProduitDto.Id != null)
                {
                    using Task<Produit> taskFind = _ProduitRepository.FindAsync(ProduitDto.Id);
                    await taskFind;
                    if (taskFind.IsCompleted && taskFind.IsCompletedSuccessfully)
                    {
                        if (taskFind.Result != null)
                        {
                            _ProduitRepository.Entry(_mapper.Map<Produit>(ProduitDto), taskFind.Result);
                            using Task<Produit> result = _ProduitRepository.UpdateAsync(_mapper.Map<Produit>(ProduitDto));
                            await result;
                            if (result.IsCompleted && result.IsCompletedSuccessfully)
                            {
                                using Task<int> taskUpdate = _ProduitRepository.SaveChangesAsync();
                                await taskUpdate;
                                if (taskUpdate.IsCompleted && taskUpdate.IsCompletedSuccessfully)
                                {
                                    return taskUpdate.Result;
                                }
                                else
                                {
                                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit"}");
                                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit");
                                }
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Produit"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Produit");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": Produit"}");
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
                    ProduitDto.Id = new Guid();
                    using Task<Produit> result = _ProduitRepository.AddAsync(_mapper.Map<Produit>(ProduitDto));
                    await result;
                    if (result.IsCompleted && result.IsCompletedSuccessfully)
                    {
                        using Task<int> taskAdd = _ProduitRepository.SaveChangesAsync();
                        await taskAdd;
                        if (taskAdd.IsCompleted && taskAdd.IsCompletedSuccessfully)
                        {
                            return taskAdd.Result;
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Produit"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Produit");
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
            if (_ProduitRepository != null && Id != null)
            {
                using Task<Produit> task = _ProduitRepository.FindAsync(Id);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    if (task.Result != null)
                    {
                        using Task taskDelete = _ProduitRepository.DeleteAsync(task.Result);
                        await taskDelete;
                        if (taskDelete.IsCompleted && taskDelete.IsCompletedSuccessfully)
                        {
                            using Task<int> taskSave = _ProduitRepository.SaveChangesAsync();
                            await taskSave;
                            if (taskSave.IsCompleted && taskSave.IsCompletedSuccessfully)
                            {
                                return taskSave.Result;
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Produit");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Produit"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Produit");
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
