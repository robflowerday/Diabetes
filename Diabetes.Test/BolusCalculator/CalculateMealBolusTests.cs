using System;

using NUnit.Framework;

using Diabetes.BolusCalculator;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateMealBolusTests
    {
        [Test]
        [TestCase(10, 1, 10)]
        [TestCase(15.6, 5.2, 3)]
        [TestCase(23.4, 0.5, 46.8)]
        public static void CalculateMealBolus_ValidInputValues_ReturnsMealBolus(
            double inputCarbs, double inputICR, double expectedMealBolusValue)
        {
            double actualMealBolusValue = CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR);
            Assert.AreEqual(expectedMealBolusValue, actualMealBolusValue);
        }

        [Test]
        [TestCase(0, 5, 0)]
        [TestCase(0.0, 5.5, 0.0)]
        [TestCase(0.0, 3, 0)]
        public static void CalculateMealBolus_ZeroCarbsInput_ReturnsZero(
            double inputCarbs, double inputICR, double expectedMealBolusValue)
        {
            double actualMealBolusValue = CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR);
            Assert.AreEqual(expectedMealBolusValue, actualMealBolusValue);
        }

        [Test]
        [TestCase(-3, 2)]
        [TestCaseAttribute(-3.20, 4.4)]
        public static void CalculateMealBolus_NegativeCarbsInput_ThrowsArgumentExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentException>(code: () =>
                CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.CalculateMealBolus_NegativeCarbs, inputCarbs);
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        [TestCase(13, 0)]
        [TestCase(23.4, 0.0)]
        public static void CalculateMealBolus_ZeroICRInput_ThrowsArgumentExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentException>(code: () =>
                CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = string.Format(ExceptionMessages.CalculateMealBolus_ZeroICR, inputICR);
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        [TestCase(24, -2)]
        [TestCase(23.4, -2.3)]
        public static void CalculateMealBolus_NegativeICRInput_ThrowsArgumentExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentException>(code: () =>
                CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = string.Format(ExceptionMessages.CalculateMealBolus_NegativeICR, inputICR);
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        public static void CalculateMealBolus_nullCarbsInput_ThrowsExceptionWithAppropriateErrorMessage()
        {
            double inputCarbs = null;
            double inputICR = 2;

            var exception = Assert.Throws<ArgumentNullException>(code: () =>
                CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = ExceptionMessages.CalculateMealBolus_nullCarbsInput;
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        public static void CalculateMealBolus_nullICRInput_ThrowsExceptionWithAppropriateErrorMessage()
        {
            double inputCarbs = 25;
            double inputICR = null;

            var exception = Assert.Throws<ArgumentNullException>(code: () =>
                CalculateBolus.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = ExceptionMessages.CalculateMealBolus_nullICRInput;
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }
    }
}