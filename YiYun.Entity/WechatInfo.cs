using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YiYun.Entity
{
    [DataContract]
    public class WechatInfo
    {
        private string _AccountID = "";
        private string _AccountName = "";
        private string _TokenCheck = "";
        private string _EncodingAESKey = "";
        private string _AppID = "";
        private string _AppSecret = "";
        [DataMember]
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        [DataMember]
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }
        [DataMember]
        public string TokenCheck
        {
            get { return _TokenCheck; }
            set { _TokenCheck = value; }
        }
        [DataMember]
        public string EncodingAESKey
        {
            get { return _EncodingAESKey; }
            set { _EncodingAESKey = value; }
        }
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
    }
}
