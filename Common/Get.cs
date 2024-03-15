using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class Get
    {
        /// <summary>
        /// get同步请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetJson<T>(string url)
        {
            string returnText = RequestUtility.HttpGet(url);
            T t = JsonHelper.GetObject<T>(returnText);
            return t;
        }
        ///// <summary>
        ///// get异步请求
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <returns></returns>
        public static async Task<T> GetJsonAsync<T>(string url)
        {
            string returnText = await RequestUtility.HttpGetAsync(url);
            T t = JsonHelper.GetObject<T>(returnText);
            return t;
        }
    }
}
