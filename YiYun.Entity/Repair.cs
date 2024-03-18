using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Repair
    {
        private string _ID;
        private string _VillageID;
        private string _HouseID;
        private string _HouseCode;
        private string _HouseOwner;
        private string _CreateDate;
        private string _ReparirContent;
        private string _DealState;

        [DataMember]
        public string ID 
        { 
            get { return _ID; } 
            set { _ID = value; } 
        }

        [DataMember]
        public string VillageID 
        { 
            get {  return _VillageID; } 
            set { _VillageID = value; } 
        }

        [DataMember]
        public string HouseID 
        { 
            get { return _HouseID; } 
            set { _HouseID = value; } 
        }

        [DataMember]
        public string HouseCode 
        { 
            get { return _HouseCode; } 
            set { _HouseCode = value; } 
        }

        [DataMember]
        public string HouseOwner 
        { 
            get { return _HouseOwner; } 
            set { _HouseOwner = value; } 
        }

        [DataMember]
        public string CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        [DataMember]
        public string ReparirContent 
        { 
            get {  return _ReparirContent; } 
            set { _ReparirContent = value; } 
        }

        [DataMember]
        public string DealState 
        { 
            get { return _DealState; } 
            set { _DealState = value; } 
        }
    }
}
