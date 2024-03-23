using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class ZDMX
    {
        private string _SoID;
        private string _SoTaskID;
        private string _YearMonth;
        private string _SFProjectName;
        private string _JSTypeT;
        private string _Qty;
        private string _Amount;
        private string _WYJ;
        private string _IsMustPay;

        [DataMember]
        public string SoID
        {
            get { return _SoID; }
            set { _SoID = value; }
        }

        [DataMember]
        public string SoTaskID
        {
            get { return _SoTaskID; }
            set { _SoTaskID = value; }
        }

        [DataMember]
        public string YearMonth
        {
            get { return _YearMonth; }
            set { _YearMonth = value; }
        }

        [DataMember]
        public string SFProjectName
        {
            get { return _SFProjectName; }
            set { _SFProjectName = value; }
        }

        [DataMember]
        public string JSTypeT
        {
            get { return _JSTypeT; }
            set { _JSTypeT = value; }
        }

        [DataMember]
        public string Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        [DataMember]
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        [DataMember]
        public string WYJ
        {
            get { return _WYJ; }
            set { _WYJ = value; }
        }

        [DataMember]
        public string IsMustPay
        {
            get { return _IsMustPay; }
            set { _IsMustPay = value; }
        }
    }
}
