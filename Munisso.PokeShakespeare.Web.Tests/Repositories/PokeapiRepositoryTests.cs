using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace Munisso.PokeShakespeare.Repositories
{
    [TestFixture]
    public class PokeapiRepositoryTests
    {
        private const string RESPONSE_ENGLISH = @"
        {
            ""name"": ""pokemon"",
            ""flavor_text_entries"": [
                {
                    ""flavor_text"": ""First description"",
                    ""language"": {
                        ""name"": ""en"",
                        ""url"": ""https://pokeapi.co/api/v2/language/9/""
                    }
                },
                {
                    ""flavor_text"": ""Second description"",
                    ""language"": {
                        ""name"": ""en"",
                        ""url"": ""https://pokeapi.co/api/v2/language/9/""
                    }
                }
            ]    
        }
        ";

        private const string RESPONSE_JAPANESE = @"
        {
            ""name"": ""pokemon"",
            ""flavor_text_entries"": [
                {
                    ""flavor_text"": ""Japanese description"",
                    ""language"": {
                        ""name"": ""ja"",
                        ""url"": ""https://pokeapi.co/api/v2/language/9/""
                    }
                }
            ]    
        }
        ";

        private MockHttpMessageHandler mockHttp;
        private PokeapiRepository repository;

        [SetUp]
        public void Setup()
        {
            this.mockHttp = new MockHttpMessageHandler();
            this.repository = new PokeapiRepository(mockHttp);
        }

        [Test]
        public void Test_Contructor_Missing_Handler()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PokeapiRepository(null);
            });
        }

        [Test]
        public async Task Test_GetDescription_Correct()
        {
            this.mockHttp.When("https://pokeapi.co/api/v2/pokemon-species/pokemon").Respond(HttpStatusCode.OK, "application/json", RESPONSE_ENGLISH);
            var descr = await this.repository.GetDescription("pokemon");
            Assert.AreEqual("pokemon", descr.Name);
            Assert.AreEqual("First description", descr.Description);
        }

        [Test]
        public void Test_GetDescription_NotFound()
        {
            this.mockHttp.When("https://pokeapi.co/api/v2/pokemon-species/pokemon").Respond(HttpStatusCode.NotFound, "application/json", "");
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await this.repository.GetDescription("pokemon");
            }, "The Pokemon pokemon doesn't exist.");
        }

        [Test]
        public void Test_GetDescription_ApiError()
        {
            this.mockHttp.When("https://pokeapi.co/api/v2/pokemon-species/pokemon").Respond(HttpStatusCode.InternalServerError, "application/json", "");
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await this.repository.GetDescription("pokemon");
            }, "Error calling the remote endpoint");
        }

        [Test]
        public void Test_GetDescription_NoEnglish()
        {
            this.mockHttp.When("https://pokeapi.co/api/v2/pokemon-species/pokemon").Respond(HttpStatusCode.OK, "application/json", RESPONSE_JAPANESE);
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await this.repository.GetDescription("pokemon");
            }, "There is no English description for pokemon");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        public void Test_GetDescription_Empty(string pokemonName)
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await this.repository.GetDescription(pokemonName);
            });
        }
    }
}