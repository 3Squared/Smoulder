using System;

namespace TrainDataListener.ScheduleData
{
    public class CachedTrainData
    {
        public long id;
        public long activation_id;
        public long? schedule_id;
        public string head_code;
        public string train_id;
        public int toc_id;
        public string train_uid;
        public string train_service_code;
        public DateTime? activated_departure_timestamp;
        public DateTime? actual_departure_timestamp;
        public string origin;
        public DateTime? scheduled_departure;
        public DateTime? scheduled_arrival;
        public string destination;
        public DateTime? last_reported_timestamp;
        public int last_reported_delay;
        public string last_reported_location;
        public string last_reported_type;
        public bool? cancelled;
        public bool? cancelled_at_origin;
        public bool? cancelled_immediatly;
        public bool? cancelled_en_route;
        public bool? cancelled_out_of_plan;
        public DateTime? cancellation_timestamp;
        public bool? schedule_cancelled;
        public int? stp_start_indicator_index;
        public int? stp_end_indicator_index;
        public bool schedule_just_for_today;
        public bool has_schedule;
        public bool should_have_departed_exception;
    }
}
