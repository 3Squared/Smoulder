using System;

namespace TrainDataListener.Repository
{
    public static class DateConverter
    {
        private const int HoursMilliseconds = 3600000;

        /// <summary>
        /// Convert the Unix epoch time to a DateTime this is from 1970
        /// </summary>
        /// <param name="unixTimeStamp">The unix epoch timestamp in milliseconds e.g. 1441186352000</param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixEpoch(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Convert the Unix epoch time to a nullable DateTime
        /// </summary>
        /// <param name="unixEpochTime"></param>
        /// <returns></returns>
        public static DateTime? ConvertUnixEpochToDateTime(string unixEpochTime)
        {
            if (String.IsNullOrEmpty(unixEpochTime))
            {
                return null;
            }
            long timestamp = Convert.ToInt64(unixEpochTime);

            return ConvertFromUnixEpoch(timestamp);
        }

        [Obsolete("Use the CorrectUnixEpochDaylightSavingDatTime instead")]
        public static DateTime? ConvertAndCorrectUnixEpochToDateTime(string unixEpochTime)
        {
            if (String.IsNullOrEmpty(unixEpochTime))
            {
                return null;
            }
            long timestamp = Convert.ToInt64(unixEpochTime) - HoursMilliseconds;

            return ConvertFromUnixEpoch(timestamp);
        }

        public static DateTime? CorrectUnixEpochDaylightSavingDatTime(string unixEpochTime)
        {
            if (String.IsNullOrEmpty(unixEpochTime))
            {
                return null;
            }
            long timestamp = Convert.ToInt64(unixEpochTime);

            DateTime dateTime = ConvertFromUnixEpoch(timestamp);

            bool isDaylightSavingTime = TimeZoneInfo.Local.IsDaylightSavingTime(dateTime);
            if (isDaylightSavingTime)
            {
                timestamp = Convert.ToInt64(unixEpochTime) - HoursMilliseconds;
                return ConvertFromUnixEpoch(timestamp);
            }
            return dateTime;
        }

        public static string GetSqlDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
