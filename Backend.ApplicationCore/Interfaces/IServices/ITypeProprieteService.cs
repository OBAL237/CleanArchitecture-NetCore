using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IServices
{
    public interface ITypeProprieteService 
    {
        Task<IEnumerable<TypeProprieteResponse>> GetAllAsync();
        Task<int> CreateOrUpdateAsync(TypeProprieteDto typeProprieteDto);
        Task<int> DeleteByIdAsync(Guid Id);
    }
}
