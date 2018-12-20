using System;
using System.Data;
using System.Linq;
using Dapper;
using TrainDataListener.ScheduleData;
using TrainDataListener.TrainData;

namespace TrainDataListener.Repository
{
    public class ScheduleRepository : BaseRepository
    {
        private static DataTable scheduleDataTable = new DataTable("Schedule");
        private static DataTable scheduleSegmentDataTable = new DataTable("ScheduleSegment");
        private static DataTable scheduleLocationDataTable = new DataTable("ScheduleLocation");
        private static DataTable cachedTrainDataTable = new DataTable("cachedTrainData");
        private static DataTable cachedTrainDataArchiveTable = new DataTable("cachedTrainDataArchive");

        public void AddSchedule(TrustMessage trustMessage)
        {
            var activationMessage = (TrainActivationMsgV1) trustMessage.MessageData;
            var activationData = activationMessage.TrainActivationData;

            var startDate = DateTime.Parse(activationData.ScheduleStartTimestamp).Date;
            var endDate = DateTime.Parse(activationData.ScheduleStartTimestamp).Date;
            var trainUid = activationData.UIDNumber;

            var schedule = GetScheduleData(startDate, trainUid);
            if (schedule == null)
            {
                return;
            }
            var scheduleSegment = schedule.scheduleSegments.First();
            var locations = scheduleSegment.scheduleLocations;
            if (locations.Count == 0)
            {
                return;
            }
            var departureLocation = locations.Where(x => !string.IsNullOrWhiteSpace(x.departure)).OrderBy(x => x.departure).First();
            var arrivalLocation = locations.Where(x => !string.IsNullOrWhiteSpace(x.arrival)).OrderByDescending(x => x.arrival).Last();

            var activated_departure_timestamp = DateTime.Parse(activationData.EventTimestamp);
            var activatedDate = activated_departure_timestamp.Date;
            var scheduled_arrival = activatedDate.AddHours(int.Parse(arrivalLocation.arrival.Substring(0, 2))).AddMinutes(int.Parse(arrivalLocation.arrival.Substring(2, 2)));
            var scheduled_departure = activatedDate.AddHours(int.Parse(departureLocation.departure.Substring(0, 2))).AddMinutes(int.Parse(departureLocation.departure.Substring(2, 2)));

            if (scheduled_arrival < scheduled_departure)
            {
                scheduled_arrival = scheduled_arrival.AddDays(1);
            }

            var sql = @"INSERT INTO CachedTrainData (activation_id,
            schedule_id,
            head_code,
            train_id,
            toc_id,
            train_uid,
            train_service_code,
            activated_departure_timestamp,
            actual_departure_timestamp,
            origin,
            scheduled_departure,
            scheduled_arrival,
            destination,
            last_reported_timestamp,
            last_reported_delay,
            last_reported_location,
            last_reported_type,
            cancelled,
            cancelled_at_origin,
            cancelled_immediatly,
            cancelled_en_route,
            cancelled_out_of_plan,
            cancellation_timestamp,
            schedule_cancelled,
            stp_start_indicator_index,
            stp_end_indicator_index,
            schedule_just_for_today,
            has_schedule,
            should_have_departed_exception
                ) Values (@activation_id,
            @schedule_id,
            @head_code,
            @train_id,
            @toc_id,
            @train_uid,
            @train_service_code,
            @activated_departure_timestamp,
            @actual_departure_timestamp,
            @origin,
            @scheduled_departure,
            @scheduled_arrival,
            @destination,
            @last_reported_timestamp,
            @last_reported_delay,
            @last_reported_location,
            @last_reported_type,
            @cancelled,
            @cancelled_at_origin,
            @cancelled_immediatly,
            @cancelled_en_route,
            @cancelled_out_of_plan,
            @cancellation_timestamp,
            @schedule_cancelled,
            @stp_start_indicator_index,
            @stp_end_indicator_index,
            @schedule_just_for_today,
            @has_schedule,
            @should_have_departed_exception);";

