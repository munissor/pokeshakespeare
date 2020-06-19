using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Repositories.Models.Pokemon
{
    internal class FlavorTextEntry
    {
        [JsonPropertyNameAttribute("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyNameAttribute("language")]
        public Language Language { get; set; }
    }
}