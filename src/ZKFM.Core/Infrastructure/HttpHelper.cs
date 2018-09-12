using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ZKFM.Core.Infrastructure
{
    public static class HttpHelper
    {
        private static readonly HttpClient _httpClient;
        private static readonly CookieContainer _cookies;

        static HttpHelper()
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12;

            //_cookies = new CookieContainer();
            //_httpClient = new HttpClient(new HttpClientHandler()
            //{
            //    UseCookies = true,
            //    CookieContainer = _cookies
            //});
            //_httpClient.DefaultRequestHeaders.Referrer = new System.Uri("http://music.163.com");
            //_httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.152 Safari/537");
            //_httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
        }

        public static async Task<string> NetEaseRequest(string url, object obj, string method = "GET")
        {
            try
            {
                var result = string.Empty;
                var requestType = method.Trim().ToUpper();
                var param = "";

                if (requestType == "GET" && obj != null)
                {
                    //如果是get请求 拼接url
                    param = obj.ParseQueryString();
                    var sep = url.Contains('?') ? "&" : "?";
                    url += sep + param;
                }
                else if (requestType == "POST" && obj != null)
                {
                    var json = obj.ToJson();
                    var cryptoreq = NetEaseCryptoHelper.EncryptedRequest(json);
                    param = "params=" + cryptoreq.@params.UrlEncode() + "&encSecKey=" + cryptoreq.encSecKey.UrlEncode();
                }

                //var response = new HttpResponseMessage();
                //if (requestType == "GET")
                //    response = await _httpClient.GetAsync(url);
                //else
                //{
                //    var httpContent = new StringContent(param);
                //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                //    httpContent.Headers.ContentType.CharSet = "utf-8";
                //    response = await _httpClient.PostAsync(url, httpContent);
                //}
                   

                //result = await response.Content.ReadAsStringAsync();

                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = requestType;
                request.Headers[HttpRequestHeader.Referer] = "http://music.163.com";
                request.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.152 Safari/537";
                request.ContinueTimeout = 1000 * 20;
                //request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
                request.Headers[HttpRequestHeader.Cookie] = "appver=1.5.2;";


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
            catch (System.Exception)
            {
                return null;
            }
        }

        private static string GetResponseBody(HttpWebResponse response)
        {
            string responseBody = string.Empty;
            var contentEncoding = response.Headers[HttpResponseHeader.ContentEncoding];
            if (contentEncoding != null && contentEncoding.ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else if (contentEncoding != null && contentEncoding.ToLower().Contains("deflate"))
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


    }
}
