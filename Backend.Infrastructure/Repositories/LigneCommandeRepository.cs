
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Infrastructure.Repositories
{
    public class LigneCommandeRepository : GenericRepository<LigneCommande>, ILigneCommandeRepository
    {
        public LigneCommandeRepository(IApplicationDatabaseContext context) : base(context)  
        {
        }

        public List<LigneCommande> GetLigneCommandesOfDate(DateTime dateTimeRef)
        {
            var ligneCommandes = (from lc in _context.LigneCommandes
                                     join cmd in _context.Commandes on lc.CommandeId equals cmd.Id
                                     where cmd.DateEnregistrement.Date.Equals(dateTimeRef.Date)
                                     select lc).OrderByDescending(e => e.Prix).ToList();
            return ligneCommandes;
        }
    }
}
