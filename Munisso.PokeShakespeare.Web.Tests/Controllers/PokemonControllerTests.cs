using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Services;
using Microsoft.AspNetCore.Mvc;

namespace Munisso.PokeShakespeare.Controllers
{
    public class PokemonControllerTests
    {
        private Mock<IPokeShakespeareService> pokeShakespeareServiceMock;
        private PokemonController pokemonController;

        [SetUp]
        public void Setup()
        {
            this.pokeShakespeareServiceMock = new Mock<IPokeShakespeareService>();
            this.pokemonController = new PokemonController(pokeShakespeareServiceMock.Object);
        }

        [Test]
        public void Test_Constructor_Missing_Service()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PokemonController(null);
            });
        }

        [Test]
        public async Task Test_Get_Ok()
        {
            string pokemon = "pikachu";
            string description = "shakespeare description";

            this.pokeShakespeareServiceMock.Setup(m => m.GetPokemonDescription(pokemon)).ReturnsAsync(new ShakespeareDescription(pokemon, description));

            var response = (OkObjectResult)await this.pokemonController.Get(pokemon);
            this.pokeShakespeareServiceMock.Verify(m => m.GetPokemonDescription(It.IsAny<string>()), Times.Once);
            var value = (ShakespeareDescription)response.Value;
            Assert.AreEqual(pokemon, value.Name);
            Assert.AreEqual(description, value.Description);
        }

        [Test]
        public async Task Test_Get_Invalid()
        {
            string pokemon = "invalid";

            this.pokeShakespeareServiceMock.Setup(m => m.GetPokemonDescription(pokemon)).Throws(new ArgumentException("Does not exist"));

            var response = await this.pokemonController.Get(pokemon);
            this.pokeShakespeareServiceMock.Verify(m => m.GetPokemonDescription(It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOf(typeof(NotFoundResult), response);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Test_Get_Empty(string pokemon)
        {
            var response = await this.pokemonController.Get(pokemon);
            this.pokeShakespeareServiceMock.Verify(m => m.GetPokemonDescription(It.IsAny<string>()), Times.Never);
            Assert.IsInstanceOf(typeof(BadRequestResult), response);
        }
    }
}