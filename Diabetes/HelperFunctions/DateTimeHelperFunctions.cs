using System;


namespace Diabetes.HelperFunctions
{
    public static class DateTimeHelperFunctions
    {
        public static (DateTime, DateTime) GetSleepingPeriodStartAndEndDateTimes(
            TimeSpan sleepPeriodStartTimeSpan, TimeSpan sleepPeriodEndTimeSpan)
        {
            // Get current date and time
            DateTime currentDateTime = DateTime.Now;
            TimeSpan currentTime = currentDateTime.TimeOfDay;
            
            // Get recent dates
            DateTime yesterday = currentDateTime.AddDays(-1);
            DateTime dayBeforeYesterday = yesterday.AddDays(-1);
            
            // Initialize sleep period start and end date times
            DateTime sleepPeriodStartDateTime;
            DateTime sleepPeriodEndDateTime;
            
            // If start time > end time (sleeping overnight)
            if (sleepPeriodStartTimeSpan >= sleepPeriodEndTimeSpan)
            {
                // If current time is:
                // before start time and after end time
                // or after start time
                if ((currentTime < sleepPeriodStartTimeSpan && currentTime > sleepPeriodEndTimeSpan) ||
                    (currentTime > sleepPeriodStartTimeSpan))
                {
                    sleepPeriodStartDateTime = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);

                    sleepPeriodEndDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month,
                        currentDateTime.Day,
                        sleepPeriodEndTimeSpan.Hours, sleepPeriodEndTimeSpan.Minutes,
                        sleepPeriodEndTimeSpan.Seconds);
                }
                
                // If before end time
                else
                {
                    sleepPeriodStartDateTime = new DateTime(
                        dayBeforeYesterday.Year, dayBeforeYesterday.Month, dayBeforeYesterday.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);
                    
                    sleepPeriodEndDateTime = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day,
                        sleepPeriodEndTimeSpan.Hours, sleepPeriodEndTimeSpan.Minutes,
                        sleepPeriodEndTimeSpan.Seconds);
                }
            }

            //  If start time < end time (sleeping in same day)
            else
            {
                // If current time is:
                // after start time and before end time
                // or before start time
                if ((currentTime > sleepPeriodStartTimeSpan && currentTime < sleepPeriodEndTimeSpan) ||
                    (currentTime < sleepPeriodStartTimeSpan))
                {
                    sleepPeriodStartDateTime = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);

                    sleepPeriodEndDateTime = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);
                }

                // if after end time
                else
                {
                    sleepPeriodStartDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);

                    sleepPeriodEndDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day,
                        sleepPeriodStartTimeSpan.Hours, sleepPeriodStartTimeSpan.Minutes,
                        sleepPeriodStartTimeSpan.Seconds);
                }
            }
            
            return (sleepPeriodStartDateTime, sleepPeriodEndDateTime);
        }
    }
}