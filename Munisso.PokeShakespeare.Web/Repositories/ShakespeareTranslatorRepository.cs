using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Repositories.Models.Shakespeare;

namespace Munisso.PokeShakespeare.Repositories
{
    public class ShakespeareTranslatorRepository : HttpRepositoryBase, IShakespeareTranslatorRepository
    {
        public const string API_URL = "https://api.funtranslations.com/translate/shakespeare.json";

        private string ApiKey;

        public ShakespeareTranslatorRepository()
            : this(null)
        {
        }

        public ShakespeareTranslatorRepository(string apiKey)
            : this(apiKey, new HttpClientHandler())
        {
        }

        public ShakespeareTranslatorRepository(string apiKey, HttpMessageHandler messageHandler)
            : base(messageHandler)
        {
            ApiKey = apiKey;
        }

        public async Task<Translation> Translate(string text)
        {
            using (var httpClient = base.GetClient())
            {
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    httpClient.DefaultRequestHeaders.Add("X-Funtranslations-Api-Secret", this.ApiKey);
                }

                var req = new StringContent($"text={text}", Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(API_URL, req);

                // Validate that the API call succeeded
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error calling the remote endpoint");
                }

                // extract the data we need
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Response>(content);

                // we assume that if the request is successful the response is well formed
                return new Translation(data.Contents.Text, data.Contents.Translated);
            }
        }
    }
}