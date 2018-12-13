namespace TrainDataListener.Repository
{
    public static class Columns
    {
        public static string id = "id";
        public static string last_modified = "last_modified";

        public static string is_deleted = "is_deleted";

        public static string transaction_type = "transaction_type";
        public static string main_train_uid = "main_train_uid";
        public static string assoc_train_uid = "assoc_train_uid";
        public static string assoc_start_date = "assoc_start_date";
        public static string assoc_end_date = "assoc_end_date";
        public static string assoc_days = "assoc_days";
        public static string category = "category";
        public static string date_indicator = "date_indicator";
        public static string location = "location";
        public static string base_location_suffix = "base_location_suffix";
        public static string assoc_location_suffix = "assoc_location_suffix";
        public static string diagram_type = "diagram_type";
        public static string CIF_stp_indicator = "CIF_stp_indicator";

        public static string tiploc_code = "tiploc_code";
        public static string nalco = "nalco";
        public static string stanox = "stanox";
        public static string crs_code = "crs_code";
        public static string description = "description";
        public static string tps_description = "tps_description";

        // JsonScheduleV1Content

        public static string CIF_bank_holiday_running = "CIF_bank_holiday_running";
        public static string CIF_train_uid = "CIF_train_uid";
        public static string applicable_timetable = "applicable_timetable";
        public static string atoc_code = "atoc_code";
        public static string new_schedule_segment_id = "new_schedule_segment_id";
        public static string schedule_days_runs = "schedule_days_runs";
        public static string schedule_end_date = "schedule_end_date";
        public static string schedule_segment_id = "schedule_segment_id";
        public static string schedule_start_date = "schedule_start_date";
        public static string train_status = "train_status";

        // new_schedule_segment
        public static string traction_class = "traction_class";
        public static string uic_code = "uic_code";

        // schedule_segment
        public static string schedule_id = "schedule_id";
        public static string signalling_id = "signalling_id";
        public static string CIF_train_category = "CIF_train_category";
        public static string CIF_headcode = "CIF_headcode";
        public static string CIF_course_indicator = "CIF_course_indicator";
        public static string CIF_train_service_code = "CIF_train_service_code";
        public static string CIF_business_sector = "CIF_business_sector";
        public static string CIF_power_type = "CIF_power_type";
        public static string CIF_timing_load = "CIF_timing_load";
        public static string CIF_speed = "CIF_speed";
        public static string CIF_operating_characteristics = "CIF_operating_characteristics";
        public static string CIF_train_class = "CIF_train_class";
        public static string CIF_sleepers = "CIF_sleepers";
        public static string CIF_reservations = "CIF_reservations";
        public static string CIF_connection_indicator = "CIF_connection_indicator";
        public static string CIF_catering_code = "CIF_catering_code";
        public static string CIF_service_branding = "CIF_service_branding";

        //schedule_location
        public static string location_type = "location_type";
        public static string record_identity = "record_identity";
        public static string tiploc_instance = "tiploc_instance";
        public static string arrival = "arrival";
        public static string departure = "departure";
        public static string pass = "pass";
        public static string public_arrival = "public_arrival";
        public static string public_departure = "public_departure";
        public static string platform = "platform";
        public static string line = "line";
        public static string path = "path";
        public static string engineering_allowance = "engineering_allowance";
        public static string pathing_allowance = "pathing_allowance";
        public static string performance_allowance = "performance_allowance";

        // Movement table columns
        public static string msg_type = "msg_type"; // Also used by TD (Train Data)
        public static string source_dev_id = "source_dev_id";
        public static string user_id = "user_id";
        public static string original_data_source = "original_data_source";
        public static string msg_queue_timestamp = "msg_queue_timestamp";
        public static string source_system_id = "source_system_id";
        public static string event_type = "event_type";
        public static string gbtt_timestamp = "gbtt_timestamp";
        public static string original_loc_stanox = "original_loc_stanox";
        public static string planned_timestamp = "planned_timestamp";
        public static string timetable_variation = "timetable_variation";
        public static string original_loc_timestamp = "original_loc_timestamp";
        public static string current_train_id = "current_train_id";
        public static string delay_monitoring_point = "delay_monitoring_point";
        public static string next_report_run_time = "next_report_run_time";
        public static string reporting_stanox = "reporting_stanox";
        public static string actual_timestamp = "actual_timestamp";
        public static string correction_ind = "correction_ind";
        public static string event_source = "event_source";
        public static string train_file_address = "train_file_address";
        public static string division_code = "division_code";
        public static string train_terminated = "train_terminated";
        public static string train_id = "train_id";
        public static string offroute_ind = "offroute_ind";
        public static string variation_status = "variation_status";
        public static string train_service_code = "train_service_code";
        public static string toc_id = "toc_id";
        public static string loc_stanox = "loc_stanox";
        public static string auto_expected = "auto_expected";
        public static string direction_ind = "direction_ind";
        public static string route = "route";
        public static string planned_event_type = "planned_event_type";
        public static string next_report_stanox = "next_report_stanox";
        public static string line_ind = "line_ind";

        public static string schedule_source = "schedule_source";
        public static string tp_origin_timestamp = "tp_origin_timestamp";
        public static string creation_timestamp = "creation_timestamp";
        public static string tp_origin_stanox = "tp_origin_stanox";
        public static string origin_dep_timestamp = "origin_dep_timestamp";
        public static string d1266_record_number = "d1266_record_number";
        public static string train_call_type = "train_call_type";
        public static string train_uid = "train_uid";
        public static string train_call_mode = "train_call_mode";
        public static string schedule_type = "schedule_type";
        public static string sched_origin_stanox = "sched_origin_stanox";
        public static string schedule_wtt_id = "schedule_wtt_id";

        public static string orig_loc_stanox = "orig_loc_stanox";
        public static string orig_loc_timestamp = "orig_loc_timestamp";
        public static string dep_timestamp = "dep_timestamp";
        public static string canx_timestamp = "canx_timestamp";
        public static string canx_reason_code = "canx_reason_code";
        public static string canx_type = "canx_type";

        public static string reinstatement_timestamp = "reinstatement_timestamp";
        public static string division_code_id = "division_code_id";

        public static string reason_code = "reason_code";
        public static string coo_timestamp = "coo_timestamp";

        public static string revised_train_id = "revised_train_id";
        public static string event_timestamp = "event_timestamp";


        // (TD) Train Data columns
        public static string time = "time";
        public static string area_id = "area_id";
        //public static string msg_type = "msg_type";
        public static string from = "from";
        public static string to = "to";
        public static string descr = "descr";
        public static string report_time = "report_time";

        // Signal Data Columns
        public static string address = "address";
        public static string data = "data";

        //public static string id = "id";
        // public static string tiploc_code = "tiploc_code";
        public static string atco_code = "atco_code";
        public static string station_name = "station_name";
        public static string northing = "northing";
        public static string easting = "easting";
        public static string latitude = "latitude";
        public static string longitude = "longitude";

        // OperatorRTPPM columns
        public static string name = "name";
        public static string code = "code";
        public static string total = "total";
        public static string onTime = "onTime";
        public static string late = "late";
        public static string cancelVeryLate = "cancelVeryLate";
        public static string PPMRag = "PPMRag";
        public static string PPMRagPercent = "PPMRagPercent";
        public static string rollingPPMTrendInd = "rollingPPMTrendInd";
        public static string rollingPPMDisplayFlag = "rollingPPMDisplayFlag";
        public static string rollingPPMRag = "rollingPPMRag";
        public static string rollingPPMPercentage = "rollingPPMPercentage";
        public static string snapshotTime = "snapshotTime";

        // OperatorToleranceTotal columns
        public static string operatorId = "operatorId";
        public static string timeband = "timeband";
        //public static string total = "name";
        //public static string onTime = "name";
        //public static string late = "name";
        //public static string cancelVeryLate = "name";

        //OperatorServiceGroup columns
        public static string sectorCode = "sectorCode";

        // ReferenceCorpusData columns
        public static string uic = "uic";
        public static string tiploc = "tiploc";
        public static string nlc = "nlc";
        public static string nlcdesc = "nlcdesc";

        // ReferenceSmartData columns
        public static string fromBerth = "fromBerth";
        public static string td = "td";
        //public static string stanox = "fromBerth";
        //public static string route = "fromBerth";
        public static string stepType = "stepType";
        public static string toBerth = "toBerth";
        public static string toLine = "toLine";
        //public static string platform = "fromBerth";
        public static string eventColumn = "event";
        public static string comment = "comment";
        public static string stanme = "stanme";
        public static string fromLine = "fromLine";

        // ReferenceTrainPlanningData columns
        public static string location_name = "location_name";
        public static string timing_point_type = "timing_point_type";




    }
}
