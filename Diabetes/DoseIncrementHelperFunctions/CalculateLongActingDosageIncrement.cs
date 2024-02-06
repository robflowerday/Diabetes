using System;

namespace Diabetes.DoseIncrementHelperFunctions
{
    public static class CalculateLongActingDosageIncrement
    {
        public static int SimpleCalculation(
            double? initialBloodGlucoseReading, double? finalBloodGlucoseReading,
            int currentLongActingInsulinDoseRecommendation)
        {
            // If the initial or final blood glucose reading is null, make no change
            if (initialBloodGlucoseReading == null || finalBloodGlucoseReading == null)
            {
                Console.WriteLine($"Recalculation of long acting glucose recommendation not possible without both Initial bloog glucose reading and final blood glucose reading.");
                Console.WriteLine($"Initial blood glucose value given: {initialBloodGlucoseReading}.");
                Console.WriteLine($"Final blood glucose value given: {finalBloodGlucoseReading}.");
                Console.WriteLine($"Long acting insulin dose recommendation remains at: {currentLongActingInsulinDoseRecommendation}.");
                return currentLongActingInsulinDoseRecommendation;
            }
            
            // Calculate % change in blood glucose
            double? pctChangeInBloodGlucoseReading =
                (finalBloodGlucoseReading - initialBloodGlucoseReading) / initialBloodGlucoseReading;
            
            // If blood glucose rises by more than 10%, increase long acting insulin by 10% (rounded up)
            if (pctChangeInBloodGlucoseReading > 0.1)
            {
                int newDoseRecommendation = (int)Math.Round(currentLongActingInsulinDoseRecommendation * 1.1);
                Console.WriteLine($"Long term insulin daily dosage recommendation has increased to {newDoseRecommendation} units per day.");
                Console.WriteLine($"This is an increase of {newDoseRecommendation - currentLongActingInsulinDoseRecommendation} units from {currentLongActingInsulinDoseRecommendation} units per day.");
                return newDoseRecommendation;
            }
            
            // If blood glucose falls by more than 10%, decrease long acting insulin by 10% (rounded up)
            if (pctChangeInBloodGlucoseReading < 0.1)
            {
                int newDoseRecommendation = (int)Math.Round(currentLongActingInsulinDoseRecommendation * 0.9);
                Console.WriteLine($"Long term insulin daily dosage recommendation has decreased to {newDoseRecommendation} units per day.");
                Console.WriteLine($"This is an decrease of {currentLongActingInsulinDoseRecommendation - newDoseRecommendation} units from {currentLongActingInsulinDoseRecommendation} units per day.");
                return newDoseRecommendation;
            }
            
            // If there is less than 10% change in blood glucose readings, don't change the recommendation
            Console.WriteLine($"Recommended daily dose of long acting insulin remains the same at {currentLongActingInsulinDoseRecommendation} units per day.");
            return currentLongActingInsulinDoseRecommendation;
        }
    }
}