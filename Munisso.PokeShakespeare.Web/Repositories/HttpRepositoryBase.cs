using System.Net.Http;

namespace Munisso.PokeShakespeare.Repositories
{
    public class HttpRepositoryBase
    {
        private HttpMessageHandler messageHandler;

        public HttpRepositoryBase(HttpMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler ?? throw new System.ArgumentNullException(nameof(messageHandler));
        }

        public HttpClient GetClient()
        {
            return new HttpClient(this.messageHandler);
        }
    }
}