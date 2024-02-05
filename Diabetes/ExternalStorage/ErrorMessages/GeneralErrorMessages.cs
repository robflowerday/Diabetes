namespace Diabetes.ExternalStorage.ErrorMessages
{
    public static class GeneralErrorMessages
    {
        // Multiple user configuration property relationship validation errors
        public static readonly string DataModelPropertyRelationshipErrors = "One or more data model property relationship validation errors exist:\n{0}";
        
        // Positive / Non-negative
        public static readonly string StrictlyPositive = "{1}: {0} must me greater than 0.";
        public static readonly string NonNegative = "{1}: {0} must be greater than or equal to 0.";
        
        // A not less than B
        public static readonly string ANotLessThanB = "{2}: {0} cannot be less than {3}: {1}.";
    }
}