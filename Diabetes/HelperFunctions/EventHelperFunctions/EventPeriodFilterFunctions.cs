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
    }
}