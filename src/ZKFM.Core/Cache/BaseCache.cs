using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ZKFM.Core.Cache
{
    public class BaseCache<T>
    {
        protected static object synObj = new object();
        public static ConcurrentDictionary<string, Tuple<T, DateTime>> innerData = null;
        public static int ThresholdCount = 1000;//阈值 超过该值 则开始清理过期cache
        public static int ExpireTime = 60;//过期时间 单位：分钟

        public BaseCache()
        {
            innerData = new ConcurrentDictionary<string, Tuple<T, DateTime>>();
        }

        public static  bool Set(string key, T val)
        {
            if (innerData.ContainsKey(key))
            {
                return innerData.TryUpdate(key, new Tuple<T, DateTime>(val, DateTime.Now), innerData[key]);
            }
            else
            {
                if (innerData.Count >= ThresholdCount)
                {
                    lock (synObj)
                    {
                        if (innerData.Count >= ThresholdCount)
                        {
                            //清理过期cache
                            TryClear();
                        }
                    }
                }
                return innerData.TryAdd(key, new Tuple<T, DateTime>(val, DateTime.Now));
            }
        }

        public static T Get(string key)
        {
            Tuple<T, DateTime> tuple = default(Tuple<T, DateTime>);
            innerData.TryGetValue(key, out tuple);
            return tuple.Item1;

        }


        private static void TryClear()
        {
            var val = default(Tuple<T, DateTime>);
            var copyData = new ConcurrentDictionary<string, Tuple<T, DateTime>>(innerData);
            foreach (var item in copyData)
            {
                if (item.Value.Item2.AddMinutes(ExpireTime) < DateTime.Now)
                {
                    innerData.TryRemove(item.Key, out val);
                }
            }
        }
    }
}
