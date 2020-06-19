using System.Threading.Tasks;
using System.Net.Http;
using Munisso.PokeShakespeare.Models;

namespace Munisso.PokeShakespeare.Repositories
{
    public interface IPokeapiRepository
    {
        Task<PokemonDescription> GetDescription(string pokemonName);
    }
}