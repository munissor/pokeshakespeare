using System;
using System.Threading.Tasks;

namespace Munisso.PokeShakespeare.Repositories
{
    public interface ICache
    {
        Task Set<T>(string key, T content, TimeSpan offset);

        Task<T> Get<T>(string key);
    }
}