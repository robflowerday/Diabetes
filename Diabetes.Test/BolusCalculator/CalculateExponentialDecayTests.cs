using System;
using NUnit.Framework;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateExponentialDecayTests
    {
        /// <summary>
        /// P0 less than 0
        ///     Having, and therefore starting with a negative amount of
        ///     a substance does not make sense in reality. In reality,
        ///     a substance either exists, or does not, it does not exist
        ///     in reverse, in the sense that things like anti-matter could
        ///     be considered.
        /// </summary>
        /// <param name="P0"> Initial amount of substance during exponential decay. </param>
        /// <exception cref="NotImplementedException"></exception>
        [Test]
        [TestCase(-1)]
        [TestCase(-0.0000000000000000001)]
        [TestCase(-2147483647)]
        public void CalculateExponentialDecayTests_NegativeInitialValue_ThrowsArgumentOutOfRangeExceptionWithApproriateMessage(double P0)
        {
            double PT = 0.01; // Final amount of substance in exponential decay where the amount of substance is negligible.
            double T = 180; // Time at which the amount of substance in the exponential decay becomes its final negligible amount.

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateExponentialDecayRate(P0: P0, PT: PT, T: T));
            
            string expectedExceptionMessage = string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(P0), P0);
            
            Assert.AreEqual(expected: nameof(P0), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
    }
}