            using (IDbConnection connection = GetSqlConnection())
            {
                var affectedRows = connection.Execute(sql, new
                {
                    @activated_departure_timestamp = activated_departure_timestamp,
                    @activation_id = 1,
                    @actual_departure_timestamp = (DateTime?) null,
                    @cancellation_timestamp = (DateTime?) null,
                    @cancelled = false,
                    @cancelled_at_origin = false,
                    @cancelled_en_route = false,
                    @cancelled_immediatly = false,
                    @cancelled_out_of_plan = false,
                    @schedule_cancelled = false,
                    @destination = arrivalLocation.tiploc_code, //Should be converted via ReferenceCorpusData
                    @has_schedule = true,
                    @head_code = activationData.OriginalTrainID.Substring(3,4),
                    @last_reported_delay = 0,
                    @last_reported_location = (string) null,
                    @last_reported_timestamp = (DateTime?)null,
                    @last_reported_type = (string) null,
                    @origin = departureLocation.tiploc_code, //Should be converted via ReferenceCorpusData
                    @schedule_id = schedule.id,
                    @schedule_just_for_today = false,
                    @scheduled_arrival = scheduled_arrival,
                    @scheduled_departure = scheduled_departure,
                    @should_have_departed_exception = false,
                    @stp_end_indicator_index = 0,
                    @stp_start_indicator_index = 0,
                    @toc_id = int.Parse(activationData.TOC),
                    @train_id = activationData.OriginalTrainID,
                    @train_uid = activationData.UIDNumber,
                    @train_service_code = activationData.TrainServiceCode
                });
            }
        }

        public Schedule GetScheduleData(DateTime startDate, string trainUid)
        {
            var dateString = startDate.ToString("yyyy-M-d");
            var getSchedule = "Select * From Schedule WHERE schedule_start_date = '" + dateString +
                              "' AND CIF_train_uid = '" + trainUid + "'";
            using (IDbConnection conn = GetSqlConnection())
            {
                var schedule = conn.Query<Schedule>(getSchedule).FirstOrDefault();
                if (schedule != null)
                {
                    var getSegments = "Select * From ScheduleSegment WHERE schedule_id = '" + schedule.id + "'";
                    schedule.scheduleSegments = conn.Query<ScheduleSegment>(getSegments).ToList();
                    foreach (var segment in schedule.scheduleSegments)
                    {
                        var getLocations = "Select * From ScheduleLocation WHERE schedule_segment_id = '" + segment.id + "'";
                        segment.scheduleLocations = conn.Query<ScheduleLocation>(getLocations).ToList();
                    }
                }
                return schedule;
            }
        }

