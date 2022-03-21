using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class ProduitResponse
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public string Reference { get; set; }
        public DateTime DateEnregistrement { get; set; }
        public ICollection<ProprieteResponse> Proprietes { get; set; }
        public ICollection<LigneCommandeResponse> LigneCommandes { get; set; }
    }
}
