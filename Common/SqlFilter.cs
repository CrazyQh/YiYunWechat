using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SqlFilter
    {
        public static string SqlFilters(string source)
        {
            //单引号替换成两个单引号
            source = source.Replace("'", "''");
            //半角封号替换为全角封号，防止多语句执行
            source = source.Replace(";", "；");
            //半角括号替换为全角括号
            source = source.Replace("(", "（");
            source = source.Replace(")", "）");
            //去除执行存储过程的命令关键字
            source = source.Replace("Exec", "");
            source = source.Replace("Execute", "");
            //去除系统存储过程或扩展存储过程关键字
            source = source.Replace("xp_", "x p_");
            source = source.Replace("sp_", "s p_");
            //防止16进制注入
            source = source.Replace("0x", "0 x");
            return source;
        }
    }
}
