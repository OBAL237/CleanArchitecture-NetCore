using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ProprieteDto
    { 
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [Required]
        [JsonPropertyName("produitid")]
        public Guid ProduitId { get; set; }

        [Required]
        [JsonPropertyName("typeproprieteid")]
        public Guid TypeProprieteId { get; set; }

        [Required]
        [JsonPropertyName("valeur")] 
        public string Valeur { get; set; }
    }
}
