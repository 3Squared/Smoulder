using System;
using System.Collections.Generic;

namespace TrainDataListener.ScheduleData
{
    public class Schedule
    {
        public long id;
        public string CIF_bank_holiday_running;
        public string CIF_stp_indicator;
        public string CIF_train_uid;
        public string applicable_timetable;
        public string atoc_code;
        public string schedule_days_runs;
        public DateTime schedule_start_date;
        public DateTime schedule_end_date;
        public string train_status;
        public string transaction_type;
        public bool is_deleted;
        public DateTime last_modified;
        public List<ScheduleSegment> scheduleSegments;
    }
}