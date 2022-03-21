using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class LigneCommande
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        public Guid ProduitId { get; set; }

        [Required]
        public Guid CommandeId { get; set; }

        [Required]
        public int Quantite { get; set; }

        [Required]
        public decimal Prix { get; set; }
    }
}