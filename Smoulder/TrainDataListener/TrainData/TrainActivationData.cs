using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainActivationData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainActivationData
    {
        [XmlElement(ElementName = "OriginalTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalTrainID { get; set; }

        [XmlElement(ElementName = "EventTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventTimestamp { get; set; }

        [XmlElement(ElementName = "LocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string LocationStanox { get; set; }

        [XmlElement(ElementName = "WTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string WTTTimestamp { get; set; }

        [XmlElement(ElementName = "UIDNumber", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string UIDNumber { get; set; }

        [XmlElement(ElementName = "ScheduleStartTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ScheduleStartTimestamp { get; set; }

        [XmlElement(ElementName = "ScheduleEndTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ScheduleEndTimestamp { get; set; }

        [XmlElement(ElementName = "ScheduleSource", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ScheduleSource { get; set; }

        [XmlElement(ElementName = "ScheduleType", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ScheduleType { get; set; }

        [XmlElement(ElementName = "ScheduledWTTID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ScheduledWTTID { get; set; }

        [XmlElement(ElementName = "TOPSUID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TOPSUID { get; set; }

        [XmlElement(ElementName = "TrainPlanOriginTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainPlanOriginTimestamp { get; set; }

        [XmlElement(ElementName = "EventSource", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventSource { get; set; }

        [XmlElement(ElementName = "TrainCallMode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainCallMode { get; set; }

        [XmlElement(ElementName = "TOC", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TOC { get; set; }

        [XmlElement(ElementName = "TrainServiceCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainServiceCode { get; set; }

        [XmlElement(ElementName = "TrainFileAddress", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainFileAddress { get; set; }
    }
}