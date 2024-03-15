using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class WechatToken
    {
        private string _AppID = "";
        private string _AppSecret = "";
        private string _Token = "";
        private string _YxDateTime = "";

        [DataMember]
        public string AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }
        [DataMember]
        public string AppSecret
        {
            get { return _AppSecret; }
            set { _AppSecret = value; }
        }
        [DataMember]
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        [DataMember]
        public string YxDateTime
        {
            get { return _YxDateTime; }
            set { _YxDateTime = value; }
        }
    }
}
