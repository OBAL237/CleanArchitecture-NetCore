using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ProduitDto
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [Required]
        [JsonPropertyName("libelle")] 
        public string Libelle { get; set; }

        [Required]
        [JsonPropertyName("reference")] 
        public string Reference { get; set; }

        [JsonPropertyName("dateenregistrement")]
        public DateTime? DateEnregistrement { get; set; }
    }
}
