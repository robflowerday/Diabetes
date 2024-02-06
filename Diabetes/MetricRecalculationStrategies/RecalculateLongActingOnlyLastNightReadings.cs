using System;
using System.Collections.Generic;
using System.Linq;

using Diabetes.DoseIncrementHelperFunctions;
using Diabetes.ExternalStorage;
using Diabetes.ExternalStorage.DataModels;
using Diabetes.HelperFunctions;
using Diabetes.HelperFunctions.EventHelperFunctions;


namespace Diabetes.MetricRecalculationStrategies
{
    public class RecalculateLongActingOnlyStrategyLastNightReadings : IRecalculateMetricsStrategy<UserConfiguration>
    {
        public void RecalculateMetricsStrategy(List<EventData> events,
            DataIOHandler<UserConfiguration> userConfigurationDataIoHandler)
        {
            // Load user configuration
            UserConfiguration userConfigurationDataModelInstance = userConfigurationDataIoHandler.LoadOrCreateDataModelInstance();
            TimeSpan overnightPeriodStartTime = userConfigurationDataModelInstance.OvernightStartTime;
            TimeSpan overnightPeriodEndTime = userConfigurationDataModelInstance.OvernightEndTime;
            double minHoursWithoutAction = userConfigurationDataModelInstance.MinHoursOvernightWithoutAction;
            double maxHoursWithoutAction = userConfigurationDataModelInstance.MaxHoursOvernightWithoutAction;
            double targetHoursAfterFirstEvent = userConfigurationDataModelInstance.TargetIsolationHours;
            double minHoursAfterFirstEvent = userConfigurationDataModelInstance.MinIsolationHours;
            double maxHoursAfterFirstEvent = userConfigurationDataModelInstance.MaxIsolationHours;
            int longActingInsulinDoseRecommendation = userConfigurationDataModelInstance.LongActingInsulinDoesRecommendation;
            
            // Filter events in applicable overnight period
            var (sleepPeriodStartDateTime, sleepPeriodEndDateTime)=
                    DateTimeHelperFunctions.GetSleepingPeriodStartAndEndDateTimes(
                        sleepPeriodStartTimeSpan: overnightPeriodStartTime, sleepPeriodEndTimeSpan: overnightPeriodEndTime);
            List<EventData> overnightEvents = EventPeriodFilterFunctions.GetEventsInPeriod(
                inputEvents: events,
                periodStartDateTime: sleepPeriodStartDateTime, periodEndDateTime: sleepPeriodEndDateTime
            );
            
            // Log if there are no overnight events
            Console.WriteLine($"No overnight events between {sleepPeriodStartDateTime} and {sleepPeriodEndDateTime} found.");
            
            // Order events
            List<EventData> orderedOvernightEvents =
                EventPeriodFilterFunctions.OrderEventDataList(inputEvents: overnightEvents);
            
            // Find period with no actions taken
            List<EventData> actionFreePeriodEvents = EventPeriodFilterFunctions.GetFirstXHourPeriodWithNoActions(
                orderedInputEvents: orderedOvernightEvents,
                minHoursWithoutAction: minHoursWithoutAction, maxHoursWithoutAction: maxHoursWithoutAction
            );
            
            // Log if there are no valid action free periods found
            Console.WriteLine($"No valid action free periods found between {minHoursWithoutAction} and {maxHoursWithoutAction} hours long.");
            
            // Get event x hours into period
            EventData startingGlucoseEvent = EventPeriodFilterFunctions.GetClosestEventToXHoursIn(
                inputEvents: actionFreePeriodEvents,
                targetHoursAfterFirstEvent: targetHoursAfterFirstEvent,
                minHoursAfterFirstEvent: minHoursAfterFirstEvent, maxHoursAfterFirstEvent: maxHoursAfterFirstEvent
            );
            
            // Log if there's no event within the allowable starting range
            Console.WriteLine(
                $"No event can be found that is at least {minHoursAfterFirstEvent} after the first event in the action free period and at most {maxHoursAfterFirstEvent} after the first event in the action free period.");
            
            // Get last event in period
            EventData endingGlucoseEvent = null;
            if (actionFreePeriodEvents.Count > 0)
                endingGlucoseEvent = actionFreePeriodEvents.Last();
            
            // Determine change in long acting insulin based off change in blood glucose readings
            double? initialBloodGlucoseReading = null;
            if (startingGlucoseEvent != null && startingGlucoseEvent.BloodGLucoseLevel != null)
                initialBloodGlucoseReading = startingGlucoseEvent.BloodGLucoseLevel;
            double? finalBloodGlucoseReading = null;
            if (endingGlucoseEvent != null && endingGlucoseEvent.BloodGLucoseLevel != null)
                finalBloodGlucoseReading = endingGlucoseEvent.BloodGLucoseLevel;
            int newLongActingInsulinDoseRecommendation = CalculateLongActingDosageIncrement.SimpleCalculation(
                initialBloodGlucoseReading: initialBloodGlucoseReading,
                finalBloodGlucoseReading: finalBloodGlucoseReading,
                currentLongActingInsulinDoseRecommendation: longActingInsulinDoseRecommendation
            );
            
            // Save new long acting dose recommendation to file
            userConfigurationDataModelInstance.LongActingInsulinDoesRecommendation = newLongActingInsulinDoseRecommendation;
            userConfigurationDataIoHandler.SaveDataModelInstanceToFile(userConfigurationDataModelInstance);
        }
    }
}