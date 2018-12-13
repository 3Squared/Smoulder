using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainCancellationData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainCancellationData
    {
        [XmlElement(ElementName = "OriginalTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalTrainID { get; set; }

        [XmlElement(ElementName = "EventTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventTimestamp { get; set; }

        [XmlElement(ElementName = "LocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string LocationStanox { get; set; }

        [XmlElement(ElementName = "WTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string WTTTimestamp { get; set; }

        [XmlElement(ElementName = "TrainCancellationType", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainCancellationType { get; set; }

        [XmlElement(ElementName = "TrainServiceCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainServiceCode { get; set; }

        [XmlElement(ElementName = "ReasonCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ReasonCode { get; set; }

        [XmlElement(ElementName = "Division", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string Division { get; set; }

        [XmlElement(ElementName = "TOC", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TOC { get; set; }

        [XmlElement(ElementName = "TrainFileAddress", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainFileAddress { get; set; }
    }
}