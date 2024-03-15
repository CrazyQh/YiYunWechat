using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// json序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        //<summary>
        //json对象转换成json字符串
        //</summary>
        //<typeparam name="T"></typeparam>
        //<param name="_stringjson"></param>
        //<returns></returns>
        public static string GetJson<T>(T _obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, _obj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        //<summary>
        //json字符串转换成Json对象
        //</summary>
        //<typeparam name="T"></typeparam>
        //<param name="_json"></param>
        //<returns></returns>
        public static T GetObject<T>(string _json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(_json)))
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                return (T)json.ReadObject(ms);
            }
        }
        // <summary>
        // Newtonsoft json字符串转换成Json对象
        // </summary>
        // <typeparam name="T"></typeparam>
        // <param name="_obj"></param>
        // <returns></returns>
        public static T GetObjectNet<T>(string _json)
        {
            return JsonConvert.DeserializeObject<T>(_json);
        }
        // <summary>
        // Newtonsoft json字符串转换成ListJson对象
        // </summary>
        // <typeparam name="T"></typeparam>
        // <param name="_obj"></param>
        // <returns></returns>
        public static List<T> GetObjectNetLs<T>(string _json)
        {
            return JsonConvert.DeserializeObject<List<T>>(_json);
        }
        //<summary>
        //json字符串转化成Dictionary
        //</summary>
        //<param name="json"></param>
        //<returns></returns>
        public static Dictionary<string, string> GetDictionary(string _json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<Dictionary<string, string>>(_json);
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        //<summary>
        //Dictionary转化成json字符串
        //</summary>
        //<param name="json"></param>
        //<returns></returns>
        public static string GetDictionaryJson(Dictionary<string, string> _dictionary)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(_dictionary);
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        //<summary>
        //json字符串转换成Sorteddictionary对象
        //</summary>
        //<param name="_jsonstring"></param>
        //<returns></returns>
        public static SortedDictionary<string, string> SordtGetDictionary(string _jsonstring)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<SortedDictionary<string, string>>(_jsonstring);
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
    }
}
