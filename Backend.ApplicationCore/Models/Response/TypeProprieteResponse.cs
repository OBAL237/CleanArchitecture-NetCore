using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class TypeProprieteResponse
    {
        public Guid Id { get; set; }
        public string Libelle { get; set; }
        public bool EstArchive { get; set; }
        public ICollection<ProprieteResponse> Proprietes { get; set; }
    }
}
