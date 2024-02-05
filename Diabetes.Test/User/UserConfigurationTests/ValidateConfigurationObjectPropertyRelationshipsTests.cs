using System;
using System.Collections.Generic;

using NUnit.Framework;

using Diabetes.User;


namespace Diabetes.Test.User.UserConfigurationTests
{
    [TestFixture]
    public class ValidateConfigurationObjectPropertyRelationshipsTests
    {
        [Test]
        public void ValidateConstructor_DefaultObjectCreation_DoesntThrowError()
        {
            UserConfiguration userConfiguration = new UserConfiguration();
        }

        [Test]
        public void ValidateConfigurationObjectPropertyRelationships_UserConfigurationObjectWithValidPropertyRelationships_ReturnsTrue()
        {
            // Setup
            // Revert to default object instantiation on 04-02-2024
            UserConfiguration userConfiguration = new UserConfiguration();
            // User metrics
            userConfiguration.InsulinSensitivityFactor = 1; // Blood glucose level drops by 1 unit for every unit of insulin.
            userConfiguration.CarbToInsulinRatio = 10; // 1 unit of insulin for every 10g of carbs.
            userConfiguration.LongActingInsulinDoesRecommendation = 32; // 32 units of long acting insulin daily.
        
            // Action free isolation period
            userConfiguration.TargetIsolationHours = 4; // Aim to disregard 4 hours of event readings to ensure all previous actions are no longer affecting the users blood glucose level whilst leaving sufficient remaining event time for analysis.
            userConfiguration.MinIsolationHours = 3.5; // Disregard at least the first 3.5 hours of events in the overnight period to ensure previous actions aren't still having an effect.
            userConfiguration.MaxIsolationHours = 4.5; // Disregard at most the first 4.5 hours of events in the overnight period to ensure that the period being analysed is long enough ToDo: Replace this with a check on check analysis length long enough
            
            // Long acting insulin recalculation config variables
            userConfiguration.OvernightStartTime = new TimeSpan(20, 0, 0); // Overnight period for user starts at 8PM.
            userConfiguration.OvernightEndTime = new TimeSpan(6, 0, 0); // Overnight period for user ends at 6AM.
            userConfiguration.MinHoursOvernightWithoutAction = 7.5; // Ensure an overnight period without actions taken of at least 7.5 hours to leave time for sufficient isolation and analysis.
            userConfiguration.MaxHoursOvernightWithoutAction = 8.5; // Ensure an overnight period without actions taken of  8.5 or less to try to stay within an overnight reading and avoid the dawn effect.
            
            
            // Act
            bool userConfigPropertyRelationshipValidationResult = userConfiguration.ValidateConfigurationObjectPropertyRelationships();
            
            // Assert
            Assert.AreEqual(expected: true, actual: userConfigPropertyRelationshipValidationResult);
        }

        [Test]
        public void ValidateConfigurationObjectPropertyRelationships_HasInvalidRelationships_ReturnsFalseListsErrors()
        {
            // Setup
            // Revert to default object instantiation on 04-02-2024
            UserConfiguration userConfiguration = new UserConfiguration();
            // User metrics
            userConfiguration.InsulinSensitivityFactor = 1; // Blood glucose level drops by 1 unit for every unit of insulin.
            userConfiguration.CarbToInsulinRatio = 10; // 1 unit of insulin for every 10g of carbs.
            userConfiguration.LongActingInsulinDoesRecommendation = 32; // 32 units of long acting insulin daily.
        
            // Action free isolation period
            userConfiguration.TargetIsolationHours = 4; // Aim to disregard 4 hours of event readings to ensure all previous actions are no longer affecting the users blood glucose level whilst leaving sufficient remaining event time for analysis.
            userConfiguration.MinIsolationHours = 5; // Disregard at least the first 3.5 hours of events in the overnight period to ensure previous actions aren't still having an effect.
            userConfiguration.MaxIsolationHours = 3; // Disregard at most the first 4.5 hours of events in the overnight period to ensure that the period being analysed is long enough ToDo: Replace this with a check on check analysis length long enough
            
            // Long acting insulin recalculation config variables
            userConfiguration.OvernightStartTime = new TimeSpan(20, 0, 0); // Overnight period for user starts at 8PM.
            userConfiguration.OvernightEndTime = new TimeSpan(6, 0, 0); // Overnight period for user ends at 6AM.
            userConfiguration.MinHoursOvernightWithoutAction = 9; // Ensure an overnight period without actions taken of at least 7.5 hours to leave time for sufficient isolation and analysis.
            userConfiguration.MaxHoursOvernightWithoutAction = 8.5; // Ensure an overnight period without actions taken of  8.5 or less to try to stay within an overnight reading and avoid the dawn effect.
            
            
            // Act
            bool userConfigPropertyRelationshipValidationResult = userConfiguration.ValidateConfigurationObjectPropertyRelationships();
            
            // Assert
            Assert.AreEqual(expected: false, actual: userConfigPropertyRelationshipValidationResult);
            // Assert.IsNotNull(anObject: userConfigPropertyRelationshipValidationResult.Item2);
            // Assert.AreEqual(expected: 4, actual: userConfigPropertyRelationshipValidationResult.Item2.Count);
            
            // string targetIsolationHoursNotLessThanMinIsolationHoursErrorMsg = string.Format(
            //     UserConfigurationErrorMessages.TargetIsolationHoursNotLessThanMinIsolationHours,
            //     userConfiguration.MinIsolationHours,
            //     userConfiguration.TargetIsolationHours
            // );
            // Assert.Contains(
            //     expected: targetIsolationHoursNotLessThanMinIsolationHoursErrorMsg,
            //     actual: userConfigPropertyRelationshipValidationResult.Item2
            // );
            //
            // string maxIsolationHoursNotLessThanTargetIsolationHoursErrorMsg = string.Format(
            //     UserConfigurationErrorMessages.MaxIsolationHoursNotLessThanTargetIsolationHours,
            //     userConfiguration.MaxIsolationHours,
            //     userConfiguration.TargetIsolationHours
            // );
            // Assert.Contains(
            //     expected: maxIsolationHoursNotLessThanTargetIsolationHoursErrorMsg,
            //     actual: userConfigPropertyRelationshipValidationResult.Item2
            // );
            //
            // string maxIsolationHoursNotLessThanMinIsolationHoursErrorMsg = string.Format(
            //     UserConfigurationErrorMessages.MaxIsolationHoursNotLessThanMinIsolationHours,
            //     userConfiguration.MaxIsolationHours,
            //     userConfiguration.MinIsolationHours
            // );
            // Assert.Contains(
            //     expected: maxIsolationHoursNotLessThanMinIsolationHoursErrorMsg,
            //     actual: userConfigPropertyRelationshipValidationResult.Item2
            // );
            //
            // string maxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutActionErrorMsg = string.Format(
            //     UserConfigurationErrorMessages.MaxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutAction,
            //     userConfiguration.MaxHoursOvernightWithoutAction,
            //     userConfiguration.MinHoursOvernightWithoutAction
            // );
            // Assert.Contains(
            //     expected: maxHoursOvernightWithoutActionNotLessThanMinHoursOvernightWithoutActionErrorMsg,
            //     actual: userConfigPropertyRelationshipValidationResult.Item2
            // );
        }
    }
}