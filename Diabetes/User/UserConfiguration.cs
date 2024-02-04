using System;


namespace Diabetes.User
{
    public class UserConfiguration
    {
        // User metrics
        public double InsulinSensitivityFactor { get; set; } // How many units does blood glucose drop for every unit of insulin administered.
        public double CarbToInsulinRatio { get; set; } // How many carbs can 1 unit of insulin counteract.
        public int LongActingInsulinDoesRecommendation { get; set; } // How many units of long acting insulin should be administered daily.
        
        // Action free isolation period
        public double TargetIsolationHours { get; set; }
        public double MinIsolationHours { get; set; }
        public double MaxIsolationHours { get; set; }
        
        // Long acting insulin recalculation config variables
        public TimeSpan OvernightStartTime { get; set; } // Start time of an overnight period for the user.
        public TimeSpan OvernightEndTime { get; set; } // End time of an overnight period for the user.
        public double MinHoursOvernightWithoutAction { get; set; } // Minimum number of hours that count as a sufficient overnight period for analysis.
        public double MaxHoursOvernightWithoutAction { get; set; } // Maximum number of hours that count as a sufficient overnight period for analysis.

        // Default user configuration object constructor
        public UserConfiguration()
        {
            // User metrics
            InsulinSensitivityFactor = 1; // Blood glucose level drops by 1 unit for every unit of insulin.
            CarbToInsulinRatio = 10; // 1 unit of insulin for every 10g of carbs.
            LongActingInsulinDoesRecommendation = 32; // 32 units of long acting insulin daily.
        
            // Action free isolation period
            TargetIsolationHours = 4; // Aim to disregard 4 hours of event readings to ensure all previous actions are no longer affecting the users blood glucose level whilst leaving sufficient remaining event time for analysis.
            MinHoursOvernightWithoutAction = 3.5; // Disregard at least the first 3.5 hours of events in the overnight period to ensure previous actions aren't still having an effect.
            MaxHoursOvernightWithoutAction = 4.5; // Disregard at most the first 4.5 hours of events in the overnight period to ensure that the period being analysed is long enough ToDo: Replace this with a check on check analysis length long enough

            
            // Long acting insulin recalculation config variables
            OvernightStartTime = new TimeSpan(20, 0, 0); // Overnight period for user starts at 8PM.
            OvernightEndTime = new TimeSpan(6, 0, 0); // Overnight period for user ends at 6AM.
            MinHoursOvernightWithoutAction = 7.5; // Ensure an overnight period without actions taken of at least 7.5 hours to leave time for sufficient isolation and analysis.
            MaxHoursOvernightWithoutAction = 8.5; // Ensure an overnight period without actions taken of  8.5 or less to try to stay within an overnight reading and avoid the dawn effect.
        }

        public void ValidateConfigurationObject()
        {
            
        }
    }
}