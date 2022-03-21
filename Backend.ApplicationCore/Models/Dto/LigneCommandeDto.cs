using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class LigneCommandeDto
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [Required]
        [JsonPropertyName("produitid")] 
        public Guid ProduitId { get; set; }

        [Required]
        [JsonPropertyName("commandeid")] 
        public Guid CommandeId { get; set; }

        [Required]
        [JsonPropertyName("quantite")] 
        public int Quantite { get; set; }

    }
}
