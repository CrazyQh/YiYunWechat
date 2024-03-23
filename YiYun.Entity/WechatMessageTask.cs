using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YiYun.Entity
{
    [DataContract]
    public class WechatMessageTask
    {
        private string _ID;
        private string _MessageType;
        private string _SoTaskID;
        private string _HouseID;
        private string _Params1;
        private string _Params2;
        private string _Params3;
        private string _Params4;
        private string _Params5;
        private string _OpenID;
        private string _TemplateID;
        private string _SendState;

        [DataMember]
        public string ID
        { get { return _ID; } set {  _ID = value; } }

        [DataMember]
        public string MessageType
        { get { return _MessageType; } set { _MessageType = value; } }

        [DataMember]
        public string SoTaskID
        { get { return _SoTaskID; } set { _SoTaskID = value; } }

        [DataMember]
        public string HouseID
        { get { return _HouseID; } set { _HouseID = value; } }

        [DataMember]
        public string Params1
        { get { return _Params1; } set {  _Params1 = value; } }

        [DataMember]
        public string Params2
        { get { return _Params2; } set {  _Params2 = value; } }

        [DataMember]
        public string Params3
        { get { return _Params3; } set {  _Params3 = value; } }

        [DataMember]
        public string Params4
        { get { return _Params4; } set {  _Params4 = value; } }

        [DataMember]
        public string Params5
        { get { return _Params5; } set {  _Params5 = value; } }

        [DataMember]
        public string OpenID
        { get { return _OpenID; } set { _OpenID = value; } }

        [DataMember]
        public string TemplateID
        { get { return _TemplateID; } set { _TemplateID = value; } }

        [DataMember]
        public string SendState
        { get { return _SendState; } set { _SendState = value; } }
    }
}
