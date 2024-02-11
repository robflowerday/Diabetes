using System;

using NUnit.Framework;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateCorrectionBolusTests
    {
        // Valid inputs
        
        [Test]
        [TestCase(16, 12, 2, 2)]
        [TestCase(15.2, 15.1, 10, 0.01)]
        [TestCase(13.3, 6.1, 3.3, 2.18181818181818181818)]
        public static void
            CalculateCorrectionBolus_ValidInputArgumentsTargetBGLessThanCurrentBG_ReturnsAppropriateResult(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivity,
                double expectedBolusDoseRecommendation)
        {
            double actualBolusDoseRecommendation = Diabetes.BolusCalculator.CalculateCorrectionBolus(
                currentBloodGlucose: currentBloodGlucose, targetBloodGlucose: targetBloodGlucose,
                insulinSensitivityFactor: insulinSensitivity);
            
            Assert.AreEqual(expected: expectedBolusDoseRecommendation, actual: actualBolusDoseRecommendation, delta: 0.0001);
        }
        
        [Test]
        [TestCase(8, 12, 2, -2)]
        [TestCase(15, 15.1, 10, -0.01)]
        [TestCase(6.1, 13.3, 3.3, -2.18181818181818181818)]
        public static void
            CalculateCorrectionBolus_ValidInputArgumentsTargetBGGreaterThanCurrentBG_ReturnsAppropriateResult(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivity,
                double expectedBolusDoseRecommendation)
        {
            double actualBolusDoseRecommendation = Diabetes.BolusCalculator.CalculateCorrectionBolus(
                currentBloodGlucose: currentBloodGlucose, targetBloodGlucose: targetBloodGlucose,
                insulinSensitivityFactor: insulinSensitivity);
            
            Assert.AreEqual(expected: expectedBolusDoseRecommendation, actual: actualBolusDoseRecommendation, delta: 0.0001);
        }
        
        [Test]
        [TestCase(12, 12, 0.2, 0)]
        [TestCase(4.1, 4.1, 98.999, 0)]
        [TestCase(6.1, 6.1, 6.3, 0)]
        public static void
            CalculateCorrectionBolus_ValidInputArgumentsTargetBGEqualToCurrentBG_ReturnsAppropriateResult(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivity,
                double expectedBolusDoseRecommendation)
        {
            double actualBolusDoseRecommendation = Diabetes.BolusCalculator.CalculateCorrectionBolus(
                currentBloodGlucose: currentBloodGlucose, targetBloodGlucose: targetBloodGlucose,
                insulinSensitivityFactor: insulinSensitivity);
            
            Assert.AreEqual(expected: expectedBolusDoseRecommendation, actual: actualBolusDoseRecommendation);
        }

        // Invalid current glucose input
        
        [Test]
        public static void
            CalculateCorrectionBolus_CurrentBloodGlucoseZeroValue_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage()
        {
            double currentBloodGlucose = 0;
            double targetBloodGlucose = 6.1;
            double insulinSensitivityFactor = 3.3;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(currentBloodGlucose)) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(currentBloodGlucose));
            
            Assert.AreEqual(expected: nameof(currentBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        [Test]
        [TestCase(-6.1, 6.1, 3.3)]
        [TestCase(-2, 6.1, 3.3)]
        [TestCase(-0.00001, 6.1, 3.3)]
        [TestCase(-100000, 6.1, 3.3)]
        public static void
            CalculateCorrectionBolus_CurrentBloodGlucoseNegativeValue_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivityFactor)
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(currentBloodGlucose), currentBloodGlucose) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(currentBloodGlucose));
            
            Assert.AreEqual(expected: nameof(currentBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        // Invalid target glucose input
        
        [Test]
        public static void
            CalculateCorrectionBolus_TargetBloodGlucoseZeroValue_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage()
        {
            double currentBloodGlucose = 6.1;
            double targetBloodGlucose = 0;
            double insulinSensitivityFactor = 3.3;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(targetBloodGlucose)) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(targetBloodGlucose));
            
            Assert.AreEqual(expected: nameof(targetBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        [Test]
        [TestCase(6.1, -6.1, 3.3)]
        [TestCase(6.1, -0.00001, 3.3)]
        [TestCase(6.1, -1000000000, 3.3)]
        [TestCase(6.1, -9, 3.3)]
        public static void
            CalculateCorrectionBolus_TargetBloodGlucoseNegativeValue_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivityFactor)
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(targetBloodGlucose), targetBloodGlucose) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(targetBloodGlucose));
            
            Assert.AreEqual(expected: nameof(targetBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        [Test]
        [TestCase(6.1, 0.1, 3.3)]
        [TestCase(6.1, 0.00001, 3.3)]
        [TestCase(6.1, 3.2, 3.3)]
        [TestCase(6.1, 4, 3.3)]
        public static void
            CalculateCorrectionBolus_TargetBloodGlucoseTooLow_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivityFactor)
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueLessThanOrEqualToException, nameof(targetBloodGlucose), targetBloodGlucose, 4) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(targetBloodGlucose));
            
            Assert.AreEqual(expected: nameof(targetBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        [Test]
        [TestCase(6.1, 16.00001, 3.3)]
        [TestCase(6.1, 17, 3.3)]
        [TestCase(6.1, 22.3, 3.3)]
        [TestCase(6.1, 10000000, 3.3)]
        public static void
            CalculateCorrectionBolus_TargetBloodGlucoseTooHigh_ThrowsArgumentOutOfRangeException_WithAppropriateErrorMessage(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivityFactor)
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueGreaterThanOrEqualToException, nameof(targetBloodGlucose), targetBloodGlucose, 16) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(targetBloodGlucose));
            
            Assert.AreEqual(expected: nameof(targetBloodGlucose), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
        
        // Invalid insulin sensitivity factor input

        [Test]
        public static void
            CalculateCorrectionBolus_ZeroInsulinSensitivityFactorInput_ThrowsArgumentOutOfRangeExceptionWithAppropriateErrorMessage()
        {
            double currentBloodGlucose = 6.1;
            double targetBloodGlucose = 6.1;
            double insulinSensitivityFactor = 0;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueZeroException, nameof(insulinSensitivityFactor)) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(insulinSensitivityFactor));
            
            Assert.AreEqual(expected: nameof(insulinSensitivityFactor), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }

        [Test]
        [TestCase(6.1, 6.1, -0.00001)]
        [TestCase(6.1, 6.1, -3.5)]
        [TestCase(6.1, 6.1, -1000000)]
        public static void
            CalculateCorrectionBolus_NegativeInsulinSensitivityFactorInput_ThrowsExceptionWithAppropriateErrorMessage(
                double currentBloodGlucose, double targetBloodGlucose, double insulinSensitivityFactor)
        {

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateCorrectionBolus(currentBloodGlucose: currentBloodGlucose,
                    targetBloodGlucose: targetBloodGlucose, insulinSensitivityFactor: insulinSensitivityFactor));

            string expectedExceptionMessage =
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(insulinSensitivityFactor), insulinSensitivityFactor) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(insulinSensitivityFactor));
            
            Assert.AreEqual(expected: nameof(insulinSensitivityFactor), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
    }
}