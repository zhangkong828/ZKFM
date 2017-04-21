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
            var searchresult = service.Search("李白").Result;
            Console.WriteLine($"Total:{searchresult.Total}");
            foreach (var item in searchresult.Datas)
            {
                var msg = $"Id:{item.Id},Name:{item.Name},Author:{item.Author}";
                Console.WriteLine(msg);
            }

            var detialresult = service.GetDetialMulti(27678655, 436514312);
            Console.WriteLine(detialresult.Result.Count);

            Console.ReadKey();
        }
    }
}