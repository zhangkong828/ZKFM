﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZKFM.Core.Infrastructure;
using ZKFM.Core.Models;

namespace ZKFM.Core.Services
{
    public class NetEaseMusicService : IMusicService<NetEaseMusic, NetEaseMusicSearchResult>
    {


        //1. 搜索功能
        //    - 请求地址： http://music.163.com/api/search/get
        //    - 请求方法：post
        //    - 请求参数：
        //        * `s`: 搜索词
        //        * `limit`: 分页所用， 返回的条数(默认30)
        //        * `offset`: 偏移量,用于分页(默认0)
        //        * `type`: 搜索的种类，(默认1)[1 单曲][10 专辑][100 歌手][1000 歌单][1002 用户]


        /// <summary>
        /// 搜索功能
        /// </summary>
        public async Task<NetEaseMusicSearchResult> Search(string key)
        {
            return await Search(key);
        }

        /// <summary>
        /// 搜索功能
        /// </summary>
        public async Task<NetEaseMusicSearchResult> Search(string s, int limit = 10, int offset = 0, int type = 1)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("搜索关键字不能为空！");
            var url = $"http://music.163.com/api/search/get";
            var json = await HttpHelper.Request(url, new { s = s, limit = limit, offset = offset, type = type }, "post");
            return null;
        }


        //2. 歌曲详情
        //    - 请求地址： http://music.163.com/api/song/detail?ids=29775505,300587
        //    - 请求参数：
        //        * `ids`: 歌曲对应的ID  也可以是多个

        /// <summary>
        /// 歌曲详情
        /// </summary>
        public async Task<NetEaseMusic> GetDetial(int id)
        {
            var result = await GetDetialMulti(id);
            return result != null ? result[0] : null;
        }

        /// <summary>
        /// 歌曲详情
        /// </summary>
        public async Task<List<NetEaseMusic>> GetDetialMulti(params int[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new ArgumentException("id不能为空！");
            var url = $"http://music.163.com/api/song/detail";
            var detail = await HttpHelper.Request(url, new { ids = string.Join(",", ids) });
            throw new NotImplementedException();
        }


        //3. 获取歌词
        //    - 请求地址：http://music.163.com/api/song/lyric?id=29775505
        //    - 请求参数：
        //        * `id`: 获取歌词对应的歌曲ID

        /// <summary>
        /// 获取歌词
        /// </summary>
        public async Task<NetEaseMusic> GetLyric(int id)
        {
            if (id == 0)
                throw new ArgumentException("id不能为空！");
            var url = $"http://music.163.com/api/song/lyric";
            var lrc = await HttpHelper.Request(url, new { id = id });
            throw new NotImplementedException();
        }


        //4.  歌单（网友精选碟） hot
        //    - 请求地址：http://music.163.com/api/playlist/list
        //    - 请求参数：
        //        * `cat`: 种类(默认hot)，其它参数，参考：http://music.163.com/#/discover/playlist中的分类名称
        //        * `order`: 排序规则（默认为hot）
        //        * `offset`: 偏移量,用于分页(默认0)
        //        * `total`: 该分类下总数目
        //        * `limit`: 分页所用， 返回的条数(默认50)
        public async Task<NetEaseMusic> GetHot()
        {
            throw new NotImplementedException();
        }
    }
}
