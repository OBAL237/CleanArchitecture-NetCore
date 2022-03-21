using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IServices
{
    public interface IProprieteService 
    {
        Task<IEnumerable<ProprieteResponse>> GetAllAsync();
        Task<int> CreateOrUpdateAsync(ProprieteDto ProprieteDto);
        Task<int> DeleteByIdAsync(Guid Id);
    }
}
