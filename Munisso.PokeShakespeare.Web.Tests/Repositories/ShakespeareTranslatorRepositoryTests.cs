using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace Munisso.PokeShakespeare.Repositories
{
    [TestFixture]
    public class ShakespeareTranslatorRepositoryTests
    {
        private const string RESPONSE_VALID = @"
        {
            ""contents"": {
                ""text"": ""text"",
                ""translated"": ""translated""
            }
        }
        ";

        private MockHttpMessageHandler mockHttp;
        private ShakespeareTranslatorRepository repository;

        [SetUp]
        public void Setup()
        {
            this.mockHttp = new MockHttpMessageHandler();
            this.repository = new ShakespeareTranslatorRepository(null, mockHttp);
        }

        [Test]
        public void Test_Contructor_Missing_Handler()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ShakespeareTranslatorRepository(null, null);
            });
        }


        [Test]
        public async Task Test_Translate_Correct()
        {
            this.mockHttp.When(ShakespeareTranslatorRepository.API_URL).Respond(HttpStatusCode.OK, "application/json", RESPONSE_VALID);
            var translation = await this.repository.Translate("text");
            Assert.AreEqual("text", translation.Original);
            Assert.AreEqual("translated", translation.Translated);
        }

        [Test]
        public async Task Test_Translate_WithKey()
        {
            var key = "testkey";
            this.repository = new ShakespeareTranslatorRepository(key, mockHttp);
            this.mockHttp.Expect(ShakespeareTranslatorRepository.API_URL).WithHeaders("X-Funtranslations-Api-Secret", key).Respond(HttpStatusCode.OK, "application/json", RESPONSE_VALID);
            var translation = await this.repository.Translate("text");
            this.mockHttp.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void Test_Translate_ApiError()
        {
            this.mockHttp.When(ShakespeareTranslatorRepository.API_URL).Respond(HttpStatusCode.InternalServerError, "application/json", "");
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await this.repository.Translate("some text");
            }, "Error calling the remote endpoint");
        }
    }
}