using System;
using System.Collections.Generic;

using Diabetes.ExternalStorage.DataModels;
using Diabetes.ExternalStorage.ErrorMessages;


namespace Diabetes.ExternalStorage.DataModels
{
    /// <summary>
    /// Holds data model for UserConfiguration
    /// (Including validation for negative and zero values)
    ///
    /// Creates default values for instantiation
    ///
    /// Validates relationships between properties
    /// </summary>
    public class UserConfiguration : IDataModel
    {
        // User metrics
        private double _insulinSensitivityFactor;

        public double
            InsulinSensitivityFactor // How many units does blood glucose drop for every unit of insulin administered.
        {
            get { return _insulinSensitivityFactor; }
            set
            {
                if (value > 0)
                    _insulinSensitivityFactor = value;
                else
                {
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NonPositiveInsulinSensitivityFactor,
                            _insulinSensitivityFactor));
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
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NonPositiveCarbToInsulinRatio,
                            _carbsToInsulinRation));
                }
            }
        }

        private int _longActingInsulinDoseRecommendation;

        public int
            LongActingInsulinDoesRecommendation // How many units of long acting insulin should be administered daily.
        {
            get { return _longActingInsulinDoseRecommendation; }
            set
            {
                if (value >= 0)
                    _longActingInsulinDoseRecommendation = value;
                else
                {
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NegativeLongActingInsulinDoesRecommendation,
                            _longActingInsulinDoseRecommendation));
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
                    _targetIsolationHours = value;
                else
                {
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NegativeTargetIsolationHours,
                            _targetIsolationHours));
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
                    _minIsolationHours = value;
                else
                {
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NegativeMinIsolationHours,
                            _minIsolationHours));
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
                    _maxIsolationHours = value;
                else
                {
                    throw new Exception(
                        message: string.Format(UserConfigurationErrorMessages.NegativeMaxIsolationHours,
                            _maxIsolationHours));
                }
            }
        }

        // Long acting insulin recalculation config variables
        public TimeSpan OvernightStartTime { get; set; } // Start time of an overnight period for the user.
        public TimeSpan OvernightEndTime { get; set; } // End time of an overnight period for the user.

        public double
            MinHoursOvernightWithoutAction
        {
            get;
            set;
        } // Minimum number of hours that count as a sufficient overnight period for analysis.

        public double
            MaxHoursOvernightWithoutAction
        {
            get;
            set;
        } // Maximum number of hours that count as a sufficient overnight period for analysis.

        // Default user configuration object constructor
        public UserConfiguration()
        {
            // User metrics
            InsulinSensitivityFactor = 1; // Blood glucose level drops by 1 unit for every unit of insulin.
            CarbToInsulinRatio = 10; // 1 unit of insulin for every 10g of carbs.
            LongActingInsulinDoesRecommendation = 32; // 32 units of long acting insulin daily.

            // Action free isolation period
            TargetIsolationHours =
                4; // Aim to disregard 4 hours of event readings to ensure all previous actions are no longer affecting the users blood glucose level whilst leaving sufficient remaining event time for analysis.
            MinIsolationHours =
                3.5; // Disregard at least the first 3.5 hours of events in the overnight period to ensure previous actions aren't still having an effect.
            MaxIsolationHours =
                4.5; // Disregard at most the first 4.5 hours of events in the overnight period to ensure that the period being analysed is long enough ToDo: Replace this with a check on check analysis length long enough

            // Long acting insulin recalculation config variables
            OvernightStartTime = new TimeSpan(20, 0, 0); // Overnight period for user starts at 8PM.
            OvernightEndTime = new TimeSpan(6, 0, 0); // Overnight period for user ends at 6AM.
            MinHoursOvernightWithoutAction =
                7.5; // Ensure an overnight period without actions taken of at least 7.5 hours to leave time for sufficient isolation and analysis.
            MaxHoursOvernightWithoutAction =
                8.5; // Ensure an overnight period without actions taken of  8.5 or less to try to stay within an overnight reading and avoid the dawn effect.
        }

        public void ValidateDataModelPropertyRelationships()
        {
            List<string> propertyRelationshipValidationErrors = new List<string>();

            // Target isolation hours less than min isolation hours
            if (TargetIsolationHours < MinIsolationHours)
            {
                propertyRelationshipValidationErrors.Add(item: string.Format(
                    UserConfigurationErrorMessages.TargetIsolationHoursNotLessThanMinIsolationHours,
                    MinIsolationHours,
                    TargetIsolationHours
                ));
            }

            // Target isolation hours greater than max isolation hours
            if (TargetIsolationHours > MaxIsolationHours)
            {
                propertyRelationshipValidationErrors.Add(item: string.Format(
                    UserConfigurationErrorMessages.MaxIsolationHoursNotLessThanTargetIsolationHours,
                    MaxIsolationHours,
                    TargetIsolationHours
                ));
            }

            // Max isolation hours less than min isolation hours
            if (MinIsolationHours > MaxIsolationHours)
            {
                propertyRelationshipValidationErrors.Add(item: string.Format(
                    UserConfigurationErrorMessages.MaxIsolationHoursNotLessThanMinIsolationHours,
                    MaxIsolationHours,
                    MinIsolationHours
                ));
            }

            // Max overnight hours without action less than min overnight hours without action
            if (MinHoursOvernightWithoutAction > MaxHoursOvernightWithoutAction)
            {
                propertyRelationshipValidationErrors.Add(item: string.Format(
                    UserConfigurationErrorMessages.MaxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutAction,
                    MaxHoursOvernightWithoutAction,
                    MinHoursOvernightWithoutAction
                ));
            }


            // Populate errors list
            if (propertyRelationshipValidationErrors.Count > 0)
            {
                foreach (string error in propertyRelationshipValidationErrors)
                {
                    Console.WriteLine(error);
                }

                throw new Exception(
                    message: string.Format(
                        GeneralErrorMessages.DataModelPropertyRelationshipErrors,
                        propertyRelationshipValidationErrors
                    ));
            }
        }
    }
}