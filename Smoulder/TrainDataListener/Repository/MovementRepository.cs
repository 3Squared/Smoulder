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

        private void AddChangeOfIdentity(TrustMessage trustMessage)
        {
            var movementItem = (TrainChangeIdentityMsgV1)trustMessage.MessageData;
            var movementData = movementItem.TrainChangeIdentityData;
            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? eventTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementData.EventTimestamp);

            DataRow dataRow = changeOfIdentityDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue("0007");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TOPS");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");
            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.current_train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.revised_train_id] = GetValue(movementData.RevisedTrainID);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);
            dataRow[Columns.event_timestamp] = (object)eventTimestamp ?? DBNull.Value;

            changeOfIdentityDataTable.Rows.Add(dataRow);
        }

        private void AddChangeOfOrigin(TrustMessage trustMessage)
        {
            var movementItem = (TrainChangeOriginMsgV1)trustMessage.MessageData;
            var movementData = movementItem.TrainChangeOriginData;

            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? originalLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? cooTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementData.EventTimestamp);

            DataRow dataRow = changeOfOriginDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue("0006");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TRUST DA");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");
            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.original_loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.original_loc_timestamp] = (object)originalLocTimestamp ?? DBNull.Value;
            dataRow[Columns.current_train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);
            dataRow[Columns.reason_code] = GetValue(movementData.ReasonCode);
            dataRow[Columns.division_code] = GetValue(movementData.Division);
            dataRow[Columns.toc_id] = GetValue(movementData.TOC);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);
            dataRow[Columns.coo_timestamp] = (object)cooTimestamp ?? DBNull.Value;

            changeOfOriginDataTable.Rows.Add(dataRow);
        }

        private void AddReinstatement(TrustMessage trustMessage)
        {
            var movementItem = (TrainReinstatementMsgV1)trustMessage.MessageData;
            var movementData = movementItem.TrainReinstatementData;

            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? originalLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.LocationStanox);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? reinstatementTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);

            DataRow dataRow = reinstatementDataTable.NewRow();

            dataRow[Columns.msg_type] = GetValue("0005");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TOPS");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");
            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.current_train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.original_loc_timestamp] = (object)originalLocTimestamp ?? DBNull.Value;
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.original_loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.reinstatement_timestamp] = (object)reinstatementTimestamp ?? DBNull.Value;
            dataRow[Columns.division_code_id] = GetValue(movementData.Division);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);

            reinstatementDataTable.Rows.Add(dataRow);
        }

        private void AddTrainCancellation(TrustMessage trustMessage)
        {
            var movementItem = (TrainCancellationMsgV1)trustMessage.MessageData;
            var movementData = movementItem.TrainCancellationData;

            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? depTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? canxTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);
            DateTime? origLocTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.EventTimestamp);

            DataRow dataRow = cancellatoDataTable.NewRow();
            dataRow[Columns.msg_type] = GetValue("0002");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TOPS");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");

            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.orig_loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.orig_loc_timestamp] = (object)origLocTimestamp ?? DBNull.Value;
            dataRow[Columns.toc_id] = GetValue(movementData.TOC);
            dataRow[Columns.dep_timestamp] = (object)depTimestamp ?? DBNull.Value;
            dataRow[Columns.division_code] = GetValue(movementData.Division);
            dataRow[Columns.loc_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.canx_timestamp] = (object)canxTimestamp ?? DBNull.Value;
            dataRow[Columns.canx_reason_code] = GetValue(movementData.ReasonCode);
            dataRow[Columns.canx_type] = GetValue(movementData.TrainCancellationType);
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);

            cancellatoDataTable.Rows.Add(dataRow);

        }

        private void AddTrainActivation(TrustMessage trustMessage)
        {
            var movementItem = (TrainActivationMsgV1)trustMessage.MessageData;
            var movementData = movementItem.TrainActivationData;

            DateTime? msgQueueTimestamp = DateConverter.ConvertUnixEpochToDateTime(movementItem.Timestamp);
            DateTime? createdTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.TrainPlanOriginTimestamp);
            DateTime? originDepTimestamp = DateConverter.CorrectUnixEpochDaylightSavingDatTime(movementData.TrainPlanOriginTimestamp);

            DataRow dataRow = activationDataTable.NewRow();

            dataRow[Columns.msg_type] = GetValue("0001");
            dataRow[Columns.source_dev_id] = GetValue(movementItem.Sender.SessionID);
            dataRow[Columns.user_id] = GetValue(movementItem.Sender.UserID);
            dataRow[Columns.original_data_source] = GetValue("TSIA");
            dataRow[Columns.msg_queue_timestamp] = (object)msgQueueTimestamp ?? DBNull.Value;
            dataRow[Columns.source_system_id] = GetValue("TRUST");

            dataRow[Columns.schedule_source] = GetValue(movementData.ScheduleSource);
            dataRow[Columns.train_file_address] = GetValue(movementData.TrainFileAddress);
            dataRow[Columns.schedule_end_date] = GetValue(movementData.ScheduleEndTimestamp);
            dataRow[Columns.train_id] = GetValue(movementData.OriginalTrainID);
            dataRow[Columns.tp_origin_timestamp] = GetValue(movementData.TrainPlanOriginTimestamp);
            dataRow[Columns.creation_timestamp] = (object)createdTimestamp ?? DBNull.Value;
            dataRow[Columns.tp_origin_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.origin_dep_timestamp] = (object)originDepTimestamp ?? DBNull.Value;
            dataRow[Columns.train_service_code] = GetValue(movementData.TrainServiceCode);
            dataRow[Columns.toc_id] = GetValue(movementData.TOC);
            dataRow[Columns.d1266_record_number] = GetValue(movementData.TOPSUID);
            dataRow[Columns.train_call_type] = GetValue(movementData.TrainCallMode);
            dataRow[Columns.train_uid] = GetValue(movementData.UIDNumber);
            dataRow[Columns.train_call_mode] = GetValue(movementData.TrainCallMode);
            dataRow[Columns.schedule_type] = GetValue(movementData.ScheduleType);
            dataRow[Columns.sched_origin_stanox] = GetValue(movementData.LocationStanox);
            dataRow[Columns.schedule_wtt_id] = GetValue(movementData.ScheduledWTTID);
            dataRow[Columns.schedule_start_date] = GetValue(movementData.ScheduleStartTimestamp);

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

