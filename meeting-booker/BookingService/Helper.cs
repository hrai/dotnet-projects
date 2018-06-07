
using System;
using System.Globalization;
using Entities;

namespace BookingModuleService
{
    public static class Helper
    {
        public static Meeting CreateMeeting(string startTime, string duration)
        {
            var dtStartTime = DateTime.Parse(startTime);
            var tsDuration = TimeSpan.FromHours(Double.Parse(duration));

            return new Meeting
            {
                StartTime = dtStartTime,
                FinishTime = dtStartTime.AddMilliseconds(tsDuration.TotalMilliseconds)
            };
        }

        public static Office CreateOffice(string officeHours)
        {
            var officeHourArray = officeHours.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new Office
            {
                StartTime = ParseCustomTime(officeHourArray[0]),
                FinishTime = ParseCustomTime(officeHourArray[1]),
            };
        }

        private static DateTime ParseCustomTime(string time)
        {
            string pattern = "HHmm";
            DateTime dtTime;
            if (DateTime.TryParseExact(time, pattern, CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out dtTime))
            {
                return dtTime;
            }

            throw new ArgumentException("Invalid date argument");
        }


    }
}