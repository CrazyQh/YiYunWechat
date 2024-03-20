using Common;
using LitJson;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace YiYun.Entity
{
    public class PayClient
    {
        private string _openid = "";
        private string _appid = "";
        private string _MchId = "";
        private string _Paykey = "";
        private string _nonce_str = "";
        private string _prepay_id = "";
        private string _sign = "";
        private string _timestamp = "";
        private string _total_fee = "";
        private string _body = "";
        private string _out_trade_no = "";
        private string _IpAddress = "";
        private string _notify_url = "";
        private string _trade_type = "";
        private string _Request = "";
        [DataMember]
        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
        [DataMember]
        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        [DataMember]
        public string MchId
        {
            get { return _MchId; }
            set { _MchId = value; }
        }
        [DataMember]
        public string Paykey
        {
            get { return _Paykey; }
            set { _Paykey = value; }
        }
        [DataMember]
        public string nonce_str
        {
            get { return _nonce_str; }
            set { _nonce_str = value; }
        }
        [DataMember]
        public string prepay_id
        {
            get { return _prepay_id; }
            set { _prepay_id = value; }
        }
        [DataMember]
        public string sign
        {
            get { return _sign; }
            set { _sign = value; }
        }
        [DataMember]
        public string timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        [DataMember]
        public string total_fee
        {
            get { return _total_fee; }
            set { _total_fee = value; }
        }
        [DataMember]
        public string body
        {
            get { return _body; }
            set { _body = value; }
        }
        [DataMember]
        public string out_trade_no
        {
            get { return _out_trade_no; }
            set { _out_trade_no = value; }
        }
        [DataMember]
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        [DataMember]
        public string notify_url
        {
            get { return _notify_url; }
            set { _notify_url = value; }
        }
        [DataMember]
        public string trade_type
        {
            get { return _trade_type; }
            set { _trade_type = value; }
        }
        [DataMember]
        public string Request
        {
            get { return _Request; }
            set { _Request = value; }
        }

    }

    public class SaoBeiPayClient
    {
        public string merchant_no { get; set; }
        public string terminal_id { get; set; }
        public string total_fee { get; set; }
        public string access_token { get; set; }
        public string open_id { get; set; }
        public string notify_url { get; set; }
        public string PayUrl { get; set; }
    }
    /// <summary>
    /// 扫呗统一下单返回数据
    /// </summary>
    public class SaoBeiTongyi
    {
        /// <summary>
        /// 
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 预支付请求成功
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 青岛易云网科信息技术有限公司
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchant_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_trace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ali_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string package_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paySign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string front_notify_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string key_sign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jdParams { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bestpayParams { get; set; }
    }
    /// <summary>
    /// 扫呗回调数据
    /// </summary>
    public class SaoBeiCallObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string channel_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string key_sign { get; set; }
        /// <summary>
        /// 青岛易云网科信息技术有限公司
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchant_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 支付成功
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_trace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_id { get; set; }

    }
    public class WxPayData
    {
        public const string SIGN_TYPE_MD5 = "MD5";
        public const string SIGN_TYPE_HMAC_SHA256 = "HMAC-SHA256";
        public WxPayData()
        {

        }

        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws WxPayException
        **/
        public string ToXml()
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
        */
        public SortedDictionary<string, object> FromXml(string xml, string key)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"].ToString().Equals("SUCCESS"))
                {
                    CheckSign(key);//验证签名,不通过会抛异常
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;

        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
        */
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }


                str += string.Format("{0}={1}\n", pair.Key, pair.Value.ToString());
            }
            str = HttpUtility.HtmlEncode(str);
            return str;
        }


        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign(string signType, string paykey)
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + paykey;

            if (signType == SIGN_TYPE_MD5)
            {
                var md5 = MD5.Create();
                var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var sb = new StringBuilder();
                foreach (byte b in bs)
                {
                    sb.Append(b.ToString("x2"));
                }
                //所有字符转为大写
                return sb.ToString().ToUpper();
            }
            else if (signType == SIGN_TYPE_HMAC_SHA256)
            {
                return CalcHMACSHA256Hash(str, WxPayConfig.GetConfig().GetKey());
            }
            else
            {
                throw new Exception("sign_type 不合法");
            }
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名 SHA256
        */
        public string MakeSign(string paykey)
        {
            return MakeSign(SIGN_TYPE_MD5, paykey);
        }



        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign(string key)
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string cal_sign = MakeSign(key);

            if (cal_sign == return_sign)
            {
                return true;
            }
            throw new Exception("WxPayData签名验证错误!");
        }

        /**
        * @获取Dictionary
        */
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }


        private string CalcHMACSHA256Hash(string plaintext, string salt)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(plaintext),
            baSalt = enc.GetBytes(salt);
            System.Security.Cryptography.HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
        public static string GetJsApiParameters(string appid, string prepay_id, string paykey)
        {
            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", appid);
            jsApiParam.SetValue("timeStamp", TimerHelper.GetTimestamp());
            jsApiParam.SetValue("nonceStr", TenPayV3Util.GetNoncestr());
            jsApiParam.SetValue("package", prepay_id);
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign(paykey));
            string parameters = jsApiParam.ToJson();
            return parameters;
        }
    }
    public class WxPayConfig
    {

        private static volatile IConfig config;
        private static object syncRoot = new object();

        public static IConfig GetConfig()
        {
            if (config == null)
            {
                lock (syncRoot)
                {
                    if (config == null)
                        config = new DemoConfig();
                }
            }
            return config;
        }





    }
    public class DemoConfig : IConfig
    {
        public DemoConfig()
        {
        }


        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置），请妥善保管，避免密钥泄露
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置），请妥善保管，避免密钥泄露
        */

        public string GetAppID()
        {
            return "";
        }
        public string GetMchID()
        {
            return "";
        }
        public string GetKey()
        {
            return "";
        }
        public string GetAppSecret()
        {
            return "";
        }



        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
         * 1.证书文件不能放在web服务器虚拟目录，应放在有访问权限控制的目录中，防止被他人下载；
         * 2.建议将证书文件名改为复杂且不容易猜测的文件
         * 3.商户服务器要做好病毒和木马防护工作，不被非法侵入者窃取证书文件。
        */
        public string GetSSlCertPath()
        {
            return "";
        }
        public string GetSSlCertPassword()
        {
            return "";
        }



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public string GetNotifyUrl()
        {
            return "";
        }

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public string GetIp()
        {
            return "0.0.0.0";
        }


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public string GetProxyUrl()
        {
            return "";
        }


        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public int GetReportLevel()
        {
            return 1;
        }


        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public int GetLogLevel()
        {
            return 1;
        }
    }
    public interface IConfig
    {

        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置），请妥善保管，避免密钥泄露
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置），请妥善保管，避免密钥泄露
        */

        string GetAppID();
        string GetMchID();
        string GetKey();
        string GetAppSecret();



        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
         * 1.证书文件不能放在web服务器虚拟目录，应放在有访问权限控制的目录中，防止被他人下载；
         * 2.建议将证书文件名改为复杂且不容易猜测的文件
         * 3.商户服务器要做好病毒和木马防护工作，不被非法侵入者窃取证书文件。
        */
        string GetSSlCertPath();
        string GetSSlCertPassword();



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        string GetNotifyUrl();

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        string GetIp();


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        string GetProxyUrl();


        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        int GetReportLevel();


        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        int GetLogLevel();


    }
}
