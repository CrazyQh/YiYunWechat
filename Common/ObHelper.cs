using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ObHelper
    {
        /// <summary>
        /// DataSet反射List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            DataTable table = ds.Tables[0];
            List<T> ls = new List<T>();
            foreach (DataRow dr in table.Rows)
            {
                T t = Activator.CreateInstance<T>();
                foreach (PropertyInfo pt in t.GetType().GetProperties())
                {
                    string name = pt.Name;
                    if (table.Columns.Contains(name))
                    {
                        string value = dr[name].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            pt.SetValue(t, value);
                        }
                        else
                        {
                            pt.SetValue(t, string.Empty);
                        }
                    }
                }
                ls.Add(t);
            }
            return ls;
        }
    }
    public class ObHelperByte
    {
        /// <summary>
        /// DataSet反射List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            DataTable table = ds.Tables[0];
            List<T> ls = new List<T>();
            foreach (DataRow dr in table.Rows)
            {

                T t = Activator.CreateInstance<T>();
                foreach (PropertyInfo pt in t.GetType().GetProperties())
                {

                    string name = pt.Name;
                    string value = "";
                    byte[] MyData = new byte[0];
                    if (table.Columns.Contains(name))
                    {
                        if (pt.PropertyType.Name == "Byte[]")  //you 增加一个Byte类型
                        {
                            MyData = (byte[])dr[name];        //you  增加一个Byte类型
                        }                                     //you  增加一个Byte类型
                        else
                        {
                            value = dr[name].ToString();
                        }
                        if (!string.IsNullOrEmpty(value))   
                        {
                            pt.SetValue(t, value);
                        }
                        else if (MyData.Length > 0)   // you  增加一个Byte类型
                        {
                            pt.SetValue(t, MyData);   // you  增加一个Byte类型
                        }
                        else
                        {
                            pt.SetValue(t, string.Empty);
                        }
                    }
                }
                ls.Add(t);
            }
            return ls;
        }
    }
}
