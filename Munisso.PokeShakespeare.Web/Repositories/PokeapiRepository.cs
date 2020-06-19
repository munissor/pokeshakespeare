using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Repositories.Models.Pokemon;

namespace Munisso.PokeShakespeare.Repositories
{
    public class PokeapiRepository : HttpRepositoryBase, IPokeapiRepository
    {
        public const string POKEAPI_URL = "https://pokeapi.co/api/v2";
        const string LANGUAGE = "en";

        public PokeapiRepository()
            : base(new HttpClientHandler())
        {
        }

        public PokeapiRepository(HttpMessageHandler messageHandler)
            : base(messageHandler)
        {
        }


        public async Task<PokemonDescription> GetDescription(string pokemonName)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                throw new ArgumentException($"'{nameof(pokemonName)}' cannot be null or whitespace", nameof(pokemonName));
            }

            string endpoint = $"{POKEAPI_URL}/pokemon-species/{pokemonName}";
            using (var httpClient = base.GetClient())
            {
                var response = await httpClient.GetAsync(endpoint);

                // Validate that the API call succeeded
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException($"The Pokemon {pokemonName} doesn't exist.");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error calling the remote endpoint");
                }

                // extract the data we need
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Species>(content);

                // There are many descriptions depending on the language and game edition,
                // here we select the first english one
                var textEntry = (from entry in data.FlavorTextEntries
                                 where entry.Language.Name == LANGUAGE
                                 select entry).FirstOrDefault();

                if (textEntry == null)
                {
                    throw new Exception($"There is no English description for {pokemonName}");
                }

                return new PokemonDescription(data.Name, textEntry.FlavorText);
            }
        }
    }
}