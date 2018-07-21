using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZKFM.Core.Infrastructure
{
    public static class ObjectExtension
    {
        public static string ParseQueryString(this object obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(obj))));
        }

        private static readonly JsonSerializerSettings _ignoreSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public static string ToJson(this object obj, Formatting formatting = Formatting.None, bool ignoreNull = false)
        {
            return JsonConvert.SerializeObject(obj, formatting, ignoreNull ? _ignoreSettings : null);
        }
    }
}
