using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainChangeIdentityData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainChangeIdentityData
    {
        [XmlElement(ElementName = "OriginalTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalTrainID { get; set; }

        [XmlElement(ElementName = "EventTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventTimestamp { get; set; }

        [XmlElement(ElementName = "RevisedTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string RevisedTrainID { get; set; }

        [XmlElement(ElementName = "TrainServiceCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainServiceCode { get; set; }

        [XmlElement(ElementName = "TrainFileAddress", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainFileAddress { get; set; }
    }
}