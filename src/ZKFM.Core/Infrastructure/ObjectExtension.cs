using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace ZKFM.Core.Infrastructure
{
    public static class ObjectExtension
    {
        public static string ParseQueryString(this object obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(obj))));
        }
    }
}
