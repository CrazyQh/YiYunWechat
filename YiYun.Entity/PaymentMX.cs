using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class PaymentMX
    {
        private string _YearMonth;
        private string _ProjectName;
        private string _Qty;
        private string _YSAmount;
        private string _SSAmount;

        [DataMember]
        public string YearMonth
        { 
            get { return _YearMonth; } 
            set { _YearMonth = value; } 
        }

        [DataMember]
        public string ProjectName
        { 
            get { return _ProjectName; } 
            set { _ProjectName = value; } 
        }

        [DataMember]
        public string Qty
        { 
            get { return _Qty; } 
            set { _Qty = value; } 
        }

        [DataMember]
        public string YSAmount
        { 
            get { return _YSAmount; } 
            set { _YSAmount = value; } 
        }

        [DataMember]
        public string SSAmount
        {
            get { return _SSAmount; }
            set { _SSAmount = value; }
        }
    }

    public class RefundMX
    {
        private string _YearMonth;
        private string _ProjectName;
        private string _Qty;
        private string _SSAmount;
        private string _RefundAmount;

        [DataMember]
        public string YearMonth
        {
            get { return _YearMonth; }
            set { _YearMonth = value; }
        }

        [DataMember]
        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }

        [DataMember]
        public string Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        [DataMember]
        public string RefundAmount
        {
            get { return _RefundAmount; }
            set { _RefundAmount = value; }
        }

        [DataMember]
        public string SSAmount
        {
            get { return _SSAmount; }
            set { _SSAmount = value; }
        }
    }
}
