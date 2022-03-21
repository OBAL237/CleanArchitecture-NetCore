using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Persistence.Config
{
    public class ApplicationDatabaseContext : DbContext,IApplicationDatabaseContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Propriete> Proprietes { get; set; }
        public DbSet<TypePropriete> TypeProprietes { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<LigneCommande> LigneCommandes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
