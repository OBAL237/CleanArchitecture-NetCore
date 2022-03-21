using AutoMapper;
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.ApplicationCore.Interfaces.IServices;
using Backend.Domain;
using Backend.Domain.Entities;
using Backend.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Services
{
    public class LigneCommandeService: ILigneCommandeService
    {
        private readonly ILigneCommandeRepository _LigneCommandeRepository;
        private readonly ILogger<LigneCommandeService> _log;
        private readonly IMapper _mapper;
        public LigneCommandeService(ILogger<LigneCommandeService> log,
                                    IMapper mapper,
                                    ILigneCommandeRepository LigneCommandeRepository)
        {
            _log = log;
            _mapper = mapper;
            _LigneCommandeRepository = LigneCommandeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LigneCommandeResponse>> GetAllAsync() 
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Get all Type");
            if (_LigneCommandeRepository != null)
            {
                using Task<IEnumerable<LigneCommande>> task = _LigneCommandeRepository.FindAllAsync();
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    return _mapper.Map<IEnumerable<LigneCommandeResponse>>(task.Result);
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
        /// <param name="ligneCommandeDto"></param>
        /// <returns></returns>
        public async Task<int> CreateOrUpdateAsync(LigneCommandeDto ligneCommandeDto)
        {
            _log.LogDebug($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - request to Create or Update Type");
            if (_LigneCommandeRepository != null && ligneCommandeDto != null)
            {
                LigneCommande currentLigneCommande = _mapper.Map<LigneCommande>(ligneCommandeDto);
                if (ligneCommandeDto.Id != null)
                {
                    using Task<LigneCommande> taskFind = _LigneCommandeRepository.FindAsync(ligneCommandeDto.Id);
                    await taskFind;
                    if (taskFind.IsCompleted && taskFind.IsCompletedSuccessfully)
                    {
                        if (taskFind.Result != null)
                        {
                            currentLigneCommande.Prix = taskFind.Result.Prix;
                            _LigneCommandeRepository.Entry(currentLigneCommande, taskFind.Result);
                            using Task<LigneCommande> result = _LigneCommandeRepository.UpdateAsync(currentLigneCommande);
                            await result;
                            if (result.IsCompleted && result.IsCompletedSuccessfully)
                            {
                                using Task<int> taskUpdate = _LigneCommandeRepository.SaveChangesAsync();
                                await taskUpdate;
                                if (taskUpdate.IsCompleted && taskUpdate.IsCompletedSuccessfully)
                                {
                                    return taskUpdate.Result;
                                }
                                else
                                {
                                    _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande"}");
                                    throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande");
                                }
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": LigneCommande"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_UPDATE + ": LigneCommande");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_NOT_EXIST_MSG_ERROR + ": LigneCommande"}");
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
                    await InitialyseDataBeforSaveAsync(currentLigneCommande);
                    using Task<LigneCommande> result = _LigneCommandeRepository.AddAsync(currentLigneCommande);
                    await result;
                    if (result.IsCompleted && result.IsCompletedSuccessfully)
                    {
                        using Task<int> taskAdd = _LigneCommandeRepository.SaveChangesAsync();
                        await taskAdd;
                        if (taskAdd.IsCompleted && taskAdd.IsCompletedSuccessfully)
                        {
                            return taskAdd.Result;
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande");
                        }
                    }
                    else
                    {
                        _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": LigneCommande"}");
                        throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_ADD + ": LigneCommande");
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
        /// <param name="ligneCommandeDto"></param>
        /// <returns></returns>
        private async Task InitialyseDataBeforSaveAsync(LigneCommande  ligneCommandeDto)
        {
            ligneCommandeDto.Id = new Guid();
            using Task<decimal> x = GetPrice(_LigneCommandeRepository.GetLigneCommandesOfDate(DateTime.Now.Date));
            await x;
            if (x.IsCompleted && x.IsCompletedSuccessfully)
            {
                ligneCommandeDto.Prix = x.Result;
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
            if (_LigneCommandeRepository != null && Id != null)
            {
                using Task<LigneCommande> task = _LigneCommandeRepository.FindAsync(Id);
                await task;
                if (task.IsCompleted && task.IsCompletedSuccessfully)
                {
                    if (task.Result != null)
                    {
                        using Task taskDelete = _LigneCommandeRepository.DeleteAsync(task.Result);
                        await taskDelete;
                        if (taskDelete.IsCompleted && taskDelete.IsCompletedSuccessfully)
                        {
                            using Task<int> taskSave = _LigneCommandeRepository.SaveChangesAsync();
                            await taskSave;
                            if (taskSave.IsCompleted && taskSave.IsCompletedSuccessfully)
                            {
                                return taskSave.Result;
                            }
                            else
                            {
                                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande"}");
                                throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_SAVE + ": LigneCommande");
                            }
                        }
                        else
                        {
                            _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": LigneCommande"}");
                            throw new Exception(ErrorsConstants.S_REPO_MSG_ERROR_REMOVE + ": LigneCommande");
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

        public async Task<decimal> GetPrice(List<LigneCommande> ligneCommandes)
        {
            if (ligneCommandes != null)
            {
                int compteur = ligneCommandes.Count();
                if (compteur > 1)
                {
                    return ligneCommandes[0].Prix + ligneCommandes[1].Prix;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                _log.LogError($"{this.GetType().Name} -> {MethodBase.GetCurrentMethod()} - {ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG + ": Ligne Commande"}");
                throw new Exception(ErrorsConstants.S_APPLICATION_DBCONTEXT_NULL_MSG + ": Ligne Commande");
            }
        }
    }
}
