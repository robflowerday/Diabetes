// using System;
// using System.Collections.Generic;
// using Diabetes.ExternalStorage;
// using Diabetes.ExternalStorage.DataModels;
//
//
// namespace Diabetes.MetricRecalculationStrategies
// {
//     public class RecalculateLongActingOnlyStrategy : IRecalculateMetricsStrategy<UserConfiguration>
//     {
//         public void RecalculateMetricsStrategy(List<EventData> events,
//             DataIOHandler<UserConfiguration> userConfigurationDataIoHandler)
//         {
//             // Load user configuration
//             UserConfiguration userConfigurationDataModelInstance = userConfigurationDataIoHandler.LoadOrCreateDataModelInstance();
//             TimeSpan overnightPeriodStart = userConfigurationDataModelInstance.OvernightStartTime;
//             TimeSpan overnightPeriodEnd = userConfigurationDataModelInstance.OvernightEndTime;
//             double minHoursWithoutAction = userConfigurationDataModelInstance.MinHoursOvernightWithoutAction;
//             double maxHoursWithoutAction = userConfigurationDataModelInstance.MaxHoursOvernightWithoutAction;
//             double targetHoursAfterFirstEvent = userConfigurationDataModelInstance.TargetIsolationHours;
//             double minHoursAfterFirstEvent = userConfigurationDataModelInstance.MinIsolationHours;
//             double maxHoursAfterFirstEvent = userConfigurationDataModelInstance.MaxIsolationHours;
//             int longActingInsulinDoseRecommendation = userConfigurationDataModelInstance.LongActingInsulinDoesRecommendation;
//             
//             // Filter events in applicable overnight period
//             List<EventData> overnightEvents = EventHelperFunctions.EventPeriodFilterFunctions.GetEventsInPeriod(
//                 inputEvents: events,
//                 periodStart: overnightPeriodStart, periodEnd: overnightPeriodEnd
//             );
//             
//             // Find period with no actions taken
//             List<EventData> actionFreePeriodEvents = EventHelperFunctions.EventPeriodFilterFunctions.GetFirstXHourPeriodWithNoActions(
//                 inputEvents: overnightEvents,
//                 minHoursWithoutAction: minHoursWithoutAction, maxHoursWithoutAction: maxHoursWithoutAction
//             );
//             
//             // Get event x hours into period
//             EventData startingGlucoseEvent = EventHelperFunctions.EventPeriodFilterFunctions.GetClosestEventToXHoursIn(
//                 inputEvents: actionFreePeriodEvents,
//                 targetHoursAfterFirstEvent: targetHoursAfterFirstEvent,
//                 minHoursAfterFirstEvent: minHoursAfterFirstEvent, maxHoursAfterFirstEvent: maxHoursAfterFirstEvent
//             );
//             
//             // Get last event in period
//             EventData endingGlucoseEvent =
//                 EventHelperFunctions.EventPeriodFilterFunctions.GetLastEventInPeriod(inputEvents: actionFreePeriodEvents);
//             
//             // Calculate change in blood glucose readings
//             double changeInBloodGlucose =
//                 EventHelperFunctions.EventCalcuationFunctions.CalculateBGChage(
//                     event1: startingGlucoseEvent, event2: endingGlucoseEvent
//                 );
//             
//             // Determine change in long acting insulin based off change
//             int recommendedDoseIncrement = CalculateLongActingDosageIncrement(changeInBG: changeInBloodGlucose);
//             
//             // Set new long acting dose recommendation
//             longActingInsulinDoseRecommendation += recommendedDoseIncrement;
//             if (recommendedDoseIncrement == 0)
//                 Console.WriteLine("No change in recommended dosage of long acting insulin.");
//             else
//                 Console.WriteLine(
//                     $"Long acting insulin dosage recommendation changed by: {changeInBloodGlucose} to {recommendedDoseIncrement} units daily.");
//             
//             // Save new long acting dose recommendation to file
//             userConfigurationDataModelInstance.LongActingInsulinDoesRecommendation = longActingInsulinDoseRecommendation;
//             userConfigurationDataIoHandler.SaveDataModelInstanceToFile(userConfigurationDataModelInstance);
//         }
//     }
// }