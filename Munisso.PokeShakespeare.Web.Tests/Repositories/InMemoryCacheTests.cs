using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;


namespace Munisso.PokeShakespeare.Repositories
{
    [TestFixture]
    public class InMemoryCacheTests
    {
        private Mock<ObjectCache> cacheMock;
        private InMemoryCache cache;

        [SetUp]
        public void Setup()
        {
            this.cacheMock = new Mock<ObjectCache>();
            this.cache = new InMemoryCache(cacheMock.Object);
        }

        [Test]
        public void Test_Contructor_Missing_Cache()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new InMemoryCache(null);
            });
        }

        [Test]
        public async Task Test_Set_Correct()
        {
            string data = "data";
            await this.cache.Set("key", data, new TimeSpan(1, 0, 0));
            this.cacheMock.Verify(m => m.Set(
                It.Is<CacheItem>(ci => ci.Key == "key" && ci.Value.Equals(data)),
                It.IsAny<CacheItemPolicy>()), Times.Once
            );
        }

        [Test]
        public async Task Test_Set_Failure()
        {
            string data = "data";
            this.cacheMock.Setup(m => m.Set(It.IsAny<CacheItem>(), It.IsAny<CacheItemPolicy>())).Throws(new Exception("Cache failure"));
            await this.cache.Set("key", data, new TimeSpan(1, 0, 0));
            this.cacheMock.Verify(m => m.Set(
                It.Is<CacheItem>(ci => ci.Key == "key" && ci.Value.Equals(data)),
                It.IsAny<CacheItemPolicy>()), Times.Once
            );
        }

        [Test]
        [TestCase("key", ExpectedResult = "value")]
        [TestCase("not_esisting", ExpectedResult = null)]
        public async Task<String> Test_Get_Correct(string key)
        {
            this.cacheMock.Setup(m => m.Get("key", It.IsAny<String>())).Returns("value");
            this.cacheMock.Setup(m => m.Get("not_esisting", It.IsAny<String>())).Returns(null);

            var value = await this.cache.Get<string>(key);
            this.cacheMock.Verify(m => m.Get(key, It.IsAny<String>()), Times.Once);
            return value;
        }

        [Test]
        public async Task Test_Get_Failure()
        {
            string data = null;
            this.cacheMock.Setup(m => m.Get("key", It.IsAny<String>())).Throws(new Exception("Cache failure"));
            var value = await this.cache.Get<string>("key");
            this.cacheMock.Verify(m => m.Get("key", It.IsAny<String>()), Times.Once);
            Assert.AreEqual(data, value);
        }
    }
}