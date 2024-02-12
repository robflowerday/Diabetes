namespace Diabetes
{
    public class ExceptionMessages
    {
        // Argument out of range exception message to append
        public static readonly string ArgumentOutOfRageMessageExtension = "\nParameter name: {0}";

        // Input parameter out of range exceptions
        public static readonly string InputParameterValueNegativeException = "Input parameter {0} cannot be negative, input value given: {1}";
        
        // Input parameter relationship exceptions
        public static readonly string InputParameterAEarlierThanInputParameterB = "Input parameter {0} cannot be earlier than input parameter {1}, input values given: {0}: {2}, {1}: {3}";
    }
}