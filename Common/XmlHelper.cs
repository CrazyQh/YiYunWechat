using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Common
{
    /// <summary>
    /// xml需要完善
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// xml字符串转成json
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string XmlStringToJson(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            string json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }
        /// <summary>
        /// xml文件转换成Json
        /// </summary>
        /// <param name="_Path">路径</param>
        /// <returns></returns>
        public static string XmlFileToJson(string _Path)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(_Path);
            string sb = JsonConvert.SerializeXmlNode(xmldoc);
            return sb;
        }
        /// <summary>
        /// xml字符串转换DataSet
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static DataSet XmlStringToDataSet(string xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlString);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// xml文件转换成DataSet
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static DataSet XmlFileToDataSet(string _path)
        {
            if (!string.IsNullOrEmpty(_path))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    //根据地址加载Xml文件
                    xmldoc.Load(_path);
                    DataSet ds = new DataSet();
                    //读取文件中的字符流
                    StrStream = new StringReader(xmldoc.InnerXml);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Linq读取xml
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="NodeName">节点名称</param>
        /// <returns></returns>
        public static string LinqReadXml(string filePath, string NodeName)
        {
            try
            {
                string str = "";
                XDocument xoc = XDocument.Load(filePath);
                var query = from people in xoc.Descendants(NodeName) select people.Value;
                foreach (var item in query)
                {
                    str += item;
                }
                return str;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// xml字符串转化SortDictionary
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> XmlToDictionary(string xml)
        {
            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                //获取到根节点<xml>
                XmlNode xmlNode = xmlDoc.FirstChild;
                XmlNodeList nodes = xmlNode.ChildNodes;
                foreach (XmlNode xn in nodes)
                {
                    //获取xml的键值对到WxPayData内部的数据中
                    XmlElement xe = (XmlElement)xn;
                    dic[xe.Name] = xe.InnerText;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dic;
        }
    }
}
