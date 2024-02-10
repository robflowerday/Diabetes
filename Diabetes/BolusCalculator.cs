using System;
namespace Diabetes
{
    public static class BolusCalculator
    {
        /// <summary>
        /// Takes in your insulin sensitivity factor and returns the no. units of inulin needed to
        /// bring you to your target blood glucose level, excluding existing carbs and insulin in
        /// your system, carbs to be eaten in the future and other factors like exercise, time of
        /// day, stress levels and illness.
        ///
        /// Should allow a result that is positive, negative or zero.
        /// 
        /// Should error if current glucose, target glucose or insulin sensitivity factor is less
        /// than or equal to zero.
        /// </summary>
        /// <param name="currentBloodGlucose"> Current Blood glucose level. </param>
        /// <param name="targetBloodGlucose"> Blood glucose level user aims to be after bolus dose
        ///                                     has finished acting. </param>
        /// <param name="insulinSensitivityFactor"> The number of units measuring your blood
        ///                             glucose level that drop due to 1 unit of insulin. </param>
        /// <returns> The number of units required to bring your glucose level to your intended
        ///             level, based only on your insulin sensitivity factor. </returns>
        /// <exception cref="NotImplementedException"></exception>
        public static double CalculateCorrectionBolus(double currentBloodGlucose, double targetBloodGlucose,
            double insulinSensitivityFactor)
        {
            // Current blood glucose input exceptions
            if (currentBloodGlucose == 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(currentBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(currentBloodGlucose)));
            if (currentBloodGlucose < 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(currentBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(currentBloodGlucose), currentBloodGlucose));
            
            // Target blood glucose input exceptions
            if (targetBloodGlucose == 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(targetBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(targetBloodGlucose)));
            if (targetBloodGlucose < 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(targetBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(targetBloodGlucose), targetBloodGlucose));
            if (targetBloodGlucose > 0 && targetBloodGlucose <= 4)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(targetBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueLessThanOrEqualToException, nameof(targetBloodGlucose), targetBloodGlucose, 4));
            if (targetBloodGlucose >= 16)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(targetBloodGlucose),
                    message: string.Format(ExceptionMessages.InputParameterValueGreaterThanOrEqualToException, nameof(targetBloodGlucose), targetBloodGlucose, 16));
            
            // Insulin sensitivity factor input exceptions
            if (insulinSensitivityFactor == 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(insulinSensitivityFactor),
                    message: string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(insulinSensitivityFactor)));
            if (insulinSensitivityFactor < 0)
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(insulinSensitivityFactor),
                    message: string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(insulinSensitivityFactor), insulinSensitivityFactor));
            
            return (currentBloodGlucose - targetBloodGlucose) / insulinSensitivityFactor;
        }
    }
}