//Coverted using http://xmltocsharp.azurewebsites.net/
using System.Xml.Serialization;
namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "Sender", Namespace = "http://xml.networkrail.co.uk/ns/2008/EAI")]
    public class Sender
    {
        [XmlAttribute(AttributeName = "organisation")]
        public string Organisation { get; set; }

        [XmlAttribute(AttributeName = "application")]
        public string Application { get; set; }

        [XmlAttribute(AttributeName = "component")]
        public string Component { get; set; }

        [XmlAttribute(AttributeName = "userID")]
        public string UserID { get; set; }

        [XmlAttribute(AttributeName = "sessionID")]
        public string SessionID { get; set; }
    }
}

