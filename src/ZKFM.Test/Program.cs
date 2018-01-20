using System;
using System.Collections.Generic;
using ZKFM.Core.Services;

namespace ZKFM.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new NetEaseMusicService();

            ////搜索
            //var searchresult = service.Search("李白").Result;
            //Console.WriteLine($"Total:{searchresult.Total}");

            ////详情
            //var detial = service.GetDetialMulti(27678655, 436514312).Result;
            //Console.WriteLine(detial.Count);

            var url = service.GetMusicUrl(27678655).Result;

            //歌词
            var lrc = service.GetLyric(436514312).Result;
            Console.WriteLine(lrc);

            Console.ReadKey();
        }
    }
}