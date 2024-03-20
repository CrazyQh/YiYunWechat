using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class JFMX
    {
        private string _SN;
        private string _WxPayType;
        private string _DJSSAmount;
        private string _CreateAt;

        [DataMember]
        public string SN 
        { 
            get { return _SN; } 
            set { _SN = value; } 
        }

        [DataMember]
        public string WxPayType 
        { 
            get { return _WxPayType; } 
            set { _WxPayType = value; } 
        }

        [DataMember]
        public string DJSSAmount 
        { 
            get { return _DJSSAmount; } 
            set { _DJSSAmount = value; } 
        }

        [DataMember]
        public string CreateAt 
        { 
            get { return _CreateAt; } 
            set { _CreateAt = value; } 
        }
    }
}
