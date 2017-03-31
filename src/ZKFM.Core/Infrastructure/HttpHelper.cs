using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZKFM.Core.Infrastructure
{
    public static class HttpHelper
    {

        public static async Task<string> Request(string url, object obj, string method = "GET")
        {
            var result = string.Empty;
            var requestType = method.Trim().ToUpper();
            var param = "";
            if (obj != null)
            {
                //将对象转换成QueryString形式的字符串
                param = obj.ParseQueryString();
            }

            if (requestType == "GET" && !string.IsNullOrEmpty(param))
            {
                //如果是get请求 拼接url
                var sep = url.Contains('?') ? "&" : "?";
                url += sep + param;
            }

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = requestType;
            request.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.152 Safari/537";
            request.ContinueTimeout = 1000 * 10;
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";

            if (requestType == "POST")
            {
                //post请求  写入数据
                byte[] bs = Encoding.UTF8.GetBytes(param);
                request.ContentType = "application/x-www-form-urlencoded";
                using (Stream reqStream = await request.GetRequestStreamAsync())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }
            }
            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                result = GetResponseBody(response);
            }
            return result;
        }

        private static string GetResponseBody(HttpWebResponse response)
        {
            string responseBody = string.Empty;
            if (response.Headers[HttpResponseHeader.ContentEncoding].ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else if (response.Headers[HttpResponseHeader.ContentEncoding].ToLower().Contains("deflate"))
            {
                using (DeflateStream stream = new DeflateStream(
                    response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            return responseBody;
        }


        private static string ParseQueryString(this object obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(obj))));
        }


    }
}
