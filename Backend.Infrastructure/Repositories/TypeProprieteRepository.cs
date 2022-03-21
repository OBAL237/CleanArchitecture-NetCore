using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class TypeProprieteRepository : GenericRepository<TypePropriete>, ITypeProprieteRepository
    {
        public TypeProprieteRepository(IApplicationDatabaseContext context) : base(context)  
        {
        }
    }
}
