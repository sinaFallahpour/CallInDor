using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.InMemoryCache
{
    public interface ICacheService
    {


       //get from cache
        public Task<string> GetCacheValue(string Key);
       
       //set to chece
        public Task SetCacheValue(string key, string value);

        //remove from cache
        public Task RemoveCacheVaue(string key);


        //Task Increment(string key, int value);


    }
}
