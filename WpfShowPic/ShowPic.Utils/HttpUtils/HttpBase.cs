using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace ShowPic.Utils.HttpUtils
    {
    /// <summary>
    /// HTTP访问基类
    /// </summary>
    public class Httpbase
    {
        private static readonly string absoluteUrl = "https://localhost:7249/";

        /// <summary>
        /// get方式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> param)
        {
            string p = string.Empty;
            if (param != null && param.Count() > 0)
            {
                foreach (var keypair in param)
                {
                    if (keypair.Value != null)
                    {
                        p += $"{keypair.Key}={keypair.Value}&";
                    }
                }
            }
            if (!string.IsNullOrEmpty(p))
            {
                p = p.TrimEnd('&');
            }
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"{absoluteUrl}{url}?{p}", HttpCompletionOption.ResponseContentRead).Result;
            string strJson = string.Empty;
            Stream stream = response.Content.ReadAsStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                strJson = reader.ReadToEnd();
            }
            return strJson;
        }

        /// <summary>
        /// POST方式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Post<T>(string url, T t)
        {
            var content = JsonConvert.SerializeObject(t);

            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync($"{absoluteUrl}{url}", httpContent).Result;

            var respContent = response.Content.ReadAsStringAsync().Result;
            return respContent;
        }

        public static string Put<T>(string url, T t)
        {
            var content = JsonConvert.SerializeObject(t);

            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = httpClient.PutAsync($"{absoluteUrl}{url}", httpContent).Result;

            var respContent = response.Content.ReadAsStringAsync().Result;
            return respContent;
        }

        public static string Delete(string url, Dictionary<string, string> param)
        {
            string p = string.Empty;
            if (param != null && param.Count() > 0)
            {
                foreach (var keypair in param)
                {
                    p += $"{keypair.Key}={keypair.Value}&";
                }
            }
            if (!string.IsNullOrEmpty(p))
            {
                p = p.TrimEnd('&');
            }
            var httpClient = new HttpClient();
            var response = httpClient.DeleteAsync($"{absoluteUrl}{url}?{p}").Result;
            var respContent = response.Content.ReadAsStringAsync().Result;
            return respContent;
        }


        public static T StrToObject<T>(string str)
        {
            var t = JsonConvert.DeserializeObject<T>(str);
            return t;
        }
    }
}
