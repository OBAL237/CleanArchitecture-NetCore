
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class CommandeRepository : GenericRepository<Commande>, ICommandeRepository
    {
        public CommandeRepository(IApplicationDatabaseContext context) : base(context) 
        {
        }
    }
}
