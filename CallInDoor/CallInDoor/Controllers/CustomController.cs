using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.InMemoryCache;

namespace CallInDoor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : BaseControlle
    {
        //private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly ICacheService _cacheService;


        public CustomController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }





        //public CacheController(ICacheService cacheService, ConnectionMultiplexer connectionMultiplexer)
        //{
        //    _cacheService = cacheService;
        //    _connectionMultiplexer = connectionMultiplexer;
        //}


        [HttpGet("GetCache/{key}")]
        public async Task<ActionResult> GetCacheValue(string key)
        {
            var cacheValue = await _cacheService.GetCacheValue("salam");
            return string.IsNullOrEmpty(cacheValue) ? (ActionResult)NotFound() : Ok(cacheValue);
        }




        [HttpGet("SetCacheValue")]
        public async Task<ActionResult> SetCacheValue(string key, string val)
        {
            await _cacheService.SetCacheValue("salam", val);
            var sas = await _cacheService.GetCacheValue("salam");

            return Ok();
        }



        //[HttpPost("IncrementValue")]
        //public async Task<ActionResult> IncrementValue([FromBody] mode1 request)
        //{
        //    await _cacheService.Increment(request.Key, request.Value);

        //    var cacheValue = await _cacheService.GetCacheValue(request.Key);
        //    return string.IsNullOrEmpty(cacheValue) ? (ActionResult)NotFound() : Ok(cacheValue);
        //}



        /// <summary>
        /// decrement
        /// </summary>
        ///// <returns></returns>
        //[HttpPost("DecremenmtValue")]
        //public async Task<ActionResult> DecremenmtValue()
        //{
        //    var _db = _connectionMultiplexer.GetDatabase();
        //    _db.StringSet("decrement", 1);
        //    await _db.StringDecrementAsync("decrement", 50);
        //    await _db.StringDecrementAsync("decrement");

        //    string ass = await _db.StringGetAsync("decrement");
        //    return Ok(ass);
        //}





        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string key)
        {
            await _cacheService.RemoveCacheVaue(key);
            return Ok();
        }






        ///// <summary>
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("expireValue")]
        //public async Task<ActionResult> expireValue()
        //{

        //    var _db = _connectionMultiplexer.GetDatabase();
        //    await _db.StringSetAsync("time", 1);
        //    await _db.KeyExpireAsync("time", DateTime.Now.AddMinutes(1));
        //    return Ok();
        //}

    }
}
