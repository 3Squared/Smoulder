using System.Collections.Generic;

namespace TrainDataListener.ScheduleData
{
    public class ScheduleSegment
    {
        public long id;
        public long schedule_id;
        public string signalling_id;
        public string CIF_train_category;
        public string CIF_headcode;
        public string CIF_course_indicator;
        public string CIF_train_service_code;
        public string CIF_business_sector;
        public string CIF_power_type;
        public string CIF_timing_load;
        public string CIF_speed;
        public string CIF_operating_characteristics;
        public string CIF_train_class;
        public string CIF_sleepers;
        public string CIF_reservations;
        public string CIF_connection_indicator;
        public string CIF_catering_code;
        public string CIF_service_branding;
        public string last_modified;
        public List<ScheduleLocation> scheduleLocations;
    }
}