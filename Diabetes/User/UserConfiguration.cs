using System;


namespace Diabetes.User
{
    public class UserConfiguration
    {
        // User metrics
        private double _insulinSensitivityFactor;
        public double InsulinSensitivityFactor // How many units does blood glucose drop for every unit of insulin administered.
        {
            get { return _insulinSensitivityFactor; }
            set
            {
                if (value > 0)
                    _insulinSensitivityFactor = value;
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Insulin Sensitivity Factor value given: {value}. Insulin Sensitivity Factor must be greater than 0.");
                }
            }
        }

        private double _carbsToInsulinRation;
        public double CarbToInsulinRatio // How many carbs can 1 unit of insulin counteract.
        {
            get { return _carbsToInsulinRation; }
            set
            {
                if (value > 0)
                    _carbsToInsulinRation = value;
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Carbs to Insulin Ratio value given: {value}. Carbs to Insulin Ratio must be greater than 0.");
                }
            }
        }

        private int _longActingInsulinDoseRecommendation;

        public int LongActingInsulinDoesRecommendation // How many units of long acting insulin should be administered daily.
        {
            get { return _longActingInsulinDoseRecommendation; }
            set
            {
                if (value >= 0)
                    _longActingInsulinDoseRecommendation = value;
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Long Acting Insulin Does Recommendation value given: {value}. Long Acting Insulin Does Recommendation must be 0 or greater.");
                }
            }
        }
        
        // Action free isolation period
        private double _targetIsolationHours;
        public double TargetIsolationHours
        {
            get { return _targetIsolationHours; }
            set
            {
                if (value >= 0)
                    if (_minIsolationHours > value)
                        throw new ArgumentException(
                            "Target Isolation Hours must be greater than or equal to minimum target isolation Hours.");
                    else if (_maxIsolationHours < value)
                        throw new ArgumentException(
                            "Target Isolation Hours must be less than or equal to maximum target isolation Hours.");
                    else
                    {
                        _targetIsolationHours = value;
                    }
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Target Isolation Hours value given: {value}. Target Isolation Hours must be 0 or greater.");
                }
            }
        }

        private double _minIsolationHours;
        public double MinIsolationHours
        {
            get { return _minIsolationHours; }
            set
            {
                if (value >= 0)
                    if (_targetIsolationHours == null)
                        _minIsolationHours = value;
                    else if (_targetIsolationHours < value)
                        throw new ArgumentException(
                            "Target Isolation Hours must be greater than or equal to minimum target isolation Hours.");
                    else
                    {
                        _minIsolationHours = value;
                    }
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Minimum Isolation Hours value given: {value}. Minimum Isolation Hours must be 0 or greater.");
                }
            }
        }

        private double _maxIsolationHours;
        public double MaxIsolationHours
        {
            get { return _maxIsolationHours; }
            set
            {
                if (value >= 0)
                    if (_targetIsolationHours == null)
                        _minIsolationHours = value;
                    else if (_targetIsolationHours > value)
                        throw new ArgumentException(
                            "Target Isolation Hours must be less than or equal to maximum target isolation Hours.");
                    else
                    {
                        _maxIsolationHours = value;
                    }
                else
                {
                    throw new ArgumentException(
                        message:
                        $"Invalid Maximum Isolation Hours value given: {value}. Maximum Isolation Hours must be 0 or greater.");
                }
            }
        }
        
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