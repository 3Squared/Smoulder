using System.Xml.Serialization;

namespace TrainDataListener.TrainData
{
    [XmlRoot(ElementName = "TrainMovementData", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
    public class TrainMovementData
    {
        [XmlElement(ElementName = "OriginalTrainID", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OriginalTrainID { get; set; }

        [XmlElement(ElementName = "EventTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventTimestamp { get; set; }

        [XmlElement(ElementName = "LocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string LocationStanox { get; set; }

        [XmlElement(ElementName = "GBTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string GBTTTimestamp { get; set; }

        [XmlElement(ElementName = "WTTTimestamp", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string WTTTimestamp { get; set; }

        [XmlElement(ElementName = "PlannedMovementType", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string PlannedMovementType { get; set; }

        [XmlElement(ElementName = "MovementType", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string MovementType { get; set; }

        [XmlElement(ElementName = "EventSource", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string EventSource { get; set; }

        [XmlElement(ElementName = "RevisionFlag", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string RevisionFlag { get; set; }

        [XmlElement(ElementName = "OffRouteFlag", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string OffRouteFlag { get; set; }

        [XmlElement(ElementName = "Direction", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string Direction { get; set; }

        [XmlElement(ElementName = "Route", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string Route { get; set; }

        [XmlElement(ElementName = "TrainServiceCode", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainServiceCode { get; set; }

        [XmlElement(ElementName = "Division", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string Division { get; set; }

        [XmlElement(ElementName = "TOC", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TOC { get; set; }

        [XmlElement(ElementName = "TimetableVariation", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TimetableVariation { get; set; }

        [XmlElement(ElementName = "VariationStatus", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string VariationStatus { get; set; }

        [XmlElement(ElementName = "NextLocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string NextLocationStanox { get; set; }

        [XmlElement(ElementName = "NextReportRunTime", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string NextReportRunTime { get; set; }

        [XmlElement(ElementName = "TerminatedFlag", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TerminatedFlag { get; set; }

        [XmlElement(ElementName = "DelayMonitoringFlag", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string DelayMonitoringFlag { get; set; }

        [XmlElement(ElementName = "ReportingLocationStanox", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string ReportingLocationStanox { get; set; }

        [XmlElement(ElementName = "AutoExpectedFlag", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string AutoExpectedFlag { get; set; }

        [XmlElement(ElementName = "TrainFileAddress", Namespace = "http://xml.networkrail.co.uk/ns/2008/Train")]
        public string TrainFileAddress { get; set; }
    }
}