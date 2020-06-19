
using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Repositories.Models.Pokemon
{
    internal class Species
    {
        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("flavor_text_entries")]
        public FlavorTextEntry[] FlavorTextEntries { get; set; }
    }
}
