using System;


namespace Diabetes
{
    public class EventData
    {
        public DateTime EventDateTime { get; set; }
        public int? Carbs { get; set; } = null;
        public int? ShortActingInsulinUnits { get; set; } = null;
        public int? LongActingInsulinUnits { get; set; } = null;
        public double? BloodGLucoseLevel { get; set; } = null;
    }
}