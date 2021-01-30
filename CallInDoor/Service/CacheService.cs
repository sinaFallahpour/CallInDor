using Microsoft.Extensions.Caching.Memory;
using Service.Interfaces.InMemoryCache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService : ICacheService
    {

        private readonly MemoryCache _cahce = new MemoryCache(new MemoryCacheOptions { });

        public Task<string> GetCacheValue(string Key)
        {
            return Task.FromResult(_cahce.Get<string>(Key));
        }


        public Task SetCacheValue(string key, string value)
        {
            //new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove)
            _cahce.Set(key, value);
            return Task.CompletedTask;
        }

        public Task RemoveCacheVaue(string key)
        {
            _cahce.Remove(key);
            return Task.CompletedTask;
        }




 






    }
}
