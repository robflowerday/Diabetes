using System;
using System.Reflection;
using NUnit.Framework;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateRemainingInsulinTests
    {
        [Test]
        [TestCase("2024-01-01 05:00:00.000000", "2024-01-01 10:00:00.000000", 15, 180)]
        [TestCase("2024-01-01 18:00:00.000000", "2024-01-01 23:00:00.000000", 15, 180)]
        [TestCase("2024-01-01 18:30:00.000000", "2024-01-01 23:00:00.000000", 15, 180)]
        [TestCase("2024-01-01 18:44:59.000000", "2024-01-01 23:00:00.000000", 15, 180)]
        [TestCase("2024-01-01 22:57:59.999999", "2024-01-01 23:00:00.000000", 1, 1)]
        [TestCase("2024-01-01 21:44:59.999999", "2024-01-02 01:00:00.000000", 15, 180)]
        public void CalculateRemainingInsulin_InsulinGivenPriorToActivePlusOnsetTime_ReturnsZero(
            string timeAdministeredString, string timeOfCalculationString, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeOfCalculationString);

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: 0, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase("2024-01-01 05:00:00.000000")]
        [TestCase("2024-01-01 18:00:00.000000")]
        [TestCase("2024-01-01 18:30:00.000000")]
        [TestCase("2024-01-01 18:44:59.000000")]
        [TestCase("2024-01-01 22:57:59.999999")]
        [TestCase("2024-01-01 21:44:59.999999")]
        public void CalculateRemainingInsulin_TimeOfAdministrationEqualsTimeOfCalculation_ReturnsFullDose(
            string timeAdministeredAndCalculationString)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredAndCalculationString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeAdministeredAndCalculationString);
            int insulinOnsetTimeMinutes = 15;
            int insulinActiveTimeMinutes = 180;

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: initialInsulinUnits, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase("2024-01-01 23:30:00.000000", "2024-01-02 01:30:00.000000", 30, 30)]
        [TestCase("2024-01-02 06:12:23.123456", "2024-01-02 07:12:23.123456", 15, 45)]
        [TestCase("2024-01-02 10:30:00.000000", "2024-01-02 11:30:00.000000", 45, 15)]
        public void CalculateRemainingInsulin_InsulinGivenOnActiveTimeBoundary_ReturnsZero(
            string timeAdministeredString, string timeOfCalculationString, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeOfCalculationString);

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: 0, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase("2024-01-01 23:31:00.000000", "2024-01-02 01:30:00.000000", 30, 30)]
        [TestCase("2024-01-01 23:59:00.000000", "2024-01-02 01:30:00.000000", 30, 30)]
        [TestCase("2024-01-02 01:28:30.000000", "2024-01-02 01:30:00.000000", 1, 1)]
        public void CalculateRemainingInsulin_InsulinGivenStillDuringOnsetPeriod_ReturnsZero(
            string timeAdministeredString, string timeOfCalculationString, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeOfCalculationString);

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: initialInsulinUnits, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase("2024-01-01 23:30:00.000000", "2024-01-02 00:30:00.000000", 30, 30)]
        [TestCase("2024-01-02 00:28:00.000000", "2024-01-02 00:30:00.000000", 1, 1)]
        [TestCase("2024-01-02 00:26:00.000000", "2024-01-02 00:30:00.000000", 3, 1)]
        public void CalculateRemainingInsulin_InsulinGivenAtOnsetPeriodEarlyBoundary_ReturnsZero(
            string timeAdministeredString, string timeOfCalculationString, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeOfCalculationString);

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: initialInsulinUnits, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase("2024-01-02 01:29:00.000000", "2024-01-02 01:30:00.000000", 1, 1)]
        [TestCase("2024-01-02 01:00:00.000000", "2024-01-02 01:30:00.000000", 1, 30)]
        [TestCase("2024-01-01 23:59:00.000000", "2024-01-02 01:01:00.000000", 1, 1)]
        [TestCase("2024-01-01 23:31:00.000000", "2024-01-02 01:01:00.000000", 1, 30)]
        [TestCase("2024-01-01 23:31:00.000000", "2024-01-02 01:01:00.000000", 1, 30)]
        public void CalculateRemainingInsulin_InsulinGivenAtCalculationTime_ReturnsTotalDose(
            string timeAdministeredString, string timeOfCalculationString, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministeredDateTime = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculationDateTime = DateTime.Parse(timeOfCalculationString);

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: initialInsulinUnits, actual: remainingInsulinUnits);
        }

        [Test]
        [TestCase(16, "2024-01-01 10:00:00.000000", "2024-01-01 06:00:00.000000", 15, 240)]
        public void CalculateRemainingInsulin_InsulinGivenInActivePeriod_ReturnsExponentiallyReducedAmount(
            double initialInsulinUnits, string timeAdministeredString, string timeOfCalculationString,
            int insulinOnsetTimeMinutes, int insulinActiveTimeMinutes, double expectedRemainingInsulinValue)
        {
            DateTime timeAdministered = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculation = DateTime.Parse(timeOfCalculationString);

            double remainingInsulin = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministered,
                timeOfCalculation: timeOfCalculation, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);
            
            Assert.AreEqual(expected: expectedRemainingInsulinValue, actual: remainingInsulin);
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInitialInsulinUnits_ReturnsZero()
        {
            double initialInsulinUnits = 0;
            DateTime timeAdministeredDateTime = new DateTime(2024, 1, 1);
            DateTime timeOfCalculationDateTime = new DateTime(2024, 1, 1);
            int insulinOnsetTimeMinutes = 15;
            int insulinActiveTimeMinutes = 180;

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministeredDateTime,
                timeOfCalculation: timeOfCalculationDateTime, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: 0, actual: remainingInsulinUnits);
        }
        
        [Test]
        [TestCase(-3)]
        [TestCase(-10.5)]
        [TestCase(-0.00001)]
        [TestCase(-12734827364)]
        public void CalculateRemainingInsulin_NegativeInitialInsulinUnits_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage(double initialInsulinUnits)
        {
            DateTime timeAdministered = new DateTime(2024, 1, 1, 10, 0, 0);
            DateTime timeOfCalculation = new DateTime(2024, 1, 1, 11, 0, 0);
            int insulinOnsetTimeMinutes = 15;
            int insulinActiveTimeMinutes = 180;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateRemainingInsulin(initialInsulinUnits: initialInsulinUnits,
                    timeAdministered: timeAdministered, timeOfCalculation: timeOfCalculation,
                    insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                    insulinActiveTimeMinutes: insulinActiveTimeMinutes));

            string expectedExceptionMessage = 
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(initialInsulinUnits), initialInsulinUnits) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(initialInsulinUnits));
            
            Assert.AreEqual(expected: nameof(initialInsulinUnits), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInsulinOnsetTimeMinutes_CalculatesNormallyImmediatelyDecayingExponentially()
        {
            
        }
        
        [Test]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-2147483648)]
        public void CalculateRemainingInsulin_NegativeInsulinOnsetTimeMinutes_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage(int insulinOnsetTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministered = new DateTime(2024, 1, 1, 10, 0, 0);
            DateTime timeOfCalculation = new DateTime(2024, 1, 1, 11, 0, 0);
            int insulinActiveTimeMinutes = 180;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateRemainingInsulin(initialInsulinUnits: initialInsulinUnits,
                    timeAdministered: timeAdministered, timeOfCalculation: timeOfCalculation,
                    insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                    insulinActiveTimeMinutes: insulinActiveTimeMinutes));

            string expectedExceptionMessage = 
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(insulinOnsetTimeMinutes), initialInsulinUnits) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(insulinOnsetTimeMinutes));
            
            Assert.AreEqual(expected: nameof(insulinOnsetTimeMinutes), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
        
        [Test]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-2147483648)]
        public void CalculateRemainingInsulin_NegativeInsulinActiveTimeMinutes_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage(int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;
            DateTime timeAdministered = new DateTime(2024, 1, 1, 10, 0, 0);
            DateTime timeOfCalculation = new DateTime(2024, 1, 1, 11, 0, 0);
            int insulinOnsetTimeMinutes = 180;

            var exception = Assert.Throws<ArgumentOutOfRangeException>(code: () =>
                Diabetes.BolusCalculator.CalculateRemainingInsulin(initialInsulinUnits: initialInsulinUnits,
                    timeAdministered: timeAdministered, timeOfCalculation: timeOfCalculation,
                    insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                    insulinActiveTimeMinutes: insulinActiveTimeMinutes));

            string expectedExceptionMessage = 
                string.Format(ExceptionMessages.InputParameterValueNegativeException, nameof(insulinActiveTimeMinutes), initialInsulinUnits) +
                string.Format(ExceptionMessages.ArgumentOutOfRageMessageExtension, nameof(insulinActiveTimeMinutes));
            
            Assert.AreEqual(expected: nameof(insulinActiveTimeMinutes), actual: exception.ParamName);
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInsulinActiveTimeMinutes_InsulinOnBoardGoesFromFullDoseToZeroImmediately()
        {
            
        }
        
        [Test]
        [TestCase("2024-01-01 10:00:00.000000", "2024-01-01 09:59:59.999999")]
        [TestCase("2024-01-01 10:00:00.000000", "2024-01-01 09:59:59.999999")]
        public void CalculateRemainingInsulin_TimeAdministeredLaterThenTimeOfCalculation_ThrowsArgumentExceptionWithAppropriateMessage(string timeAdministeredString, string timeOfCalculationString)
        {
            DateTime timeAdministered = DateTime.Parse(timeAdministeredString);
            DateTime timeOfCalculation = DateTime.Parse(timeOfCalculationString);
            
            double initialInsulinUnits = 16;
            int insulinOnsetTimeMinutes = 15;
            int insulinActiveTimeMinutes = 180;

            var exception = Assert.Throws<ArgumentException>(code: () =>
                Diabetes.BolusCalculator.CalculateRemainingInsulin(initialInsulinUnits: initialInsulinUnits,
                    timeAdministered: timeAdministered, timeOfCalculation: timeOfCalculation,
                    insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                    insulinActiveTimeMinutes: insulinActiveTimeMinutes));

            string expectedExceptionMessage = string.Format(ExceptionMessages.InputParameterAEarlierThanInputParameterB,
                nameof(timeOfCalculation), nameof(timeAdministered), timeOfCalculation, timeAdministered);
            
            Assert.AreEqual(expected: expectedExceptionMessage, actual: exception.Message);
        }
    }
}