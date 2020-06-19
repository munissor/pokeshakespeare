
using System.Text.Json.Serialization;

namespace Munisso.PokeShakespeare.Repositories.Models.Shakespeare
{
    internal class Response
    {
        [JsonPropertyNameAttribute("contents")]
        public Contents Contents { get; set; }
    }
}
