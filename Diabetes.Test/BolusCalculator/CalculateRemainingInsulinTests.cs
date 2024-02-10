using System;
using System.Reflection;
using NUnit.Framework;


namespace Diabetes.Test.BolusCalculator
{
    [TestFixture]
    public class CalculateRemainingInsulinTests
    {
        [Test]
        [TestCase(
            new DateTime(2024, 1, 1, 10, 0, 0),
            new DateTime(2024, 1, 1, 5, 0, 0),
            15,
            180)]
        public void CalculateRemainingInsulin_InsulinGivenPriorToActivePlusOnsetTime_ReturnsZero(
            DateTime timeAdministered, DateTime timeOfCalculation, int insulinOnsetTimeMinutes,
            int insulinActiveTimeMinutes)
        {
            double initialInsulinUnits = 16;

            double remainingInsulinUnits = Diabetes.BolusCalculator.CalculateRemainingInsulin(
                initialInsulinUnits: initialInsulinUnits, timeAdministered: timeAdministered,
                timeOfCalculation: timeOfCalculation, insulinOnsetTimeMinutes: insulinOnsetTimeMinutes,
                insulinActiveTimeMinutes: insulinActiveTimeMinutes);

            Assert.AreEqual(expected: 0, actual: remainingInsulinUnits);
        }
        
        [Test]
        public void CalculateRemainingInsulin_InsulinGivenOnActiveTimeBoundary_ReturnsZero()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_InsulinGivenStillDuringOnsetPeriod_ReturnsZero()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_InsulinGivenAtOnsetPeriodBoundary_ReturnsZero()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_InsulinGivenAtCalculationTime_ReturnsTotalDose()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_InsulinGivenInActivePeriod_ReturnsExponentiallyReducedAmount()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInitialInsulinUnits_ReturnsZero()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_NegativeInitialInsulinUnits_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInsulinOnsetTimeMinutes_CalculatesNormallyImmediatelyDecayingExponentially()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_NegativeInsulinOnsetTimeMinutes_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_NegativeInsulinActiveTimeMinutes_ThrowsArgumentOutOfRangeExceptionWithAppropriateMessage()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_ZeroInsulinActiveTimeMinutes_InsulinOnBoardGoesFromFullDoseToZeroImmediately()
        {
            
        }
        
        [Test]
        public void CalculateRemainingInsulin_TimeAdministeredLaterThenTimeOfCalculation_ThrowsArgumentExceptionWithAppropriateMessage()
        {
            
        }
    }
}