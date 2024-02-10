using System;

namespace Diabetes
{
    public static class BolusCalculator
    {
        /// <summary>
        /// Determines the units of insulin needed in order to cover the
        /// amount of carbohydrates eaten in a meal or snack.
        ///
        /// ArgumentException if a negative number of carbs are input as this
        /// does not make sense in reality.
        ///
        /// ArgumentException if a negative number or 0 is given for insulin
        /// to carb ratio as this does not make sense in reality.
        /// </summary>
        /// <param name="carbs"> Carbohydrates eaten e.g. 63 </param>
        /// <param name="icr"> Insulin to carb ratio e.g. 6.3 means that 1 unit
        ///                      of insulin couteracts 6.3 grams of carbs. </param>
        /// <returns> Recommended bolus dose for meal as double. </returns>
        public static double CalculateMealBolus(double carbs, double icr)
        {
            if (carbs < 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_NegativeCarbsInput, carbs));
            if (icr == 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_ZeroICRInput, icr));
            if (icr <= 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_NegativeICRInput, icr));
            return carbs / icr;
        }
    }
}