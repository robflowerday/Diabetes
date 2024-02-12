using System;
using System.Collections.Generic;


namespace Diabetes
{
    public static class BolusCalculator
    {
        /// <summary>
        /// Calculate the rate of exponential decay given insulin concentration
        /// becomes insignificant (0.01 units) after T minutes.
        /// 
        /// Calculate the rate of decay in an exponential decay model.
        ///
        /// Within this application, the use case of an exponential decay
        /// model is to model the amount of insulin remaining in the system
        /// after a certain time.
        /// 
        ///
        /// P(T) = P0e^(-rT)    =>
        /// P(T)/P0 = e^(-rT)   =>
        /// ln(P(T)/P0) = -rT   =>
        /// -ln(P(T)/P0)/T = r
        /// 
        /// </summary>
        /// <param name="P0"> Initial amount of a substance.
        ///
        /// P0 less than 0
        ///     Having, and therefore starting with a negative amount of
        ///     a substance does not make sense in reality. In reality,
        ///     a substance either exists, or does not, it does not exist
        ///     in reverse, in the sense that things like anti-matter could
        ///     be considered.
        ///
        /// P0 == 0
        ///     Whilst a substance could not be present, the only scenario
        ///     in which this is valid for exponential decay is that it
        ///     decays from an amount of 0 to itself, meaning no decay
        ///     occurs. Since no decay occurs, the function is not reliant
        ///     on a decay factor and so r has infinite solutions.
        ///
        /// 0 less than P0 less than PT
        ///     This situation implies and would lead to exponential growth.
        ///     Whilst this is possible, it should be handled by a different
        ///     function.
        ///
        /// P0 == PT
        ///     This is another scenario where no decay occurs and so r
        ///     has infinite solutions.
        ///
        /// P0 > PT
        ///     Valid
        ///     This situation implies that there is a positive starting value
        ///     for the amount of the substance and that some decay occurs.
        ///  </param>
        ///
        /// 
        /// <param name="PT"> Final negligible amount of remaining substance.
        ///
        /// PT less than 0
        ///     A substance can either exist or not exist, having an amount
        ///     of 0 or a positive amount, having a negative amount does not
        ///     apply.
        ///
        /// PT == 0
        ///     A substance cannot end with a value of 0 if it did not start
        ///     with a value of 0 in exponential decay as each decrement in
        ///     value is by a non-zero fraction of the decrements starting
        ///     value that is strictly less than the value itself.
        ///     This is represented in the exponential decay function as
        ///     an asymptotic limit of 0 where as T gets large, PT gets
        ///     small enough that it becomes negligible, but not equal to
        ///     zero.
        ///
        /// 0 less than PT less than P0
        ///     Valid
        ///     Any final value greater than 0 but less than the initial
        ///     value indicates that some positive amount of decay has
        ///     occured and the amount of a substance remains a positive
        ///     non-zero value.
        /// PT == P0
        ///     This implies that no decay has occured, whilst this situation
        ///     can occur when T = 0, the decay function becomes ireelevant
        ///     to the exponential function and so infinite solutions are
        ///     possible. This case should therefore be handled in another
        ///     function.
        ///
        /// PT > P0
        ///     This would imply exponential growth rather than decay and
        ///     should be handled by a similar, but different function.
        /// </param>
        ///
        /// 
        /// <param name="T"> Total time taken for value to decay from the initial value to the final value.
        ///
        /// T == 0
        ///     If no time passes, no decaying can occur and so the starting
        ///     and ending amount of the substance must remain the same.
        ///     Because no decaying has occured, the value of the decay rate
        ///     r has no bearing on the exponential decay scenario. This
        ///     leads to r satisfying the exponential decay equation with
        ///     infinite solutions as it can be anything.
        ///     Whilst this situation should be handled in external functions,
        ///     this situation should not be passed to this function and this
        ///     function should error with T = 0.
        ///
        /// T less than 0
        ///     A negative amount of time cannot pass, whilst this function
        ///     would not break, it would lead to a reverse exponential
        ///     decay function, leading to exponential growth. If this is
        ///     the desired functionality, another function should be written.
        ///     This new function and this existing function could optionally
        ///     share functionality.
        ///
        /// T greater than 0
        ///     Any T greater than 0 is valid.
        /// </param>
        /// <returns></returns>
        public static double CalculateExponentialDecayRate(double P0, double PT, double T)
        {
            if (P0 < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(P0), message: "");
            return -Math.Log(PT / P0) / T;
        }

        public static double ExponentialDecayFunction(double P0, double r, double t)
        {
            return P0 * Math.Exp(-r * t);
        }
        
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
            double T = insulinActiveTimeMinutes;
            double P0 = initialInsulinUnits;
            double PT = 0.01;
            // T = insulinActiveTimeMinutes
            // P0 = initialInsulinUnits
            // P(T) = 0.01 // The point at which there is an insignificant amount of insulin left in the body. If zero is used, the exponential decay will never end.

            double r = CalculateExponentialDecayRate(P0: P0, PT: PT, T: T); // Decay rate
            // P(T) = P0e^(-rT)
            // P(T)/P0 = e^(-rT)
            // ln(P(T)/P0) = -rT
            // r = -(ln(P(T)/P0))/T

            double minutesSinceAdministration = (timeOfCalculation - timeAdministered).TotalMinutes;
            double minutesDuringActionPeriod = minutesSinceAdministration - insulinOnsetTimeMinutes;
            
            // if (t < 0): error
            // if (t == 0): initialInsulinUnits
            // if (0 < t && t < insulinOnsetTimeMinutes): initialInsulinUnits
            // if (t == insulinOnsetTimeMinutes): initialInsulinUnits

            return ExponentialDecayFunction(P0: initialInsulinUnits, r: r, t: minutesDuringActionPeriod);
            // if (insulinOnsetTime < t && t < insulinOnsetTime + insulinActiveTime): P(t) = P0e^(-rt)

            // if (t == insulinOnsetTime + insulinActiveTime): 0
            // if (insulinOnsetTime + insulinActiveTime < t): 0
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
        // public double CalculateInsulinOnboard(BolusEvent bolusEvents, int insulinTotalActiveTimeMinutes, int insulinOnsetTimeMinutes)
        // {
        //     List<BolusEvent> activeBolusEvents = GetActiveBolusEvents(inputBolusEvents: bolusEvents, insulinTotalActiveTimeMinutes: insulinOnsetTimeMinutes);
        //
        //     double insulinObBoard = 0;
        //     foreach (BolusEvent bolusEvent in activeBolusEvents)
        //         insulinObBoard += CalculateRemainingInsulin(initialInsulinUnits: bolusEvent.Units,
        //             timeAdministered: bolusEvent.TimeAdministered);
        //
        //     return insulinObBoard;
        // }
    }
}