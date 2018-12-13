using Smoulder.Interfaces;
using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainChangeOriginMsgV1", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainChangeOriginMsgV1 : TrustMessageData
    {
        [XmlElement(ElementName = "Sender", Namespace = "http://xml.networkrail.co.uk/ns/2008/EAI")]
        public Sender Sender { get; set; }

        [XmlElement(ElementName = "TrainChangeOriginData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public TrainChangeOriginData TrainChangeOriginData { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "eai", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Eai { get; set; }

        [XmlAttribute(AttributeName = "nr", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Nr { get; set; }

        [XmlAttribute(AttributeName = "classification")]
        public string Classification { get; set; }

        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }

        [XmlAttribute(AttributeName = "owner")]
        public string Owner { get; set; }

        [XmlAttribute(AttributeName = "originMsgId")]
        public string OriginMsgId { get; set; }
    }

}

