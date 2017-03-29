using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZKFM.Web.Controllers
{
    [Produces("application/json")]
    [Route("API/NetEaseMusic/v1")]
    public class NetEaseController : Controller
    {
        //匹配路由  'API/NetEaseMusic/v1/Get/{id}'
        [HttpGet("Get/{id}")]
        public IActionResult GetMusic(string id)
        {
            return new JsonResult(id);
        }

        //匹配路由  'API/NetEasemMusic/v1/Search/{key}'
        [HttpGet("Search/{key}")]
        public IActionResult SearchMusic(string key)
        {
            return new JsonResult(key);
        }
    }
}