using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class House
    {
        private string _ID;
        private string _VillageID;
        private string _VillageName;
        private string _AreaID;
        private string _AreaName;
        private string _FloorID;
        private string _FloorName;
        private string _UnitID;
        private string _UnitName;
        private string _DoorNo;
        private string _HouseCode;
        private string _HouseOwner;
        private string _DamagesFlag;
        private string _OnlinePay;
        private string _Tel; 
        private string _OpenID1; 
        private string _OpenID2;

        [DataMember]
        public string ID
        { 
            get { return _ID; } 
            set {  _ID = value; } 
        }

        [DataMember]
        public string VillageID
        { 
            get { return _VillageID;} 
            set { _VillageID = value; } 
        }

        [DataMember] 
        public string VillageName 
        { 
            get { return _VillageName; } 
            set { _VillageName = value; } 
        }

        [DataMember] 
        public string AreaID
        { 
            get { return _AreaID; } 
            set { _AreaID = value; } 
        }

        [DataMember] 
        public string AreaName
        { 
            get { return _AreaName; } 
            set { _AreaName = value; } 
        }

        [DataMember] 
        public string FloorID
        { 
            get { return _FloorID; } 
            set { _FloorID = value; } 
        }

        [DataMember] 
        public string FloorName
        { 
            get { return _FloorName; } 
            set { _FloorName = value; } 
        }

        [DataMember]
        public string UnitID
        { 
            get { return _UnitID; } 
            set { _UnitID = value; } 
        }

        [DataMember]
        public string UnitName
        { 
            get { return _UnitName; } 
            set { _UnitName = value; } 
        }

        [DataMember]
        public string DoorNo
        { 
            get { return _DoorNo; } 
            set { _DoorNo = value; } 
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
        public string DamagesFlag
        { 
            get { return _DamagesFlag; } 
            set { _DamagesFlag = value; } 
        }

        [DataMember]
        public string OnlinePay
        { 
            get { return _OnlinePay; } 
            set { _OnlinePay = value; } 
        }

        [DataMember]
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        [DataMember]
        public string OpenID1
        {
            get { return _OpenID1; }
            set { _OpenID1 = value; }
        }

        [DataMember]
        public string OpenID2
        {
            get { return _OpenID2; }
            set { _OpenID2 = value; }
        }
    }
}
