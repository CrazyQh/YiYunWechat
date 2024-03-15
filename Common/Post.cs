using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Post
    {
        /// <summary>
        /// post同步请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="contenet"></param>
        /// <returns></returns>
        public static T PostJson<T>(string url, string contenet)
        {
            string returnText = RequestUtility.HttpPost(url, contenet, encoding: Encoding.Default);
            T t = JsonHelper.GetObject<T>(returnText);
            return t;
        }
        /// <summary>
        /// post异步请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="contenet"></param>
        /// <returns></returns>
        public static async Task<T> PostJsonAsync<T>(string url, StringContent content)
        {
            string returnText = await RequestUtility.HttpPostAsync(url, content);
            T t = JsonHelper.GetObject<T>(returnText);
            return t;
        }
    }
}
