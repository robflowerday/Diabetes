namespace Diabetes.User
{
    public static class UserConfigurationErrorMessages
    {
        // Positive / Non-negative
        public static readonly string StrictlyPositive = "{1}: {0} must me greater than 0.";
        public static readonly string NonNegative = "{1}: {0} must be greater than or equal to 0.";

        public static readonly string NonPositiveInsulinSensitivityFactor = string.Format(StrictlyPositive, "{0}", "InsulinSensitivityFactor");
        public static readonly string NonPositiveCarbToInsulinRatio = string.Format(StrictlyPositive, "{0}", "CarbToInsulinRatio");
        
        public static readonly string NegativeLongActingInsulinDoesRecommendation = string.Format(NonNegative, "{0}", "LongActingInsulinDoesRecommendation");
        public static readonly string NegativeTargetIsolationHours = string.Format(NonNegative, "{0}", "TargetIsolationHours");
        public static readonly string NegativeMinIsolationHours = string.Format(NonNegative, "{0}", "MinIsolationHours");
        public static readonly string NegativeMaxIsolationHours = string.Format(NonNegative, "{0}", "MaxIsolationHours");
        
        // A not less than B
        public static readonly string ANotLessThanB = "{2}: {0} cannot be less than {3}: {1}.";

        public static readonly string MaxIsolationHoursNotLessThanTargetIsolationHours =
            string.Format(ANotLessThanB, "{0}", "{1}", "MaxIsolationHours", "TargetIsolationHours");

        public static readonly string TargetIsolationHoursNotLessThanMinIsolationHours =
            string.Format(ANotLessThanB, "{0}", "{1}", "TargetIsolationHours", "MinIsolationHours");
        
        public static readonly string MaxIsolationHoursNotLessThanMinIsolationHours =
            string.Format(ANotLessThanB, "{0}", "{1}", "MaxIsolationHours", "MinIsolationHours");
        
        public static readonly string MaxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutAction =
            string.Format(ANotLessThanB, "{0}", "{1}", "MaxHoursOvernightWithoutAction", "MinHoursOvernightWithoutAction");
    }
}