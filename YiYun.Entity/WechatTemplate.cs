using System.Runtime.Serialization;

namespace YiYun.Entity
{
    [DataContract]
    public class WechatTemplate
    {
        private string _TemplateID;
        private string _TemplateTitle;
        private string _TemplateContent;
        private string _TemplateExample;

        [DataMember]
        public string TemplateID
        { 
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        [DataMember]
        public string TemplateTitle
        {
            get { return _TemplateTitle; }
            set { _TemplateTitle = value; }
        }

        [DataMember]
        public string TemplateContent
        {
            get { return _TemplateContent; }
            set { _TemplateContent = value; }
        }

        [DataMember]
        public string TemplateExample
        {
            get { return _TemplateExample; }
            set { _TemplateExample = value; }
        }
    }
}
