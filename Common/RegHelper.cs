using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 注册表操作类
    /// </summary>
    public class RegHelper
    {

        //    /// <summary>
        //    ///  读取注册表
        //    /// </summary>
        //    /// <param name="root">顶级注册表</param>
        //    /// <param name="path">注册表主路径</param>
        //    /// <param name="name">注册表名称</param>
        //    /// <returns></returns>
        public static string GetReg(RegistryKey root, string path, string name)
        {
            string registData;
            RegistryKey software = root.OpenSubKey(path, true);
            if (software != null)
            {
                object t = software.GetValue(name);
                if (t != null)
                {
                    registData = t.ToString();
                }
                else
                {
                    registData = "注册表名称不存在";
                }
            }
            else
            {
                registData = "注册表不存在!";
            }
            return registData;
        }
        //    /// <summary>
        //    /// 创建或设置注册表
        //    /// </summary>
        //    /// <param name="root">顶级注册表</param>
        //    /// <param name="name">名称</param>
        //    /// <param name="value">值</param>
        //    /// <param name="path">注册表主路径</param>
        public static void WriteReg(RegistryKey root, string path, string name, string value)
        {
            RegistryKey aimdir = root.CreateSubKey(path);
            aimdir.SetValue(name, value);
            aimdir.Close();
            aimdir.Dispose();
        }
        //    /// <summary>
        //    /// 删除注册表目录
        //    /// </summary>
        //    /// <param name="root">顶级注册表</param>
        //    /// <param name="path">注册表主路径</param>
        //    /// <param name="name">注册表目录名称</param>
        //    /// <returns>Boolean</returns>
        public static bool DeleteReg(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            int m = 0;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            for (int i = 0; i < subkeyNames.Length; i++)
            {
                if (subkeyNames[i] == name)
                {
                    myKey.DeleteSubKey(name);
                    m = +1;
                }
            }
            if (m > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //    /// <summary>
        //    /// 删除注册表目录下的名称
        //    /// </summary>
        //    /// <param name="root">顶级注册表</param>
        //    /// <param name="path">注册表主路径</param>
        //    /// <param name="name">注册表目录名称</param>
        //    /// <returns>Boolean</returns>
        public static bool DeleteRegValue(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            int m = 0;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetValueNames();
            for (int i = 0; i < subkeyNames.Length; i++)
            {
                if (subkeyNames[i] == name)
                {
                    myKey.DeleteValue(name);
                    m = +1;
                }
            }
            if (m > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
