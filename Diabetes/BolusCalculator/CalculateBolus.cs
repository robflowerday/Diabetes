using System;


namespace Diabetes.BolusCalculator
{
    public static class CalculateBolus
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
        ///
        /// ArgumentNullException if either carbs or icr is input as null.
        /// </summary>
        /// <param name="carbs"> Carbohydrates eaten e.g. 63 </param>
        /// <param name="icr"> Insulin to carb ratio e.g. 6.3 means that 1 unit
        ///                      of insulin couteracts 6.3 grams of carbs. </param>
        /// <returns> Recommended bolus dose for meal as double. </returns>
        public static double CalculateMealBolus(double carbs, double icr)
        {
            if (carbs == null)
                throw new ArgumentNullException(ExceptionMessages.CalculateMealBolus_nullCarbsInput);
            if (icr == null)
                throw new ArgumentNullException(ExceptionMessages.CalculateMealBolus_nullICRInput);
            if (carbs < 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_NegativeCarbs, carbs));
            if (icr == 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_ZeroICR, icr));
            if (icr <= 0)
                throw new ArgumentException(
                    message: string.Format(ExceptionMessages.CalculateMealBolus_NegativeICR, icr));
            return carbs / icr;
        }
    }
}