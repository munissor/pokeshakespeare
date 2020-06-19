using System.Threading.Tasks;
using Munisso.PokeShakespeare.Models;

namespace Munisso.PokeShakespeare.Services
{
    public interface IPokeShakespeareService
    {
        Task<ShakespeareDescription> GetPokemonDescription(string pokemonName);
    }
}