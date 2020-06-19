using System;
using System.Threading.Tasks;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Repositories;

namespace Munisso.PokeShakespeare.Services
{
    public class PokeShakespeareService : IPokeShakespeareService
    {
        public PokeShakespeareService(
            IPokeapiRepository pokeapiRepository,
            IShakespeareTranslatorRepository shakespeareTranslatorRepository,
            ICache cache)
        {
            PokeapiRepository = pokeapiRepository ?? throw new System.ArgumentNullException(nameof(pokeapiRepository));
            ShakespeareTranslatorRepository = shakespeareTranslatorRepository ?? throw new System.ArgumentNullException(nameof(shakespeareTranslatorRepository));
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private IPokeapiRepository PokeapiRepository { get; }
        private IShakespeareTranslatorRepository ShakespeareTranslatorRepository { get; }
        private ICache Cache { get; }

        public async Task<ShakespeareDescription> GetPokemonDescription(string pokemonName)
        {
            string cache_key = $"description:{pokemonName}";
            ShakespeareDescription result = await Cache.Get<ShakespeareDescription>(cache_key);
            if (result == null)
            {
                var originalDescription = await PokeapiRepository.GetDescription(pokemonName);
                var translation = await ShakespeareTranslatorRepository.Translate(originalDescription.Description);
                result = new ShakespeareDescription(originalDescription.Name, translation.Translated);
                await Cache.Set(cache_key, result, new TimeSpan(0, 5, 0));
            }
            return result;
        }
    }
}