using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYunWeChat.Auth
{
    public class AliHelper
    {
        /// <summary>
        /// 授权 获取code
        /// </summary>
        public static readonly string AliAutoUrl = "alipays://platformapi/startapp?appId=20000067&url=https%3A%2F%2Fopenauth.alipay.com%2Foauth2%2FpublicAppAuthorize.htm%3Fapp_id%3DAPPID%26scope%3DSCOPE%26redirect_uri%3DENCODED_URL%26state%3DSTATE";

    }
}
