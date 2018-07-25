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

            //搜索
            //var searchresult = service.Search("李白").Result;
            //Console.WriteLine($"Total:{searchresult.Total}");

            //详情
            //var detial = service.GetDetial(27678655).Result;
            //Console.WriteLine(detial?.Pic);

            var url = service.GetMusicUrl(27678655).Result;
            Console.WriteLine(url);
            //歌词
            //var lrc = service.GetLyric(27678655).Result;
            //Console.WriteLine(lrc.Text);

            Console.ReadKey();
        }
    }
}