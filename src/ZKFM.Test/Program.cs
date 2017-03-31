using System;
using ZKFM.Core.Services;

namespace ZKFM.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new NetEaseMusicService();

            var searchresult = service.Search("成都");
            Console.WriteLine(searchresult.Result.Total);



            Console.ReadKey();
        }
    }
}