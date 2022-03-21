using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class CommandeDto
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [Required]
        [JsonPropertyName("numerocommande")]
        public string NumeroCommande { get; set; }

        [Required]
        [JsonPropertyName("reference")] 
        public string Reference { get; set; }

    }
}
