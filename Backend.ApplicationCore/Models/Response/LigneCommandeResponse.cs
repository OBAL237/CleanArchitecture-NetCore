using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class LigneCommandeResponse 
    {
        public Guid Id { get; set; }
        public Guid ProduitId { get; set; }
        public Guid CommandeId { get; set; }
        public int Quantite { get; set; }
        public decimal Prix { get; set; }
    }
}
