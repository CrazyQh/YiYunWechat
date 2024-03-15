using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Extend
    {
        private string _tktnostart = "";
        private string _tktnoend = "";
        
        [DataMember]
        public string TKTNOSTART
        {
            get { return _tktnostart; }
            set { _tktnostart = value; }
        }
        [DataMember]
        public string TKTNOEND
        {
            get { return _tktnoend; }
            set { _tktnoend = value; }
        }
    }

    public static class KcchangeHelper
    {
        public static bool IsInterWith(this Extend _curent, Extend _other)
        {
            if (_curent.TKTNOSTART.CompareTo(_other.TKTNOSTART) >= 0 && _curent.TKTNOSTART.CompareTo(_other.TKTNOEND) <= 0)
                return true;
            if (_curent.TKTNOEND.CompareTo(_other.TKTNOSTART) >= 0 && _curent.TKTNOEND.CompareTo(_other.TKTNOEND) <= 0)
                return true;
            return false;
        }
        public static bool ExistInterRange(this List<Extend> _currents)
        {
            return _currents.Any(k => _currents.Where(p => !object.ReferenceEquals(k, p)).Any(i => k.IsInterWith(i)));
        }
    }
}
