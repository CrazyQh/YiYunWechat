using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Village
    {
        private string _ID = "";
        private string _VillageCode = "";
        private string _VillageName = "";
        private string _OnLinePay = "";
        private string _OnLineGg = "";
        private string _OnLineBx = "";
        private string _MerchantID = "";
        private string _TerminalNumber = "";
        private string _PayToken = "";

        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember]
        public string VillageCode
        {
            get { return _VillageCode; }
            set { _VillageCode = value; }
        }

        [DataMember]
        public string VillageName
        {
            get { return _VillageName; }
            set { _VillageName = value; }
        }

        [DataMember]
        public string OnLinePay
        {
            get { return _OnLinePay; }
            set { _OnLinePay = value; }
        }

        [DataMember]
        public string OnLineGg
        {
            get { return _OnLineGg; }
            set { _OnLineGg = value; }
        }

        [DataMember]
        public string OnLineBx
        {
            get { return _OnLineBx; }
            set { _OnLineBx = value; }
        }

        [DataMember]
        public string MerchantID
        {
            get { return _MerchantID; }
            set { _MerchantID = value; }
        }

        [DataMember]
        public string TerminalNumber
        {
            get { return _TerminalNumber; }
            set { _TerminalNumber = value; }
        }

        [DataMember]
        public string PayToken
        {
            get { return _PayToken; }
            set { _PayToken = value; }
        }
    }
}
