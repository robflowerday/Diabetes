namespace Diabetes
{
    public static class ExceptionMessages
    {
        // Argument out of range exception message to append
        public static readonly string ArgumentOutOfRageMessageExtension = "\nParameter name: {0}";

        // Input parameter out of range exceptions
        public static readonly string InputParameterValueNegativeException = "Input parameter {0} cannot be negative, input value given: {1}";
        public static readonly string InputParameterValueZeroException = "Input parameter {0} cannot be zero.";
        public static readonly string InputParameterValueLessThanException = "Input parameter {0} cannot be less than {2}, input value given {1}";
        public static readonly string InputParameterValueLessThanOrEqualToException = "Input parameter {0} cannot be less than or equal to {2}, input value given {1}";
        public static readonly string InputParameterValueGreaterThanException = "Input parameter {0} cannot be greater than {2}, input value given {1}";
        public static readonly string InputParameterValueGreaterThanOrEqualToException = "Input parameter {0} cannot be greater than or equal to {2}, input value given {1}";
    }
}