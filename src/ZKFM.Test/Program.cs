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
            var searchresult = service.Search("成都").Result;
            Console.WriteLine($"Total:{searchresult.Total}");
            foreach (var item in searchresult.Datas)
            {
                Console.WriteLine($"Id:{item.Id},Name:{item.Name},Author:{item.Author}");
            }


            //var searchresult = service.GetDetial(436514312);
            //Console.WriteLine(searchresult.Result.Id);

            Console.ReadKey();
        }
    }
}