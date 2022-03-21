
using Backend.Domain.Entities;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IServices
{
    public interface ILigneCommandeService 
    {
        Task<IEnumerable<LigneCommandeResponse>> GetAllAsync();
        Task<int> CreateOrUpdateAsync(LigneCommandeDto LigneCommandeDto);
        Task<int> DeleteByIdAsync(Guid Id);
        Task<decimal> GetPrice(List<LigneCommande> ligneCommandes);
    }
}
