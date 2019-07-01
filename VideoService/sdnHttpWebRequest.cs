using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace VideoService
{
    public class sdnHttpWebRequest
    {
        #region httpUtils
        private const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";

        private CookieContainer CC = new CookieContainer();

        private void BugFix_CookieDomain(CookieContainer cookieContainer)
        {
            System.Type _ContainerType = typeof(CookieContainer);
            Hashtable table = (Hashtable)_ContainerType.InvokeMember("m_domainTable",
                                       System.Reflection.BindingFlags.NonPublic |
                                       System.Reflection.BindingFlags.GetField |
                                       System.Reflection.BindingFlags.Instance,
                                       null,
                                       cookieContainer,
                                       new object[] { });
            ArrayList keys = new ArrayList(table.Keys);
            foreach (string keyObj in keys)
            {
                string key = (keyObj as string);
                if (key[0] == '.')
                {
                    string newKey = key.Remove(0, 1);
                    table[newKey] = table[keyObj];
                }
            }
        }

        private String GetMid(String input, String s, String e)
        {
            int pos = input.IndexOf(s);
            if (pos == -1)
            {
                return "";
            }


            pos += s.Length;


            int pos_end = 0;
            if (e == "")
            {
                pos_end = input.Length;
            }
            else
            {
                pos_end = input.IndexOf(e, pos);
            }


            if (pos_end == -1)
            {
                return "";
            }


            return input.Substring(pos, pos_end - pos);
        }

        public String DoGet(String url)
        {
            String html = "";
            StreamReader reader = null;
            HttpWebRequest webReqst = (HttpWebRequest)WebRequest.Create(url);
            webReqst.Method = "GET";
            webReqst.UserAgent = DefaultUserAgent;
            webReqst.KeepAlive = true;
            webReqst.CookieContainer = CC;
            webReqst.Timeout = 30000;
            webReqst.ReadWriteTimeout = 30000;
            try
            {
                HttpWebResponse webResponse = (HttpWebResponse)webReqst.GetResponse();
                BugFix_CookieDomain(CC);
                if (webResponse.StatusCode == HttpStatusCode.OK && webResponse.ContentLength < 1024 * 1024)
                {
                    Stream stream = webResponse.GetResponseStream();
                    stream.ReadTimeout = 30000;
                    if (webResponse.ContentEncoding == "gzip")
                    {
                        reader = new StreamReader(new GZipStream(stream, CompressionMode.Decompress), Encoding.Default);
                    }
                    else
                    {
                        reader = new StreamReader(stream, Encoding.Default);
                    }
                    html = reader.ReadToEnd();
                }
            }
            catch
            {

            }

            return html;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受    
        }

        public String DoPost(String url, String Content)
        {
            string html = "";
            StreamReader reader = null;
            HttpWebRequest webReqst = null;
            //如果是发送HTTPS请求    
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                webReqst = WebRequest.Create(url) as HttpWebRequest;
                webReqst.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                webReqst = WebRequest.Create(url) as HttpWebRequest;
            }

            byte[] data = Encoding.Default.GetBytes(Content);



            webReqst.Method = "POST";
            webReqst.UserAgent = DefaultUserAgent;
            //   webReqst.ContentType = "application/x-www-form-urlencoded";
            webReqst.ContentType = "application/json";
            webReqst.ContentLength = data.Length + 1;
            webReqst.CookieContainer = CC;
            webReqst.Timeout = 30000;
            webReqst.ReadWriteTimeout = 30000;
            try
            {
                //  byte[] data = Encoding.Default.GetBytes(Content);
                Stream stream = webReqst.GetRequestStream();
                stream.Write(data, 0, data.Length);


                HttpWebResponse webResponse = (HttpWebResponse)webReqst.GetResponse();
                BugFix_CookieDomain(CC);
                if (webResponse.StatusCode == HttpStatusCode.OK && webResponse.ContentLength < 1024 * 1024)
                {
                    stream = webResponse.GetResponseStream();
                    stream.ReadTimeout = 30000;
                    if (webResponse.ContentEncoding == "gzip")
                    {
                        reader = new StreamReader(new GZipStream(stream, CompressionMode.Decompress), Encoding.Default);
                    }
                    else
                    {
                        reader = new StreamReader(stream, Encoding.Default);
                    }
                    html = reader.ReadToEnd();
                }
            }
            catch
            {

            }

            return html;
        }
        /// <summary>
        /// 利用httpClient 调用Post webapi
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string sdnDoPost(string url, string Content)
        {
            // var strJosn =  JsonConvert.SerializeObject(Content);
            // HttpWebRequest req = new HttpWebRequest();
            //  req.cont
            HttpContent httpContent = new StringContent(Content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = new System.Net.Http.HttpClient();
            //采取POST请求
            var responseJson = httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
            //将请求的数据进行序列化
            var sites = JsonConvert.DeserializeObject<IList<Site>>(responseJson);
            //遍历解析数据
            //  sites.ToList().ForEach(x => Console.WriteLine(x.Title + "：" + x.Uri));

            return responseJson;
        }
        #endregion
    }
}