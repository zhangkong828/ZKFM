using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ZKFM.Core.Cache
{
    public class BaseCache<T>
    {
        protected static object synObj = new object();
        public ConcurrentDictionary<string, Tuple<T, DateTime>> innerData = null;
        public int ThresholdCount = 200;//阈值 超过该值 则开始清理过期cache
        public int ExpireTime = 60;//过期时间 单位：分钟

        public BaseCache()
        {
            innerData = new ConcurrentDictionary<string, Tuple<T, DateTime>>();
        }

        public bool Set(string key, T val)
        {
            try
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
                            }
                        }
                    }
                    return innerData.TryAdd(key, new Tuple<T, DateTime>(val, DateTime.Now));
                }
            }
            catch
            {
                return false;
            }
        }

        public T Get(string key)
        {
            T result = default(T);
            Tuple<T, DateTime> tuple = default(Tuple<T, DateTime>);
            try
            {
                if (innerData.TryGetValue(key, out tuple))
                {
                    //var expire = tuple.Item2;
                    //var nowtime = FormatHelper.ConvertDateTimeInt(DateTime.Now);
                    //if (expire < nowtime)
                    //{
                    //    //过期删除
                    //    innerData.Remove(key);
                    //}
                    //else
                    //    result = tuple.Item1;
                }
            }
            catch  { }
            return result;

        }
    }
}
