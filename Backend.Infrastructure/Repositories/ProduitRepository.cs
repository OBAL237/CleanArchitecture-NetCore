

using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Repositories
{
    public class ProduitRepository : GenericRepository<Produit>, IProduitRepository
    {
        public ProduitRepository(IApplicationDatabaseContext context) : base(context) 
        {
        }
    }
}
