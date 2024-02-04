namespace Diabetes.User
{
    public static class UserConfigurationErrorMessages
    {
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