using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Diabetes.DynamicMetricRecalculationStrategies;


namespace Diabetes.Test.DynamicMetricRecalculationStrategies.RecalculateLongActingOnlyTests
{
    [TestFixture]
    public class GetLastNightsEventsTests
    {
        private static Dictionary<string, EventData> _testEventDictionary = new Dictionary<string, EventData>
        {
            { "middayMinusSecond", new EventData { EventDateTime = new DateTime(2024, 1, 1, 11, 59, 59) } },
            { "midday", new EventData { EventDateTime = new DateTime(2024, 1, 1, 12, 0, 0) } },
            { "middayAndSecond", new EventData { EventDateTime = new DateTime(2024, 1, 1, 12, 0, 1) } },
            { "SevenOClock", new EventData { EventDateTime = new DateTime(2024, 1, 1, 19, 0, 0) } },
            { "eightMinusSecond", new EventData { EventDateTime = new DateTime(2024, 1, 1, 19, 59, 59) } },
            { "eightOClock", new EventData { EventDateTime = new DateTime(2024, 1, 1, 20, 0, 0) } },
            { "eightAndSecond", new EventData { EventDateTime = new DateTime(2024, 1, 1, 20, 0, 1) } },
            { "nineOClock", new EventData { EventDateTime = new DateTime(2024, 1, 1, 21, 0, 0) } },
            { "midnightMinusSecond", new EventData { EventDateTime = new DateTime(2024, 1, 1, 23, 59, 59) } },
            { "midnight", new EventData { EventDateTime = new DateTime(2024, 1, 2, 0, 0, 0) } },
            { "midnightAndSecond", new EventData { EventDateTime = new DateTime(2024, 1, 2, 0, 0, 1) } },
            { "fourOClock", new EventData { EventDateTime = new DateTime(2024, 1, 2, 4, 0, 0) } }
        };

        private static List<EventData>[] _testCases =
        {
            new List<EventData>
            {
                _testEventDictionary["middayMinusSecond"],
                _testEventDictionary["fourOClock"]
            }
        };
        
        [Test]
        [TestCaseSource("_testCases")]
        public void GetLastNightsEvents__(List<EventData> expectedResult)
        {
            // Get initial events list
            List<EventData> events = new List<EventData>
            {
                _testEventDictionary["middayMinusSecond"],
                _testEventDictionary["midday"],
                _testEventDictionary["middayAndSecond"],
                _testEventDictionary["SevenOClock"],
                _testEventDictionary["eightMinusSecond"],
                _testEventDictionary["eightOClock"],
                _testEventDictionary["eightAndSecond"],
                _testEventDictionary["nineOClock"],
                _testEventDictionary["midnightMinusSecond"],
                _testEventDictionary["midnight"],
                _testEventDictionary["midnightAndSecond"],
                _testEventDictionary["fourOClock"]
            };
            
            // Process events
            RecalculateLongActingOnlyStrategy recalculateLongActingOnlyStrategy = RecalculateLongActingOnlyStrategy.GetInstance();
            List<EventData> lastNightsEvents = recalculateLongActingOnlyStrategy.GetLastNightsEvents(events: events, nightStartHour: 20, nightEndHour:6);
            
            // Assert result equals expected result
            bool areEqual = expectedResult.SequenceEqual(lastNightsEvents);
            Assert.AreEqual(expected: true, actual: areEqual);
        }

        [Test]
        [TestCase(20, 19)]
        [TestCase(1, 23)]
        public void GetLastNightsEvents_EndDateBeforeStart_ThrowsError()
        {
            // Get initial events list
            List<EventData> events = new List<EventData>
            {
                _testEventDictionary["middayMinusSecond"]
            };
            
            // Assert that processing events throws an error appropriately
            RecalculateLongActingOnlyStrategy recalculateLongActingOnlyStrategy = RecalculateLongActingOnlyStrategy.GetInstance();
            List<EventData> lastNightsEvents = recalculateLongActingOnlyStrategy.GetLastNightsEvents(events: events, currentDateTime:6);
        }
    }
}