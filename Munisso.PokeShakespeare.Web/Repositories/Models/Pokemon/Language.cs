using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Repositories.Models.Pokemon
{
    internal class Language
    {
        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("url")]
        public string Url { get; set; }
    }
}