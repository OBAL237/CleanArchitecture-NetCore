using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
    public class TypePropriete
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        public string Libelle { get; set; }

        public bool EstArchive { get; set; }
        public ICollection<Propriete> Proprietes { get; set; }

    }
}