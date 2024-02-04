namespace Diabetes.ExternalStorage.ErrorMessages
{
    public static class UserConfigurationErrorMessages
    {
        // Positive / Non-negative
        public static readonly string NonPositiveInsulinSensitivityFactor = string.Format(GeneralErrorMessages.StrictlyPositive, "{0}", "InsulinSensitivityFactor");
        public static readonly string NonPositiveCarbToInsulinRatio = string.Format(GeneralErrorMessages.StrictlyPositive, "{0}", "CarbToInsulinRatio");
        
        public static readonly string NegativeLongActingInsulinDoesRecommendation = string.Format(GeneralErrorMessages.NonNegative, "{0}", "LongActingInsulinDoesRecommendation");
        public static readonly string NegativeTargetIsolationHours = string.Format(GeneralErrorMessages.NonNegative, "{0}", "TargetIsolationHours");
        public static readonly string NegativeMinIsolationHours = string.Format(GeneralErrorMessages.NonNegative, "{0}", "MinIsolationHours");
        public static readonly string NegativeMaxIsolationHours = string.Format(GeneralErrorMessages.NonNegative, "{0}", "MaxIsolationHours");
        
        // A not less than B
        public static readonly string MaxIsolationHoursNotLessThanTargetIsolationHours =
            string.Format(GeneralErrorMessages.ANotLessThanB, "{0}", "{1}", "MaxIsolationHours", "TargetIsolationHours");

        public static readonly string TargetIsolationHoursNotLessThanMinIsolationHours =
            string.Format(GeneralErrorMessages.ANotLessThanB, "{0}", "{1}", "TargetIsolationHours", "MinIsolationHours");
        
        public static readonly string MaxIsolationHoursNotLessThanMinIsolationHours =
            string.Format(GeneralErrorMessages.ANotLessThanB, "{0}", "{1}", "MaxIsolationHours", "MinIsolationHours");
        
        public static readonly string MaxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutAction =
            string.Format(GeneralErrorMessages.ANotLessThanB, "{0}", "{1}", "MaxHoursOvernightWithoutAction", "MinHoursOvernightWithoutAction");
    }
}