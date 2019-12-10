using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HYMiniProgram
{
    /// <summary>
    /// http请求帮助类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 字符串key=value拼接
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="needUrlEncode"></param>
        /// <returns></returns>
        public static string BuildQueryString(IDictionary<string, string> paramList, bool needUrlEncode = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in paramList.Keys)
            {
                if (sb.Length > 0) sb.Append("&");
                sb.Append(key).Append("=");
                sb.Append(needUrlEncode ? HttpUtility.UrlEncode(paramList[key]) : paramList[key]);
            }
            return sb.ToString();
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }
        public static string HttpGet(string url, CookieContainer cookies = null, string referUrl = "", string contentType = "application/x-www-form-urlencoded")
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var req = (HttpWebRequest)WebRequest.Create(url);
            //req.ProtocolVersion = HttpVersion.Version10;
            var encoding = Encoding.UTF8;

            req.Method = WebRequestMethods.Http.Get;

            req.ContentType = contentType;

            req.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36";

            if (cookies != null)
            {
                req.CookieContainer = cookies;
            }


            if (!string.IsNullOrEmpty(referUrl))
            {
                req.Referer = referUrl;
            }

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                string responseData;

                if (cookies != null)
                {
                    foreach (Cookie cookie in response.Cookies)
                    {
                        cookies.Add(cookie);
                    }
                }


                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd().ToString();
                }
                return responseData;
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <param name="referUrl"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpPost(string postData, string url, CookieContainer cookies = null, string referUrl = "", string contentType = "application/x-www-form-urlencoded")
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var encoding = Encoding.UTF8;
            byte[] bs = encoding.GetBytes(postData);
            req.Method = WebRequestMethods.Http.Post;
            if (cookies != null)
            {
                req.CookieContainer = cookies;
            }
            if (!string.IsNullOrEmpty(referUrl))
            {
                req.Referer = referUrl;
            }
            req.ContentType = contentType;
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                string responseData;
                if (cookies != null)
                {
                    foreach (Cookie cookie in response.Cookies)
                    {
                        cookies.Add(cookie);
                    }
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd().ToString();
                }
                return responseData;
            }
        }


        /// <summary>
        /// HttpClient实现Get请求
        /// </summary>
        public static async Task<string> HttpGetAsync(string url, string token = "")
        {
            // 创建HttpClient（注意传入HttpClientHandler）  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrEmpty(token))
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // 超时设置
                //http.Timeout = new TimeSpan(0, 0, 0, 15);

                // await异步等待回应  
                var response = await http.GetAsync(url);
                // 确保HTTP成功状态值  
                response.EnsureSuccessStatusCode();

                // await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）  
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// HttpClient实现Post请求
        /// </summary>
        public static async Task<string> HttpPostAsync(string doUrl, Dictionary<string, string> dictionary, string token = "")
        {
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                if (!string.IsNullOrEmpty(token))
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(dictionary);

                //await异步等待回应
                var response = await http.PostAsync(doUrl, content);

                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                return await response.Content.ReadAsStringAsync();
            }

        }


        /// <summary>
        /// HttpClient实现Put请求
        /// </summary>
        public static async Task<string> HttpPutAsync(string url)
        {
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"", "数据"}//键名必须为空
				});

                //await异步等待回应
                var response = await http.PutAsync(url, content);

                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                return await response.Content.ReadAsStringAsync();
            }

        }


        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <param name="referUrl"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string postData, string url, CookieContainer cookies = null, string referUrl = "", string contentType = "application/x-www-form-urlencoded")
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var encoding = Encoding.UTF8;
            byte[] bs = encoding.GetBytes(postData);
            req.Method = WebRequestMethods.Http.Post;
            if (cookies != null)
            {
                req.CookieContainer = cookies;
            }
            if (!string.IsNullOrEmpty(referUrl))
            {
                req.Referer = referUrl;
            }
            req.ContentType = contentType;
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)await req.GetResponseAsync().ConfigureAwait(false))
            {
                string responseData;
                if (cookies != null)
                {
                    foreach (Cookie cookie in response.Cookies)
                    {
                        cookies.Add(cookie);
                    }
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd().ToString();
                }
                return responseData;
            }
        }
    }
}