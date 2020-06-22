using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Munisso.PokeShakespeare.Models;
using Munisso.PokeShakespeare.Repositories;

namespace Munisso.PokeShakespeare.Services
{
    public class PokeShakespeareServiceTests
    {
        private Mock<ICache> cacheMock;
        private Mock<IPokeapiRepository> pokeapiRepositoryMock;
        private Mock<IShakespeareTranslatorRepository> shakespeareTranslatorRepositoryMock;
        private IPokeShakespeareService service;

        [SetUp]
        public void Setup()
        {
            this.cacheMock = new Mock<ICache>();
            this.pokeapiRepositoryMock = new Mock<IPokeapiRepository>();
            this.shakespeareTranslatorRepositoryMock = new Mock<IShakespeareTranslatorRepository>();
            this.service = new PokeShakespeareService(pokeapiRepositoryMock.Object, shakespeareTranslatorRepositoryMock.Object, cacheMock.Object);
        }

        [Test]
        public void Test_Constructor_Missing_Pokeapi()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PokeShakespeareService(null, shakespeareTranslatorRepositoryMock.Object, cacheMock.Object);
            });
        }

        [Test]
        public void Test_Constructor_Missing_ShakespeareTranslator()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PokeShakespeareService(pokeapiRepositoryMock.Object, null, cacheMock.Object);
            });
        }


        [Test]
        public void Test_Constructor_Missing_Cache()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PokeShakespeareService(pokeapiRepositoryMock.Object, shakespeareTranslatorRepositoryMock.Object, null);
            });
        }


        [Test]
        public async Task Test_GetPokemonDescription_NoCache()
        {
            string pokemon = "pikachu";
            string description = "pikachu description";
            string shakespeare_description = "pikachu shakespeare description";

            this.pokeapiRepositoryMock.Setup(m => m.GetDescription(pokemon)).Returns(Task.FromResult(new PokemonDescription(pokemon, description)));
            this.shakespeareTranslatorRepositoryMock.Setup(m => m.Translate(description)).Returns(Task.FromResult(new Translation(description, shakespeare_description)));

            var result = await this.service.GetPokemonDescription(pokemon);

            this.pokeapiRepositoryMock.Verify(m => m.GetDescription(pokemon), Times.Once);
            this.shakespeareTranslatorRepositoryMock.Verify(m => m.Translate(description), Times.Once);

            Assert.AreEqual(result.Name, pokemon);
            Assert.AreEqual(result.Description, shakespeare_description);
        }

        [Test]
        public async Task Test_GetPokemonDescription_Cached()
        {
            string pokemon = "pikachu";
            string shakespeare_description = "pikachu shakespeare description";
            string cache_key = $"description:{pokemon}";

            this.cacheMock.Setup(m => m.Get<ShakespeareDescription>(cache_key)).Returns(Task.FromResult(new ShakespeareDescription(pokemon, shakespeare_description)));

            var result = await this.service.GetPokemonDescription(pokemon);

            this.cacheMock.Verify(m => m.Get<ShakespeareDescription>(cache_key), Times.Once);
            this.pokeapiRepositoryMock.Verify(m => m.GetDescription(pokemon), Times.Never);
            this.shakespeareTranslatorRepositoryMock.Verify(m => m.Translate(It.IsAny<string>()), Times.Never);

            Assert.AreEqual(result.Name, pokemon);
            Assert.AreEqual(result.Description, shakespeare_description);
        }

        [Test]
        public void Test_GetPokemonDescription_PokeapiFailure()
        {
            string pokemon = "pikachu";
            string description = "pikachu description";
            string shakespeare_description = "pikachu shakespeare description";

            this.pokeapiRepositoryMock.Setup(m => m.GetDescription(pokemon)).Throws(new Exception("Failure"));
            this.shakespeareTranslatorRepositoryMock.Setup(m => m.Translate(description)).Returns(Task.FromResult(new Translation(description, shakespeare_description)));

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await this.service.GetPokemonDescription(pokemon);
            }, "Failure");

            this.pokeapiRepositoryMock.Verify(m => m.GetDescription(pokemon), Times.Once);
            this.shakespeareTranslatorRepositoryMock.Verify(m => m.Translate(description), Times.Never);
            this.cacheMock.Verify(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Never);
        }

        [Test]
        public void Test_GetPokemonDescription_TranslatorFailure()
        {
            string pokemon = "pikachu";
            string description = "pikachu description";

            this.pokeapiRepositoryMock.Setup(m => m.GetDescription(pokemon)).Returns(Task.FromResult(new PokemonDescription(pokemon, description)));
            this.shakespeareTranslatorRepositoryMock.Setup(m => m.Translate(description)).Throws(new Exception("Failure"));

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await this.service.GetPokemonDescription(pokemon);
            }, "Failure");

            this.pokeapiRepositoryMock.Verify(m => m.GetDescription(pokemon), Times.Once);
            this.shakespeareTranslatorRepositoryMock.Verify(m => m.Translate(description), Times.Once);
            this.cacheMock.Verify(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Never);
        }
    }
}