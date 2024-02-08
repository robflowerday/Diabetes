namespace Diabetes
{
    public static class ExceptionMessages
    {
        // CalculateMealBolus
        public static readonly string CalculateMealBolus_NegativeCarbs =
            "Input carbohydrate value to CalculateMealBolus cannot be negative as negative carbohydrates cannot be consumed." +
            "Carbohydrate value input: {0}.";

        public static readonly string CalculateMealBolus_ZeroICR =
            "Input insulin to carbohydrate value to CalculateMealBolus cannot be 0 as this suggests carbohydrates have no affect on blood sugar." +
            "Insulin to carbohydrate value input: {0}.";

        public static readonly string CalculateMealBolus_NegativeICR =
            "Input insulin to carbohydrate value to CalculateMealBolus cannot be negative as this suggests consuming carbohydrates will bring blood sugar levels down." +
            "Insulin to carbohydrate value input: {0}.";
    }
}