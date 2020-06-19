using System.Threading.Tasks;
using Munisso.PokeShakespeare.Models;

namespace Munisso.PokeShakespeare.Repositories
{
    public interface IShakespeareTranslatorRepository
    {
        Task<Translation> Translate(string text);
    }
}