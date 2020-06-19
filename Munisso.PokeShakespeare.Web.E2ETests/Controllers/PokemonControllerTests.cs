using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Munisso.PokeShakespeare.Models;

namespace Munisso.PokeShakespeare.E2ETests
{
    [TestFixture]
    public class PokemonControllerTests
    {
        const string ENDPOINT = "http://localhost:5000/pokemon";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_Get_Ok()
        {
            string pokemon = "pikachu";
            string url = $"{ENDPOINT}/{pokemon}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ShakespeareDescription>(content);
                Assert.AreEqual(pokemon, data.Pokemon);
                Assert.AreEqual("At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms.", data.Description);
            }
        }

        [Test]
        public async Task Test_Get_Missing()
        {
            string pokemon = "invalid";
            string url = $"{ENDPOINT}/{pokemon}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}