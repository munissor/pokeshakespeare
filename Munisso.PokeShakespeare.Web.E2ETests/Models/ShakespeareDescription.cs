using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Models
{
    public class ShakespeareDescription
    {
        [JsonPropertyNameAttribute("pokemon")]
        public string Pokemon { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }
    }
}