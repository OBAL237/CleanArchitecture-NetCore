using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class Commande
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        public string NumeroCommande { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required] 
        public DateTime DateEnregistrement { get; set; }

        public ICollection<LigneCommande> LigneCommandes { get; set; }
    }
}
