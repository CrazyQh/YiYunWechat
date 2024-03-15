using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CopyObject
    {
        /// <summary>
        /// 反射复制对象
        /// </summary>
        /// <typeparam name="T">原数据</typeparam>
        /// <typeparam name="S">复制对象</typeparam>
        /// <param name="t">原数据</param>
        /// <param name="s">复制对象</param>
        /// <param name="_resms"></param>
        /// <returns>S</returns>
        public static S CopyObj<T, S>(T t, S s, out string _resms)
        {
            _resms = "";
            try
            {
                foreach (PropertyInfo pt in t.GetType().GetProperties())
                {
                    foreach (PropertyInfo ps in s.GetType().GetProperties())
                    {
                        if (pt.Name.Contains(ps.Name) && pt.PropertyType.Equals(ps.PropertyType))
                        {
                            ps.SetValue(s, pt.GetValue(t));
                        }
                    }
                }
                return s;
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return default(S);
            }
        }
    }
}
