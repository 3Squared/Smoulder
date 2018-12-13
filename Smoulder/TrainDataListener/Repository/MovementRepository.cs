using System;
using System.Collections.Generic;
using System.Data;
using TrainDataListener.TrainData;

namespace TrainDataListener.Repository
{
    public class SqlServerMovementRepository
    {

        private static DataTable movementDataTable = new DataTable("Movement");
        private static DataTable activationDataTable = new DataTable("MovementActivation");
        private static DataTable cancellatoDataTable = new DataTable("MovementCancellation");
        private static DataTable reinstatementDataTable = new DataTable("MovementReinstatement");
        private static DataTable changeOfOriginDataTable = new DataTable("MovementChangeOfOrigin");
        private static DataTable changeOfIdentityDataTable = new DataTable("MovementChangeOfIdentity");

        protected string GetValue(string value)
        {
            if (value == null) return null;
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return value;
        }

        static SqlServerMovementRepository()
        {
            movementDataTable.Columns.Add(new DataColumn(Columns.id));
            movementDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            movementDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.user_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            movementDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            movementDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.event_type));
            movementDataTable.Columns.Add(new DataColumn(Columns.gbtt_timestamp, typeof(DateTime)));
            movementDataTable.Columns.Add(new DataColumn(Columns.original_loc_stanox));
            movementDataTable.Columns.Add(new DataColumn(Columns.planned_timestamp, typeof(DateTime)));
            movementDataTable.Columns.Add(new DataColumn(Columns.timetable_variation));
            movementDataTable.Columns.Add(new DataColumn(Columns.original_loc_timestamp, typeof(DateTime)));
            movementDataTable.Columns.Add(new DataColumn(Columns.current_train_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.delay_monitoring_point));
            movementDataTable.Columns.Add(new DataColumn(Columns.next_report_run_time));
            movementDataTable.Columns.Add(new DataColumn(Columns.reporting_stanox));
            movementDataTable.Columns.Add(new DataColumn(Columns.actual_timestamp, typeof(DateTime)));
            movementDataTable.Columns.Add(new DataColumn(Columns.correction_ind));
            movementDataTable.Columns.Add(new DataColumn(Columns.event_source));
            movementDataTable.Columns.Add(new DataColumn(Columns.train_file_address));
            movementDataTable.Columns.Add(new DataColumn(Columns.division_code));
            movementDataTable.Columns.Add(new DataColumn(Columns.train_terminated));
            movementDataTable.Columns.Add(new DataColumn(Columns.train_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.offroute_ind));
            movementDataTable.Columns.Add(new DataColumn(Columns.variation_status));
            movementDataTable.Columns.Add(new DataColumn(Columns.train_service_code));
            movementDataTable.Columns.Add(new DataColumn(Columns.toc_id));
            movementDataTable.Columns.Add(new DataColumn(Columns.loc_stanox));
            movementDataTable.Columns.Add(new DataColumn(Columns.auto_expected));
            movementDataTable.Columns.Add(new DataColumn(Columns.direction_ind));
            movementDataTable.Columns.Add(new DataColumn(Columns.route));
            movementDataTable.Columns.Add(new DataColumn(Columns.planned_event_type));
            movementDataTable.Columns.Add(new DataColumn(Columns.next_report_stanox));

