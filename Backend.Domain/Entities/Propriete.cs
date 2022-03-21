using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class Propriete
    {
        [Key] 
        public Guid Id { get; set; }

        [Required] 
        public Guid ProduitId { get; set; }

        [Required]
        public Guid TypeProprieteId { get; set; }

        [Required]
        public string Valeur { get; set; }
    }
}