using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class Unit
    {
        private string _ID;
        private string _VillageID;
        private string _AreaID;
        private string _FloorID;
        private string _UnitName;

        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataMember]
        public string VillageID
        {
            get { return _VillageID; }
            set { _VillageID = value; }
        }

        [DataMember]
        public string AreaID
        {
            get { return _AreaID; }
            set { _AreaID = value; }
        }

        [DataMember]
        public string FloorID
        {
            get { return _FloorID; }
            set { _FloorID = value; }
        }

        [DataMember]
        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }
    }
}
