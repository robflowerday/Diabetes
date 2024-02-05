using System;
using System.Collections.Generic;
using System.Linq;
using Diabetes.ExternalStorage.DataModels;


namespace Diabetes.HelperFunctions.EventHelperFunctions
{
    public static class EventPeriodFilterFunctions
    {
        public static List<EventData> GetEventsInPeriod(List<EventData> inputEvents, DateTime periodStartDateTime,
            DateTime periodEndDateTime)
        {
            return inputEvents.Where(eventData =>
                eventData.EventDateTime >= periodStartDateTime && eventData.EventDateTime <= periodEndDateTime).ToList();
        }

        public static List<EventData> OrderEventDataList(List<EventData> inputEvents)
        {
            return inputEvents.OrderBy(eventData => eventData.EventDateTime).ToList();
        }

        /// <summary>
        /// Takes in a list of event data objects containing in ascending order according to their 'EventDateTime'
        /// property value.
        /// Returns the a list of event data objects containing all events that exist in the 'orderedInputEvents'
        /// list that belong to the first action free period of more than 'minHoursWithoutAction' and less than
        /// 'maxHoursWithoutAction' hours.
        /// </summary>
        /// <param name="orderedInputEvents"></param>
        /// <param name="minHoursWithoutAction"></param>
        /// <param name="maxHoursWithoutAction"></param>
        /// <returns></returns>
        public static List<EventData> GetFirstXHourPeriodWithNoActions(List<EventData> orderedInputEvents,
            double minHoursWithoutAction, double maxHoursWithoutAction)
        {
            DateTime actionFreePeriodStart = DateTime.MinValue;
            DateTime actionFreePeriodEnd = DateTime.MinValue;
            List<EventData> actionFreeEventPeriodList = new List<EventData>();
            bool hasPeriodExceedingMaxHours = false;
            
            for (int i = 0; i < orderedInputEvents.Count; i++)
            {
                EventData currentEvent = orderedInputEvents[i];
                
                // Check if current event has only event datetime and blood glucose reading
                if (EventPropertyFilterFunctions.IsBloodGlucoseOnlyEvent(currentEvent))
                {
                    // Add the event to the current action free event period list
                    actionFreeEventPeriodList.Add(currentEvent);
                    
                    // If not currently in an action free period, start one
                    if (actionFreePeriodStart == DateTime.MinValue)
                        actionFreePeriodStart = currentEvent.EventDateTime;
                    
                    // Update the end period of the event to the current period regardless
                    actionFreePeriodEnd = currentEvent.EventDateTime;
                    
                    // If the current action free period length is within the specified bounds,
                    // return the action free period
                    double actionFreePeriodLengthInHours = (actionFreePeriodEnd - actionFreePeriodStart).TotalHours;
                    if (actionFreePeriodLengthInHours > minHoursWithoutAction &&
                        actionFreePeriodLengthInHours < maxHoursWithoutAction)
                    {
                        return actionFreeEventPeriodList;
                    }
                    
                    // If the current action free period length is shorter than the specified bound,
                    // continue as you are
                    
                    // If the current action free period length is longer than the max number of hours,
                    // continue, but mark that this has happened because if none are found and this has
                    // occured, we'll have to loop back round starting at the second, then 3rd entry etc...
                    // to make sure that we haven't over-run because of an unusually long time between
                    // 2 periods.
                    // Also, reset the action free event period list and start and end period date times.
                    if (actionFreePeriodLengthInHours > maxHoursWithoutAction)
                    {
                        hasPeriodExceedingMaxHours = true;
                        actionFreePeriodStart = DateTime.MinValue;
                        actionFreePeriodEnd = DateTime.MinValue;
                        actionFreeEventPeriodList = new List<EventData>();
                    }
                }

                // If there is an action other than a blood glucose measurement in the event data, reset
                // the action free event period list, the start and the end period date times.
                else
                {
                    actionFreePeriodStart = DateTime.MinValue;
                    actionFreePeriodEnd = DateTime.MinValue;
                    actionFreeEventPeriodList = new List<EventData>();
                }
            }
                
            // If we've reached the last event without returning a value and at anypoint in the
            // last run we've exceeded the max number of hours in a period, we have to loop through
            // each again starting from the second, then the 3rd etc...
            if (hasPeriodExceedingMaxHours)
            {
                List<EventData> recursiveResult = GetFirstXHourPeriodWithNoActions(
                    orderedInputEvents: orderedInputEvents.Skip(1).ToList(),
                    minHoursWithoutAction: minHoursWithoutAction,
                    maxHoursWithoutAction: maxHoursWithoutAction);
                
                // Return the result of the recursive call, at worst, it will not have found a
                // valid period and will return an empty list
                return recursiveResult;
            }
            
            // If we've reached the last event without returning a value at anypoint in the
            // last run and we've never exceeded the maximum length of action free events in a
            // period, return an empty period.
            return new List<EventData>();
        }
        
        /// <summary>
        /// Finds the event in 'inputEvents' which has an 'EventDateTime' closest to the first
        /// event + 'targetHoursAfterFirstEvent' with the constraints that if no event is
        /// between the 'EventDateTime' of the first event + 'minHoursAfterFirstEvent' and
        /// the 'EventDateTime' of the first event + 'maxHoursAfterFirstEvent', returns null
        /// </summary>
        /// <param name="inputEvents"></param>
        /// <param name="targetHoursAfterFirstEvent"></param>
        /// <param name="minHoursAfterFirstEvent"></param>
        /// <param name="maxHoursAfterFirstEvent"></param>
        /// <returns></returns>
        public static EventData GetClosestEventToXHoursIn(List<EventData> inputEvents,
            double targetHoursAfterFirstEvent, double minHoursAfterFirstEvent, double maxHoursAfterFirstEvent)
        {
            // If the input events list is empty or null, return null
            if (inputEvents == null || inputEvents.Count == 0)
                return null;
            
            // Determine the first, target, minimum and maximum allowable date times
            DateTime firstDateTime = inputEvents.First().EventDateTime;
            DateTime targetDateTime = firstDateTime.AddHours(targetHoursAfterFirstEvent);
            DateTime minimumAllowedDateTime = firstDateTime.AddHours(minHoursAfterFirstEvent);
            DateTime maximumAllowedDateTime = firstDateTime.AddHours(maxHoursAfterFirstEvent);
            
            // filter events, keeping only events within the allowable range    
            List<EventData> eventsInRange = inputEvents.Where(eventData =>
                    eventData.EventDateTime > minimumAllowedDateTime &&
                    eventData.EventDateTime < maximumAllowedDateTime)
                .ToList();
            
            // If no events exist in the allowable range, return null
            if (eventsInRange.Count == 0)
                return null;
            
            // Fin the closest allowable event
            EventData closestEvent = eventsInRange
                .OrderBy(eventData => Math.Abs((eventData.EventDateTime - targetDateTime).Ticks)).First();
            
            // Return the closest event to the target date time
            return closestEvent;
        }
    }
}