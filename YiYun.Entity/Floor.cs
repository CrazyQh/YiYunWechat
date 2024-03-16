using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Floor
    {
        private string _ID;
        private string _VillageID;
        private string _AreaID;
        private string _FloorName;

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
        public string AreaID
        {
            get { return _AreaID; }
            set { _AreaID = value; }
        }

        [DataMember]
        public string FloorName
        {
            get { return _FloorName; }
            set { _FloorName = value; }
        }
    }
}
