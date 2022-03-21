
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.ApplicationCore.Interfaces.IRepositories
{
    public interface IApplicationDatabaseContext : IDisposable, IDbContext 
    {
        public  DbSet<Produit> Produits { get; set; }
        public  DbSet<Propriete> Proprietes { get; set; } 
        public  DbSet<TypePropriete> TypeProprietes { get; set; }
        public  DbSet<Commande> Commandes { get; set; }
        public  DbSet<LigneCommande> LigneCommandes { get; set; }
    }
}