        public void ProcessSchedule(TrustMessage trustMessage)
        {
            switch (trustMessage.MessageType)
            {
                case TrustMessageType.Movement:
                    AddTrainMovement(trustMessage);
                    break;
                case TrustMessageType.Activation:
                    AddSchedule(trustMessage);
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

        private void AddReinstatement(TrustMessage trustMessage)
        {
            var reinstatementMessage = (TrainReinstatementMsgV1)trustMessage.MessageData;
            var reinstatementData = reinstatementMessage.TrainReinstatementData;
        }

        private void AddChangeOfIdentity(TrustMessage trustMessage)
        {
            var changeIdentityMessage = (TrainChangeIdentityMsgV1)trustMessage.MessageData;
            var changeIdentityData = changeIdentityMessage.TrainChangeIdentityData;
        }

        private void AddChangeOfOrigin(TrustMessage trustMessage)
        {
            var changeIdentityMessage = (TrainChangeOriginMsgV1)trustMessage.MessageData;
            var changeIdentityData = changeIdentityMessage.TrainChangeOriginData;
        }

        private void AddTrainCancellation(TrustMessage trustMessage)
        {
            var cancellationMessage = (TrainCancellationMsgV1)trustMessage.MessageData;
            var cancellationData = cancellationMessage.TrainCancellationData;
        }

        private void AddTrainMovement(TrustMessage trustMessage)
        {
            var movementMessage = (TrainMovementMsgV1)trustMessage.MessageData;
            var movementData = movementMessage.TrainMovementData;

            switch (movementData.MovementType)
            {
                case "DEPARTURE":
                    DepartureMovement(movementData);
                    break;
                case "ARRIVAL":
                    Movement(movementData);
                    break;
                case "TERMINATION":
                    Movement(movementData);
                    break;
                case "CANCELLATION":
                    CancelledMovement(movementData);
                    break;
                default:
                    Movement(movementData);
                    break;
            }
        }

        private void CancelledMovement(TrainMovementData movementData)
        {
            var sql = @"Update CachedTrainData set
            cancelled = @cancelled,
            cancellation_timestamp = @cancellation_timestamp,
            last_reported_timestamp = @last_reported_timestamp,
            last_reported_delay = @last_reported_delay,
            last_reported_location = @last_reported_location,
            last_reported_type = @last_reported_type
            WHERE train_id = @train_id
            AND train_service_code = @train_service_code
            AND toc_id = toc_id;";

            using (IDbConnection connection = GetSqlConnection())
            {
                var affectedRows = connection.Execute(sql, new
                {
                    @Cancelled = true,
                    @cancellation_timestamp = movementData.EventTimestamp,
                    @last_reported_delay = movementData.Delay,
                    @last_reported_location = movementData.ReportingLocationStanox,
                    @last_reported_timestamp = movementData.EventTimestamp,
                    @last_reported_type = movementData.MovementType,
                    @train_id = movementData.OriginalTrainID,
                    @train_service_code = movementData.TrainServiceCode,
                    @toc_id = movementData.TOC
                });
            }
        }

        private void DepartureMovement(TrainMovementData movementData)
        {
            var sql = @"Update CachedTrainData set
            actual_departure_timestamp = @actual_departure_timestamp,
            last_reported_timestamp = @last_reported_timestamp,
            last_reported_delay = @last_reported_delay,
            last_reported_location = @last_reported_location,
            last_reported_type = @last_reported_type
            WHERE train_id = @train_id
            AND train_service_code = @train_service_code
            AND toc_id = toc_id;";


            using (IDbConnection connection = GetSqlConnection())
            {
                var affectedRows = connection.Execute(sql, new
                {
                    @actual_departure_timestamp = movementData.EventTimestamp,
                    @last_reported_delay = movementData.Delay,
                    @last_reported_location = movementData.ReportingLocationStanox,
                    @last_reported_timestamp = movementData.EventTimestamp,
                    @last_reported_type = movementData.MovementType,
                    @train_id = movementData.OriginalTrainID,
                    @train_service_code = movementData.TrainServiceCode,
                    @toc_id = movementData.TOC
                });
            }
        }

        private void Movement(TrainMovementData movementData)
        {
            var sql = @"Update CachedTrainData set
            last_reported_timestamp = @last_reported_timestamp,
            last_reported_delay = @last_reported_delay,
            last_reported_location = @last_reported_location,
            last_reported_type = @last_reported_type
            WHERE train_id = @train_id
            AND train_service_code = @train_service_code
            AND toc_id = toc_id;";

            using (IDbConnection connection = GetSqlConnection())
            {
                var affectedRows = connection.Execute(sql, new
                {
                    @last_reported_delay = movementData.Delay,
                    @last_reported_location = movementData.ReportingLocationStanox,
                    @last_reported_timestamp = movementData.EventTimestamp,
                    @last_reported_type = movementData.MovementType,
                    @train_id = movementData.OriginalTrainID,
                    @train_service_code = movementData.TrainServiceCode,
                    @toc_id = movementData.TOC
                });
            }
        }

        public int CleanUp()
        {
            const string sql = @"DELETE FROM [dbo].[CachedTrainData]
            WHERE scheduled_arrival < CONVERT(DATETIME, CONVERT(VARCHAR(10), DATEADD(D, -1, GETDATE()), 126) + ' 00:00:00')";

            using (IDbConnection connection = GetSqlConnection())
            {
                return connection.Execute(sql);
            }
        }
    }
}