            activationDataTable.Columns.Add(new DataColumn(Columns.id));
            activationDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            activationDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.user_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            activationDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            activationDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.schedule_source));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_file_address));
            activationDataTable.Columns.Add(new DataColumn(Columns.schedule_end_date, typeof(DateTime)));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.tp_origin_timestamp, typeof(DateTime)));
            activationDataTable.Columns.Add(new DataColumn(Columns.creation_timestamp, typeof(DateTime)));
            activationDataTable.Columns.Add(new DataColumn(Columns.tp_origin_stanox));
            activationDataTable.Columns.Add(new DataColumn(Columns.origin_dep_timestamp, typeof(DateTime)));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_service_code));
            activationDataTable.Columns.Add(new DataColumn(Columns.toc_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.d1266_record_number));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_call_type));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_uid));
            activationDataTable.Columns.Add(new DataColumn(Columns.train_call_mode));
            activationDataTable.Columns.Add(new DataColumn(Columns.schedule_type));
            activationDataTable.Columns.Add(new DataColumn(Columns.sched_origin_stanox));
            activationDataTable.Columns.Add(new DataColumn(Columns.schedule_wtt_id));
            activationDataTable.Columns.Add(new DataColumn(Columns.schedule_start_date, typeof(DateTime)));

            cancellatoDataTable.Columns.Add(new DataColumn(Columns.id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.user_id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.train_id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.orig_loc_stanox));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.orig_loc_timestamp, typeof(DateTime)));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.toc_id));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.dep_timestamp, typeof(DateTime)));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.division_code));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.loc_stanox));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.canx_timestamp, typeof(DateTime)));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.canx_reason_code));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.canx_type));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.train_service_code));
            cancellatoDataTable.Columns.Add(new DataColumn(Columns.train_file_address));

            reinstatementDataTable.Columns.Add(new DataColumn(Columns.id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.user_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.train_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.current_train_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.original_loc_timestamp, typeof(DateTime)));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.dep_timestamp, typeof(DateTime)));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.loc_stanox));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.original_loc_stanox));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.reinstatement_timestamp, typeof(DateTime)));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.division_code_id));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.train_file_address));
            reinstatementDataTable.Columns.Add(new DataColumn(Columns.train_service_code));

            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.user_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.train_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.dep_timestamp, typeof(DateTime)));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.loc_stanox));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.original_loc_stanox));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.original_loc_timestamp, typeof(DateTime)));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.current_train_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.train_service_code));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.reason_code));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.division_code));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.toc_id));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.train_file_address));
            changeOfOriginDataTable.Columns.Add(new DataColumn(Columns.coo_timestamp, typeof(DateTime)));

            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.msg_type));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.source_dev_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.user_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.original_data_source));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.msg_queue_timestamp, typeof(DateTime)));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.source_system_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.train_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.current_train_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.revised_train_id));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.train_file_address));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.train_service_code));
            changeOfIdentityDataTable.Columns.Add(new DataColumn(Columns.event_timestamp));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movementItems"></param>    
        public void AddTrustMessage(TrustMessage trustMessage)
        {

            switch (trustMessage.MessageType)
            {
                case TrustMessageType.Movement:
                    AddTrainMovement(trustMessage);
                    break;
                case TrustMessageType.Activation:
                    AddTrainActivation(trustMessage);
                    break;
                case TrustMessageType.Cancellation:
                    AddTrainCancellation(trustMessage);
                    break;
                case TrustMessageType.ChangeOrigin:
                    AddChangeOfOrigin(trustMessage);
                    break;
                case TrustMessageType.ChangeIdentity:
                    AddChangeOfIdentity(trustMessage);
                    break;
                case TrustMessageType.Reinstatement:
                    AddReinstatement(trustMessage);
                    break;
            }
        }

        private void AddChangeOfIdentity(MovementItem movementItem)
        {
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.header.msg_queue_timestamp);
            DateTime? eventTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementData.event_timestamp);

            DataRow dataRow = changeOfIdentityDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue(movementItem.header.msg_type);
            dataRow[Columns.source_dev_id] = GetValue(movementItem.header.source_dev_id);
            dataRow[Columns.user_id] = GetValue(movementItem.header.user_id);
            dataRow[Columns.original_data_source] = GetValue(movementItem.header.original_data_source);
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue(movementItem.header.source_system_id);
            dataRow[Columns.train_id] = GetValue(movementData.train_id);
            dataRow[Columns.current_train_id] = GetValue(movementData.current_train_id);
            dataRow[Columns.revised_train_id] = GetValue(movementData.revised_train_id);
            dataRow[Columns.train_file_address] = GetValue(movementData.train_file_address);
            dataRow[Columns.train_service_code] = GetValue(movementData.train_service_code);
            dataRow[Columns.event_timestamp] = (object)eventTimestamp ?? DBNull.Value;

            changeOfIdentityDataTable.Rows.Add(dataRow);
        }

        private void AddChangeOfOrigin(MovementItem movementItem)
        {
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.header.msg_queue_timestamp);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.dep_timestamp);
            DateTime? originalLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.original_loc_timestamp);
            DateTime? cooTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementData.coo_timestamp);

            DataRow dataRow = changeOfOriginDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue(movementItem.header.msg_type);
            dataRow[Columns.source_dev_id] = GetValue(movementItem.header.source_dev_id);
            dataRow[Columns.user_id] = GetValue(movementItem.header.user_id);
            dataRow[Columns.original_data_source] = GetValue(movementItem.header.original_data_source);
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue(movementItem.header.source_system_id);
            dataRow[Columns.train_id] = GetValue(movementData.train_id);
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.loc_stanox] = GetValue(movementData.loc_stanox);
            dataRow[Columns.original_loc_stanox] = GetValue(movementData.original_loc_stanox);
            dataRow[Columns.original_loc_timestamp] = (object)originalLocTimestamp ?? DBNull.Value;
            dataRow[Columns.current_train_id] = GetValue(movementData.current_train_id);
            dataRow[Columns.train_service_code] = GetValue(movementData.train_service_code);
            dataRow[Columns.reason_code] = GetValue(movementData.reason_code);
            dataRow[Columns.division_code] = GetValue(movementData.division_code);
            dataRow[Columns.toc_id] = GetValue(movementData.toc_id);
            dataRow[Columns.train_file_address] = GetValue(movementData.train_file_address);
            dataRow[Columns.coo_timestamp] = (object)cooTimestamp ?? DBNull.Value;

            changeOfOriginDataTable.Rows.Add(dataRow);
        }

        private void AddReinstatement(MovementItem movementItem)
        {
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.header.msg_queue_timestamp);
            DateTime? originalLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.original_loc_timestamp);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.dep_timestamp);
            DateTime? reinstatementTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.reinstatement_timestamp);

            DataRow dataRow = reinstatementDataTable.NewRow();

            dataRow[Columns.msg_type] = GetValue(movementItem.header.msg_type);
            dataRow[Columns.source_dev_id] = GetValue(movementItem.header.source_dev_id);
            dataRow[Columns.user_id] = GetValue(movementItem.header.user_id);
            dataRow[Columns.original_data_source] = GetValue(movementItem.header.original_data_source);
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue(movementItem.header.source_system_id);
            dataRow[Columns.train_id] = GetValue(movementData.train_id);
            dataRow[Columns.current_train_id] = GetValue(movementData.current_train_id);
            dataRow[Columns.original_loc_timestamp] = (object)originalLocTimestamp ?? DBNull.Value;
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.loc_stanox] = GetValue(movementData.loc_stanox);
            dataRow[Columns.original_loc_stanox] = GetValue(movementData.original_loc_stanox);
            dataRow[Columns.reinstatement_timestamp] = (object)reinstatementTimestamp ?? DBNull.Value;
            dataRow[Columns.division_code_id] = GetValue(movementData.division_code_id);
            dataRow[Columns.train_file_address] = GetValue(movementData.train_file_address);
            dataRow[Columns.train_service_code] = GetValue(movementData.train_service_code);

            reinstatementDataTable.Rows.Add(dataRow);
        }

        private void AddTrainCancellation(MovementItem movementItem)
        {
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.header.msg_queue_timestamp);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.dep_timestamp);
            DateTime? canxTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.canx_timestamp);
            DateTime? origLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.orig_loc_timestamp);

            DataRow dataRow = cancellatoDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue(movementItem.header.msg_type);
            dataRow[Columns.source_dev_id] = GetValue(movementItem.header.source_dev_id);
            dataRow[Columns.user_id] = GetValue(movementItem.header.user_id);
            dataRow[Columns.original_data_source] = GetValue(movementItem.header.original_data_source);
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue(movementItem.header.source_system_id);

            dataRow[Columns.train_id] = GetValue(movementData.train_id);
            dataRow[Columns.orig_loc_stanox] = GetValue(movementData.orig_loc_stanox);
            dataRow[Columns.orig_loc_timestamp] = (object)origLocTimestamp ?? DBNull.Value;
            dataRow[Columns.toc_id] = GetValue(movementData.toc_id);
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.division_code] = GetValue(movementData.division_code);
            dataRow[Columns.loc_stanox] = GetValue(movementData.loc_stanox);
            dataRow[Columns.canx_timestamp] = (object)canxTimestamp ?? DBNull.Value;
            dataRow[Columns.canx_reason_code] = GetValue(movementData.canx_reason_code);
            dataRow[Columns.canx_type] = GetValue(movementData.canx_type);
            dataRow[Columns.train_service_code] = GetValue(movementData.train_service_code);
            dataRow[Columns.train_file_address] = GetValue(movementData.train_file_address);

            cancellatoDataTable.Rows.Add(dataRow);

        }

        private void AddTrainActivation(MovementItem movementItem)
        {
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.header.msg_queue_timestamp);
            DateTime? createdTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.creation_timestamp);
            DateTime? originDepTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.origin_dep_timestamp);

            DataRow dataRow = activationDataTable.NewRow();

            dataRow[Columns.msg_type] = GetValue(movementItem.header.msg_type);
            dataRow[Columns.source_dev_id] = GetValue(movementItem.header.source_dev_id);
            dataRow[Columns.user_id] = GetValue(movementItem.header.user_id);
            dataRow[Columns.original_data_source] = GetValue(movementItem.header.original_data_source);
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue(movementItem.header.source_system_id);

            dataRow[Columns.schedule_source] = GetValue(movementData.schedule_source);
            dataRow[Columns.train_file_address] = GetValue(movementData.train_file_address);
            dataRow[Columns.schedule_end_date] = GetValue(movementData.schedule_end_date);
            dataRow[Columns.train_id] = GetValue(movementData.train_id);
            dataRow[Columns.tp_origin_timestamp] = GetValue(movementData.tp_origin_timestamp);
            dataRow[Columns.creation_timestamp] = (object)createdTimestamp ?? DBNull.Value;
            dataRow[Columns.tp_origin_stanox] = GetValue(movementData.tp_origin_stanox);
            dataRow[Columns.origin_dep_timestamp] = (object)originDepTimestamp ?? DBNull.Value;
            dataRow[Columns.train_service_code] = GetValue(movementData.train_service_code);
            dataRow[Columns.toc_id] = GetValue(movementData.toc_id);
            dataRow[Columns.d1266_record_number] = GetValue(movementData.d1266_record_number);
            dataRow[Columns.train_call_type] = GetValue(movementData.train_call_type);
            dataRow[Columns.train_uid] = GetValue(movementData.train_uid);
            dataRow[Columns.train_call_mode] = GetValue(movementData.train_call_mode);
            dataRow[Columns.schedule_type] = GetValue(movementData.schedule_type);
            dataRow[Columns.sched_origin_stanox] = GetValue(movementData.sched_origin_stanox);
            dataRow[Columns.schedule_wtt_id] = GetValue(movementData.schedule_wtt_id);
            dataRow[Columns.schedule_start_date] = GetValue(movementData.schedule_start_date);

            activationDataTable.Rows.Add(dataRow);
        }

        private void AddTrainMovement(TrustMessage trustMessage)
        {
            var movementItem = (TrainMovementMsgV1) trustMessage.MessageData;
            var movementData = movementItem.TrainMovementData;
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? gbttTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementItem.TrainMovementData.GBTTTimestamp);
            DateTime? actualTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? originalLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? plannedTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);

            DataRow dataRow = movementDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue("0003");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TRUST");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");
            dataRow[Columns.event_type] = GetValue(movementData.MovementType);
            dataRow[Columns.gbtt_timestamp] = (object)gbttTimestamp ?? DBNull.Value;
            dataRow[Columns.original_loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.planned_timestamp] = (object)plannedTimestamp ?? DBNull.Value;
            dataRow[Columns.timetable_variation] = GetValue(movementData.TimetableVariation);
            dataRow[Columns.original_loc_timestamp] = (object)originalLocTimestamp ?? DBNull.Value;
            dataRow[Columns.current_train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.delay_monitoring_point] = GetValue(movementData.DelayMonitoringFlag);
            dataRow[Columns.next_report_run_time] = GetValue(movementData.NextReportRunTime);
            dataRow[Columns.reporting_stanox] = GetValue(movementData.ReportingLocationStanox);
            dataRow[Columns.actual_timestamp] = (object)actualTimestamp ?? DBNull.Value;
            dataRow[Columns.correction_ind] = GetValue("False");
            dataRow[Columns.event_source] = GetValue(movementData.EventSource);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);
            dataRow[Columns.division_code] = GetValue(movementData.Division);
            dataRow[Columns.train_terminated] = GetValue(movementData.TerminatedFlag);
            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.offroute_ind] = GetValue(movementData.OffRouteFlag);
            dataRow[Columns.variation_status] = GetValue(movementData.VariationStatus);
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);
            dataRow[Columns.toc_id] = GetValue(movementData.TOC);
            dataRow[Columns.loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.auto_expected] = GetValue(movementData.AutoExpectedFlag);
            dataRow[Columns.direction_ind] = GetValue(movementData.Direction);
            dataRow[Columns.route] = GetValue(movementData.Route);
            dataRow[Columns.planned_event_type] = GetValue(movementData.PlannedMovementType);
            dataRow[Columns.next_report_stanox] = GetValue(movementData.NextLocationStanox);
            movementDataTable.Rows.Add(dataRow);

        }

        public void Flush()
        {
            WriteDataTableToDatabase(movementDataTable);
            WriteDataTableToDatabase(activationDataTable);
            WriteDataTableToDatabase(cancellatoDataTable);
            WriteDataTableToDatabase(reinstatementDataTable);
            WriteDataTableToDatabase(changeOfOriginDataTable);
            WriteDataTableToDatabase(changeOfIdentityDataTable);
            CleanDataTables();
        }

        private void CleanDataTables()
        {
            movementDataTable.Clear();
            activationDataTable.Clear();
            cancellatoDataTable.Clear();
            reinstatementDataTable.Clear();
            changeOfOriginDataTable.Clear();
            changeOfIdentityDataTable.Clear();
        }

    }
}

