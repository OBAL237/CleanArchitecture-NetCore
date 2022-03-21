

using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class ProprieteRepository : GenericRepository<Propriete>, IProprieteRepository
    {
        public ProprieteRepository(IApplicationDatabaseContext context) : base(context) 
        {
        }
    }
}
