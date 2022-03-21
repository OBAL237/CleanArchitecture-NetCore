using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class TypeProprieteDto
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }
        
        [Required]
        [JsonPropertyName("libelle")]
        public string Libelle { get; set; }

        [JsonPropertyName("estarchive")]
        public bool? EstArchive { get; set; }
    }
}
