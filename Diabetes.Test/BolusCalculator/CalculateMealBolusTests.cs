using System;

using NUnit.Framework;

using Diabetes;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateMealBolusTests
    {
        // Valid inputs
        
        [Test]
        [TestCase(10, 1, 10)]
        [TestCase(15.6, 5.2, 3)]
        [TestCase(23.4, 0.5, 46.8)]
        public static void CalculateMealBolus_ValidInputValues_ReturnsMealBolus(
            double inputCarbs, double inputICR, double expectedMealBolusValue)
        {
            double actualMealBolusValue = Diabetes.BolusCalculator.CalculateMealBolus(carbs: inputCarbs, icr: inputICR);
            Assert.AreEqual(expectedMealBolusValue, actualMealBolusValue);
        }
        
        [Test]
        [TestCase(0, 5, 0)]
        [TestCase(0.0, 5.5, 0.0)]
        [TestCase(0.0, 3, 0)]
        public static void CalculateMealBolus_ZeroCarbsInput_ReturnsZero(
            double inputCarbs, double inputICR, double expectedMealBolusValue)
        {
            double actualMealBolusValue = Diabetes.BolusCalculator.CalculateMealBolus(carbs: inputCarbs, icr: inputICR);
            Assert.AreEqual(expectedMealBolusValue, actualMealBolusValue);
        }

        // Carb input exceptions
        
        [Test]
        [TestCase(-3, 2)]
        [TestCaseAttribute(-3.20, 4.4)]
        public static void CalculateMealBolus_NegativeCarbsInput_ThrowsArgumentOutOfRangeExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.CalculateMealBolus_NegativeCarbsInput, inputCarbs);
            
            Assert.AreEqual();
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        // Insulin to carb ratio exceptions
        
        [Test]
        [TestCase(13, 0)]
        [TestCase(23.4, 0.0)]
        public static void CalculateMealBolus_ZeroICRInput_ThrowsArgumentExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentException>(code: () =>
                Diabetes.BolusCalculator.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = string.Format(ExceptionMessages.CalculateMealBolus_ZeroICRInput, inputICR);
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        [TestCase(24, -2)]
        [TestCase(23.4, -2.3)]
        public static void CalculateMealBolus_NegativeICRInput_ThrowsArgumentExceptionWithAppropriateErrorMessage(
            double inputCarbs, double inputICR)
        {
            var exception = Assert.Throws<ArgumentException>(code: () =>
                Diabetes.BolusCalculator.CalculateMealBolus(carbs: inputCarbs, icr: inputICR));

            string expectedExceptionMessage = string.Format(ExceptionMessages.CalculateMealBolus_NegativeICRInput, inputICR);
            
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }
    }
}