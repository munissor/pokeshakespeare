using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using Munisso.PokeShakespeare.Models;

namespace Munisso.PokeShakespeare.E2ETests
{
    [TestFixture]
    public class PokemonControllerTests
    {
        string ENDPOINT;

        [SetUp]
        public void Setup()
        {
            var testHost = Environment.GetEnvironmentVariable("TEST_HOST") ?? "localhost:5000";
            ENDPOINT = $"http://{testHost}/pokemon";
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
                Assert.AreEqual(pokemon, data.Name);
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