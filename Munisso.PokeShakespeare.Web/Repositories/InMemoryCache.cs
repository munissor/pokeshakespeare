using System;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Munisso.PokeShakespeare.Repositories
{
    public class InMemoryCache : ICache
    {
        public InMemoryCache() : this(MemoryCache.Default)
        {
        }

        public InMemoryCache(ObjectCache cache)
        {
            this.Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private ObjectCache Cache { get; }


        public Task Set<T>(string key, T content, TimeSpan offset)
        {
            var cacheItem = new CacheItem(key, content);
            var policy = new CacheItemPolicy();
            var date = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var off = new DateTimeOffset(date, offset);
            policy.AbsoluteExpiration = off;
            try
            {
                Cache.Set(cacheItem, policy);
            }
            catch (Exception)
            {
                // we don't want to fail on caching. 
                // in prod this would log somewhere so that we can monitor
            }

            return Task.CompletedTask;
        }

        public Task<T> Get<T>(string key)
        {
            T result;
            try
            {
                result = (T)Cache.Get(key);
            }
            catch (Exception)
            {
                // let's not fail on caching.
                // in prod this would log somewhere so that we can monitor
                result = default(T);
            }
            return Task.FromResult(result);
        }
    }
}