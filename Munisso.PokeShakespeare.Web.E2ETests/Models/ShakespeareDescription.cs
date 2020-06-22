using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Models
{
    public class ShakespeareDescription
    {
        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }
    }
}