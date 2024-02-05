using System;


namespace Diabetes.HelperFunctions
{
    public static class DateTimeHelperFunctions
    {
        public static DateTime GetMostRecentDateTime(TimeSpan timeSpanToConvert)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeSpan currentTimeSpan = currentDateTime.TimeOfDay;

            if (timeSpanToConvert > currentTimeSpan)
            {
                // Time has passed in current day, return current date and input time
                return new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                    timeSpanToConvert.Hours, timeSpanToConvert.Minutes, timeSpanToConvert.Seconds);
            }
            else
            {
                // Time has not passed in current day, return yesterdays date and input time
                DateTime previousDateTime = currentDateTime.AddDays(-1);
                return new DateTime(previousDateTime.Year, previousDateTime.Month, previousDateTime.Day,
                    timeSpanToConvert.Hours, previousDateTime.Minute, previousDateTime.Second);
            }
        }
    }
}