using System;
using System.Collections.Generic;
using System.Linq;

namespace Diabetes.DynamicMetricRecalculationStrategies
{
    public class RecalculateLongActingOnlyStrategy : IDynamicallyRecalculateMetricsStrategy
    {
        private DateTime _lastRecalculationDateTime;
        private static RecalculateLongActingOnlyStrategy _instance;
        
        // singleton pattern used so that _lastRecalculationDateTime is always kept even if strategy is changed
        private RecalculateLongActingOnlyStrategy() {}

        public static RecalculateLongActingOnlyStrategy GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RecalculateLongActingOnlyStrategy();
            }

            return _instance;
        }
        
        public bool IsBloodGlucoseOnlyEvent(EventData eventData)
        {
            if (eventData.BloodGLucoseLevel.HasValue && !eventData.Carbs.HasValue &&
                !eventData.ShortActingInsulinUnits.HasValue && !eventData.LongActingInsulinUnits.HasValue)
                return true;
            return false;
        }

        public List<EventData> GetLastNightsEvents(List<EventData> events, int nightStartHour = 20,
            int nightEndHour = 6)
        {
            return events
                .Where(
                    eventData =>
                    eventData.EventDateTime > DateTime.Now.AddHours(-24) &&
                    (
                        eventData.EventDateTime.TimeOfDay > new TimeSpan(nightStartHour, 0, 0)
                        || eventData.EventDateTime.TimeOfDay < new TimeSpan(nightEndHour, 0, 0)
                    ))
                .OrderBy(eventData => eventData.EventDateTime)
                .ToList();
        }

        public (EventData firstEvent, EventData lastEvent) GetActionFreePeriod(List<EventData> events,
            double minHoursInPeriod = 7.7, double maxHoursInPeriod = 8.5)
        {
            EventData firstEvent = null;
            EventData lastEvent; // = null; // not needed
            double duration = 0;

            foreach (EventData eventData in events)
            {
                // Check if event has non-blood-sugar information, reset first and last if it is
                if (!IsBloodGlucoseOnlyEvent(eventData))
                {
                    firstEvent = null;
                    // lastEvent = null;  // not needed
                }
                else
                {
                    // If its the first event found set it as the first event
                    if (firstEvent == null)
                    {
                        firstEvent = eventData;
                    }
                    
                    // If it's not the first event, set the it to be the last event
                    else
                    {
                        lastEvent = eventData;
                        duration = (lastEvent.EventDateTime - firstEvent.EventDateTime).TotalHours;
                        if (duration >= minHoursInPeriod && duration <= maxHoursInPeriod)
                        {
                            return (firstEvent, lastEvent);
                        }
                    }
                }
            }
            Console.WriteLine($"No action free period found of between {minHoursInPeriod} and {maxHoursInPeriod} hours.");
            return (null, null);
        }

        public EventData GetStartingGlucoseMeasureEvent(List<EventData> events, EventData firstPeriodEvent,
            EventData lastPeriodEvent, double minHoursAfterFirstPeriodEvent = 3.5,
            double minHoursBeforeLastPeriodEvent = 3.5)
        {
            // If there is no period specfied, return null
            if (firstPeriodEvent == null)
                return null;
            
            // Get index of first period event in recent events
            int firstPeriodEventIndex = events.IndexOf(firstPeriodEvent);
            
            // Loop through events after first period event
            for (int i = firstPeriodEventIndex + 1; i < events.Count(); i++)
            {
                EventData eventData = events[i];

                double hoursAfterFirst = (eventData.EventDateTime - firstPeriodEvent.EventDateTime).TotalHours;
                double hoursBeforeLast = (lastPeriodEvent.EventDateTime - eventData.EventDateTime).TotalHours;

                if (hoursAfterFirst >= minHoursAfterFirstPeriodEvent &&
                    hoursBeforeLast >= minHoursBeforeLastPeriodEvent)
                {
                    return eventData;
                }
            }
            
            // If no event can be found in the period that satisfies the time constraints, return null
            Console.WriteLine(
                $"No events found in period {minHoursAfterFirstPeriodEvent} hours after {firstPeriodEvent.EventDateTime.TimeOfDay} on {firstPeriodEvent.EventDateTime.DayOfWeek}, {firstPeriodEvent.EventDateTime.Date} and {minHoursBeforeLastPeriodEvent} hours before {lastPeriodEvent.EventDateTime.TimeOfDay} on {lastPeriodEvent.EventDateTime.DayOfWeek}, {lastPeriodEvent.EventDateTime.Date}");
            return null;
        }
        
        public void DynamicallyRecalculateMetricsStrategy(List<EventData> events, ref double isf, ref double cir,
            ref double longActingInsulinDoseRecommendation)
        {
            // Get events in the last 24 hours
            List<EventData> recentEvents = GetLastNightsEvents(events);
            
            // Get 7.5 to 8.5 hour period without any actions take
            var (firstEventInLongPeriod, lastEventInPeriod) = GetActionFreePeriod(recentEvents);
            
            // Get first event x hours after long period start
            EventData startingGlucoseMeasureEvent = GetStartingGlucoseMeasureEvent(recentEvents, firstEventInLongPeriod, lastEventInPeriod);
            
            // If an action free period exists,
            // and an event within the given time constraints exists
            // Calculate the change in blood sugar
            double? bgChange = 0;
            if (startingGlucoseMeasureEvent != null)
            {
                bgChange = lastEventInPeriod.BloodGLucoseLevel - startingGlucoseMeasureEvent.BloodGLucoseLevel;
            }

            // determine change in long acting recommendation based on change in bg overnight
            if (Math.Abs((double)bgChange) > 1)
            {
                longActingInsulinDoseRecommendation += Math.Round(longActingInsulinDoseRecommendation / 10);
                Console.WriteLine($"Long acting insulin changed by {Math.Round(longActingInsulinDoseRecommendation / 10)} units.");
            }
            else
            {
                Console.WriteLine($"Long acting insulin not changed");
            }
        }
    }
}