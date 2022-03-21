using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IServices
{
    public interface ICommandeService 
    {
        Task<IEnumerable<CommandeResponse>> GetAllAsync();
        Task<int> CreateOrUpdateAsync(CommandeDto CommandeDto);
        Task<int> DeleteByIdAsync(Guid Id);
    }
}
