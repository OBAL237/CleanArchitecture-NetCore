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
    public class CommandeService: ICommandeService
    {
        private readonly ICommandeRepository _CommandeRepository;
        private readonly ILogger<CommandeService> _log;
        private readonly IMapper _mapper;
        public CommandeService(ILogger<CommandeService> log,
                                    IMapper mapper,
                                    ICommandeRepository CommandeRepository)
        {
            _log = log;
            _mapper = mapper;
            _CommandeRepository = CommandeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CommandeResponse>> GetAllAsync() 
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Get all Type");
            if (_CommandeRepository != null)
            {
                using Task<IEnumerable<Commande>> task = _CommandeRepository.FindAllAsync();
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return _mapper.Map<IEnumerable<CommandeResponse>>(task.Result);
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
        /// <param name="commandeDto"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdateAsync(CommandeDto commandeDto)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Create or Update Type");
            if (_CommandeRepository != null && commandeDto != null)
            {
                Commande currentCommande = _mapper.Map<Commande>(commandeDto);
                if (commandeDto.Id != null)
                {
                    using Task<Commande> taskFind = _CommandeRepository.FindAsync(commandeDto.Id);
                    await taskFind;
                    if (taskFind.IsCompleted && taskFind.IsCompletedSuccessfully)
                    {
                        if (taskFind.Result != null)
                        {
                            currentCommande.DateEnregistrement = taskFind.Result.DateEnregistrement;
                            _CommandeRepository.Entry(currentCommande, taskFind.Result);
                            using Task<Commande> result = _CommandeRepository.UpdateAsync(currentCommande);
                            await result;
                            if (result.IsCompleted && result.IsCompletedSuccessfully)
                            {
                                using Task<int> taskUpdate = _CommandeRepository.SaveChangesAsync();
                                await taskUpdate;
                                if (taskUpdate.IsCompleted && taskUpdate.IsCompletedSuccessfully)
                                {
                                    return taskUpdate.Result;
                                }
                                else
                                {
                                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande"}");
                                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande");
                                }
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Commande"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": Commande");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": Commande"}");
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
                    currentCommande.Id = new Guid();
                    currentCommande.DateEnregistrement = DateTime.Now.Date;
                    using Task<Commande> result = _CommandeRepository.AddAsync(currentCommande);
                    await result;
                    if (result.IsCompleted && result.IsCompletedSuccessfully)
                    {
                        using Task<int> taskAdd = _CommandeRepository.SaveChangesAsync();
                        await taskAdd;
                        if (taskAdd.IsCompleted && taskAdd.IsCompletedSuccessfully)
                        {
                            return taskAdd.Result;
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Commande"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": Commande");
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
            if (_CommandeRepository != null && Id != null)
            {
                using Task<Commande> task = _CommandeRepository.FindAsync(Id);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    if (task.Result != null)
                    {
                        using Task taskDelete = _CommandeRepository.DeleteAsync(task.Result);
                        await taskDelete;
                        if (taskDelete.IsCompleted && taskDelete.IsCompletedSuccessfully)
                        {
                            using Task<int> taskSave = _CommandeRepository.SaveChangesAsync();
                            await taskSave;
                            if (taskSave.IsCompleted && taskSave.IsCompletedSuccessfully)
                            {
                                return taskSave.Result;
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": Commande");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Commande"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": Commande");
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
