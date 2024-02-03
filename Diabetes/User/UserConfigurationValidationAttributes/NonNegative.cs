using System;
using System.ComponentModel.DataAnnotations;


namespace Diabetes.User.UserConfigurationValidationAttributes
{
    public class NonNegative : ValidationAttribute
    {
        private bool IsNumericType(object value)
        {
            return value is int || value is double || value is float || value is decimal || value is long ||
                   value is short || value is byte;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (IsNumericType(value: value))
                {
                    double numericTypeValue = Convert.ToDouble(value);
                    if (numericTypeValue < 0)
                        return new ValidationResult(validationContext.DisplayName + " cannot be negative.");
                }
                else
                {
                    return new ValidationResult(validationContext.DisplayName + " must be a numeric type.");
                }
            }
            return ValidationResult.Success;
        }
    }
}