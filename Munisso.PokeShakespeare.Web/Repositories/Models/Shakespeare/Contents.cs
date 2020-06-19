
using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Repositories.Models.Shakespeare
{
    internal class Contents
    {
        [JsonPropertyNameAttribute("translated")]
        public string Translated { get; set; }

        [JsonPropertyNameAttribute("text")]
        public string Text { get; set; }
    }
}
