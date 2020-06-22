using System.Threading.Tasks;
using System.Net.Http;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Repositories.Models.Shakespeare;

namespace Munisso.PokeShakespeare.Repositories
{
    // The shakespeare translation API is very limited for the free version
    // Thir repo can be used to return dummy translations
    public class DummyShakespeareTranslatorRepository : HttpRepositoryBase, IShakespeareTranslatorRepository
    {
        public DummyShakespeareTranslatorRepository()
            : base(new HttpClientHandler())
        {
        }

        public DummyShakespeareTranslatorRepository(HttpMessageHandler messageHandler)
            : base(messageHandler)
        {
        }

        public Task<Translation> Translate(string text)
        {
            return Task.FromResult(new Translation(text, "translated"));
        }
    }
}