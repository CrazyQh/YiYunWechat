using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Notice
    {
        private string _TaskID = "";
        private string _SN = "";
        private string _CreateDate = "";
        private string _CreateName = "";
        private string _VillageID = "";
        private string _NoticeTitle = "";
        private string _NoticeContent = "";

        [DataMember]
        public string TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }
        [DataMember]
        public string SN
        {
            get { return _SN; }
            set { _SN = value; }
        }
        [DataMember]
        public string CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        [DataMember]
        public string CreateName
        {
            get { return _CreateName; }
            set { _CreateName = value; }
        }
        [DataMember]
        public string VillageID
        {
            get { return _VillageID; }
            set { _VillageID = value; }
        }
        [DataMember]
        public string NoticeTitle
        {
            get { return _NoticeTitle; }
            set { _NoticeTitle = value; }
        }
        [DataMember]
        public string NoticeContent
        {
            get { return _NoticeContent; }
            set { _NoticeContent = value; }
        }
    }
}
