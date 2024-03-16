using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Area
    {
        private string _ID;
        private string _VillageID;
        private string _AreaName;

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
        public  string AreaName 
        {
            get { return _AreaName; } 
            set { _AreaName = value; } 
        }
    }
}
