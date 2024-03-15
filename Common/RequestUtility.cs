using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RequestUtility
    {
        #region Get请求
        public static string HttpGet(string url, Encoding encoding = null)
        {
            string message = string.Empty;
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = encoding ?? Encoding.UTF8;
                message = wc.DownloadString(url);
            }
            catch (Exception ex0)
            {
                message = ex0.Message;
            }
            return message;
        }
        public static async Task<string> HttpGetAsync(string url)
        {
            string message = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex0)
            {
                message = ex0.Message;
            }
            return message;
        }
        #endregion

        #region Post请求
        public static string HttpPost(string url, string content, Encoding encoding = null)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                Request.ContentLength = bytes.Length;
                using (Stream stream = Request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        result = streamreader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex0)
            {
                result = ex0.Message;
            }
            return result;
        }
        public static string HttpPostForJson(string url, string content, Encoding encoding = null)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                Request.ContentType = "application/json";
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                Request.ContentLength = bytes.Length;
                using (Stream stream = Request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        result = streamreader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex0)
            {
                result = ex0.Message;
            }
            return result;
        }
        public static async Task<string> HttpPostAsync(string url, StringContent content)
        {
            string message = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex0)
            {
                message = ex0.Message;
            }
            return message;
        }
        #endregion
    }
}
