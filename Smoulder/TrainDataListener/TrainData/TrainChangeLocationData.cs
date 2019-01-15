using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainChangeLocationData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainChangeLocationData
    {
        [XmlElement(ElementName = "OriginalTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalTrainID { get; set; }

        [XmlElement(ElementName = "CurrentTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string CurrentTrainID { get; set; }

        [XmlElement(ElementName = "EventTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventTimestamp { get; set; }

        [XmlElement(ElementName = "LocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string LocationStanox { get; set; }

        [XmlElement(ElementName = "WTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string WTTTimestamp { get; set; }

        [XmlElement(ElementName = "OriginalLocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalLocationStanox { get; set; }

        [XmlElement(ElementName = "OriginalWTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalWTTTimestamp { get; set; }

        [XmlElement(ElementName = "TrainServiceCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainServiceCode { get; set; }

        [XmlElement(ElementName = "TrainFileAddress", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainFileAddress { get; set; }
    }
}