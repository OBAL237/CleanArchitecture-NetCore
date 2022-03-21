using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ApplicationCore.Interfaces.IRepositories
{
    public interface ILigneCommandeRepository : IGenericRepository<LigneCommande>
    {
        List<LigneCommande> GetLigneCommandesOfDate(DateTime dateTimeRef);
    }
}
