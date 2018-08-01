using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZKFM.Core.Cache;
using ZKFM.Core.Services;

namespace ZKFM.FM.Controllers
{
    [Produces("application/json")]
    [Route("API/NetEaseMusic/V1")]
    public class NetEaseController : Controller
    {
        public NetEaseMusicService service;

        public NetEaseController()
        {
            service = new NetEaseMusicService();
        }

        //匹配路由  'API/NetEaseMusic/V1/Get/{id}'
        [HttpGet("Get/{id}")]
        public IActionResult GetMusic(int id)
        {
            if (id <= 0)
                return new JsonResult(new { code = -1, msg = "参数异常" });
            var result = NetEaseMusicCache.Get(id.ToString());
            if (result == null)
            {
                result = service.GetDetial(id).Result;
                if (result != null)
                {
                    result.Src = service.GetMusicUrl(id).Result;
                    result.Lrc = service.GetLyric(id).Result;
                    NetEaseMusicCache.Set(id.ToString(), result);
                }
            }

            if (result == null)
                return new JsonResult(new { code = -1, msg = "获取失败" });
            else
                return new JsonResult(new { code = 0, data = result });
        }

        //匹配路由  'API/NetEasemMusic/V1/Search/{key}'
        [HttpGet("Search/{key}")]
        public IActionResult SearchMusic(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new JsonResult("参数异常");
            var result = NetEaseMusicSearchResultCache.Get(key);
            if (result == null || result.Total <= 0)
            {
                result = service.Search(key).Result;

                NetEaseMusicSearchResultCache.Set(key, result);
            }
            return new JsonResult(result);
        }
    }
}