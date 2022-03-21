using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class CommandeResponse 
    {
        public Guid Id { get; set; }
        public string NumeroCommande { get; set; }
        public string Reference { get; set; }
        public DateTime DateEnregistrement { get; set; }
        public ICollection<LigneCommandeResponse> LigneCommandes { get; set; }
    }
}
