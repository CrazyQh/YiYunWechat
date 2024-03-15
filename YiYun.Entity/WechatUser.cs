using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YiYun.Entity
{
    [DataContract]
    public class WechatUser
    {
        private string _ID = "";
        private string _AccountID = "";
        private string _OpenID = "";
        private string _Subscribe = "";
        private string _SubscribeTime = "";
        private string _UnSubscribeTime = "";

        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [DataMember]
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        [DataMember]
        public string OpenID
        {
            get { return _OpenID; }
            set { _OpenID = value; }
        }
        [DataMember]
        public string Subscribe
        {
            get { return _Subscribe; }
            set { _Subscribe = value; }
        }
        [DataMember]
        public string SubscribeTime
        {
            get { return _SubscribeTime; }
            set { _SubscribeTime = value; }
        }
        [DataMember]
        public string UnSubscribeTime
        {
            get { return _UnSubscribeTime; }
            set { _UnSubscribeTime = value; }
        }
    }
}
