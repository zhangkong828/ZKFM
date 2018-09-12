using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZKFM.Core.Infrastructure;
using ZKFM.Core.Models;
using ZKFM.Core.Services.DataFormatter;

namespace ZKFM.Core.Services
{
    public class NetEaseMusicService : IMusicService<NetEaseMusic, NetEaseMusicSearchResult>
    {

        public NetEaseMusicService()
        {

        }

        /// <summary>
        ///1. 搜索功能
        ///    - 请求地址： http://music.163.com/weapi/search/get
        ///    - 请求方法：post
        ///    - 请求参数：
        ///        * `s`: 搜索词
        ///        * `limit`: 分页所用， 返回的条数(默认30)
        ///        * `offset`: 偏移量,用于分页(默认0)
        ///        * `type`: 搜索的种类，(默认1)[1 单曲][10 专辑][100 歌手][1000 歌单][1002 用户]
        /// </summary>
        public async Task<NetEaseMusicSearchResult> Search(string key, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 10 ? 10 : pageSize;
            var limit = pageSize;
            var offset = (pageIndex - 1) * pageSize;
            return await Search(key, limit, offset, 1);
        }

        /// <summary>
        /// 搜索功能
        /// </summary>
        public async Task<NetEaseMusicSearchResult> Search(string s, int limit, int offset, int type)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("搜索关键字不能为空！");
            var url = $"http://music.163.com/weapi/search/get";
            var json = await HttpHelper.NetEaseRequest(url, new { s = s, limit = limit, offset = offset, type = type, csrf_token = "" }, "POST");
            return NetEaseMusicDataFormatter.FormatSearchResult(json);
        }



        /// <summary>
        ///2. 歌曲详情
        ///    - 请求地址： http://music.163.com/weapi/v3/song/detail?ids=[29775505,300587]
        ///    - 请求方法：post
        ///    - 请求参数：
        ///       * `c`: [{"id":id}]
        ///       * `ids`: 歌曲对应的ID  
        /// </summary>
        public async Task<NetEaseMusic> GetDetial(int id)
        {
            int tryCount = 3;
            TryPost:
            var url = $"http://music.163.com/weapi/v3/song/detail";
            var json = await HttpHelper.NetEaseRequest(url, new { ids = string.Join(",", id).AddBrackets(), c = new List<object>() { new { id = id } }.ToJson() }, "POST");
            var result = NetEaseMusicDataFormatter.FormatDetialResult(json);
            if (result == null)
            {
                if (tryCount > 0)
                {
                    Thread.Sleep(500);
                    tryCount--;
                    goto TryPost;
                }
            }

            if (result!=null)
            {
                result.Src = await GetMusicUrl(id);
                result.Lrc = await GetLyric(id);
            }
            return result;
        }


        /// <summary>
        ///3.  获取歌词  nolyric表示无歌词，uncollected表示暂时无人提交歌词
        ///    - 请求地址：http://music.163.com/api/song/lyric"
        ///    - 请求方法：get
        ///    - 请求参数：
        ///        * `os`: osx
        ///        * `id`: 歌曲ID
        ///        * `lv`: -1
        ///        * `kv`: -1
        ///        * `tv`: -1
        /// </summary>
        public async Task<Lrc> GetLyric(int id)
        {
            if (id == 0)
                throw new ArgumentException("id不能为空！");
            var url = $"http://music.163.com/api/song/lyric";
            var json = await HttpHelper.NetEaseRequest(url, new { os = "osx", id = id, lv = -1, kv = -1, tv = -1 });
            return NetEaseMusicDataFormatter.FormatLyricResult(json);
        }


        /// <summary>
        ///4. 歌曲url
        //    - 请求地址：http://music.163.com/weapi/song/enhance/player/url
        //    - 请求参数：
        //        * `ids`: 歌曲对应的ID
        //        * `br`: 320000 || 999000
        //        * `csrf_token`: 登录后的token
        /// </summary>
        public async Task<string> GetMusicUrl(int id)
        {
            int tryCount = 3;
            TryPost:
            var url = $"http://music.163.com/weapi/song/enhance/player/url";
            var json = await HttpHelper.NetEaseRequest(url, new { ids = string.Join(",", id).AddBrackets(), br = 999000 }, "POST");
            var result = NetEaseMusicDataFormatter.FormatUrlResult(json);
            if (string.IsNullOrWhiteSpace(result))
            {
                if (tryCount > 0)
                {
                    Thread.Sleep(500);
                    tryCount--;
                    goto TryPost;
                }
            }
            return result;
        }

    }
}
