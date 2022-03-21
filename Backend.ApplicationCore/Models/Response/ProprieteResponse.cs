using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class ProprieteResponse
    {
        public Guid Id { get; set; }
        public Guid ProduitId { get; set; }
        public Guid TypeProprieteId { get; set; }
        public string Valeur { get; set; }
    }
}
