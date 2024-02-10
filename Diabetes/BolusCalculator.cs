using System;
using System.Collections.Generic;


namespace Diabetes
{
    public static class BolusCalculator
    {
        /// <summary>
        /// Takes the amount of insulin given at a given time in the past and returns
        /// the amount of insulin remaining in the body at the time of calculation.
        ///
        /// The calculation is based on the insulin action curve. This function assumes
        /// the insulin action curve follows an exponential decay model with an insulin
        /// onset period.
        /// </summary>
        /// <param name="initialInsulinUnits"></param>
        /// <param name="timeAdministered"></param>
        public static double CalculateRemainingInsulin(double initialInsulinUnits, DateTime timeAdministered, DateTime timeOfCalculation,
            int insulinOnsetTimeMinutes, int insulinActiveTimeMinutes)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        ///  Determines the number of effective insulin units remaining in the users
        /// system due to previous bolus injections.
        ///
        /// Takes the amount of time taken after a dose of bolus insulin until there
        /// is an insignificant concentration of the insulin remaining in the users
        /// system.
        ///
        /// Finds the amount of insulin injected and the time of injection for all
        /// bolus injection events within the above time frame.
        ///
        /// Calculates the proportion of and therefore the amount of insulin remaining
        /// in the users body from each of these injections and sums the results.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public double CalculateInsulinOnboard(BolusEvent bolusEvents, int insulinTotalActiveTimeMinutes, int insulinOnsetTimeMinutes)
        {
            List<BolusEvent> activeBolusEvents = GetActiveBolusEvents(inputBolusEvents: bolusEvents, insulinTotalActiveTimeMinutes: insulinOnsetTimeMinutes);

            double insulinObBoard = 0;
            foreach (BolusEvent bolusEvent in activeBolusEvents)
                insulinObBoard += CalculateRemainingInsulin(initialInsulinUnits: bolusEvent.Units,
                    timeAdministered: bolusEvent.TimeAdministered);

            return insulinObBoard;
        }
    }
}