using System;
using System.Collections.Generic;


namespace Diabetes
{
    public class SampleEvents
    {
        public List<EventData> Events { get; set; }

        public SampleEvents()
        {
            Events = new List<EventData>();
            Events.Add(event1);
            Events.Add(event2);
            Events.Add(event3);
            Events.Add(event4);
        }
        
        // Events
        EventData event1 = new EventData
        {
            EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
            BloodGLucoseLevel = 10
        };

        EventData event2 = new EventData
        {
            EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
            Carbs = 30
        };

        EventData event3 = new EventData
        {
            EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
            ShortActingInsulinUnits = 4
        };

        EventData event4 = new EventData
        {
            EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
            LongActingInsulinUnits = 16
        };
    }
}