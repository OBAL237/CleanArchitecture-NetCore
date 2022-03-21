using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IServices
{
    public interface IProduitService 
    {
        Task<IEnumerable<ProduitResponse>> GetAllAsync();
        Task<int> CreateOrUpdateAsync(ProduitDto ProduitDto);
        Task<int> DeleteByIdAsync(Guid Id);
    }
}
