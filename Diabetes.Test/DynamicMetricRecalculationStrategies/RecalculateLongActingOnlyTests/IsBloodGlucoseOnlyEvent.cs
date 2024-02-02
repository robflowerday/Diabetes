using System;

using NUnit.Framework;

using Diabetes.DynamicMetricRecalculationStrategies;


namespace Diabetes.Test.DynamicMetricRecalculationStrategies.RecalculateLongActingOnlyTests
{
    [TestFixture]
    public class IsBloodGlucoseOnlyEvent
    {
        public class TestData
        {
            public EventData EventData { get; set; }
            public bool ExpectedResult { get; set; }
        }

        private static TestData[] testCases =
        {
            // Carb only event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    Carbs = 10
                },
                ExpectedResult = false
            },
            // Short acting insulin only event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    ShortActingInsulinUnits = 10
                },
                ExpectedResult = false
            },
            // Long acting insulin only event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    ShortActingInsulinUnits = 10
                },
                ExpectedResult = false
            },
            // Blood glucose only event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    BloodGLucoseLevel = 10
                },
                ExpectedResult = true
            },
            // Carb and blood glucose only event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    Carbs = 10,
                    BloodGLucoseLevel = 10
                },
                ExpectedResult = false
            },
            // Full event
            new TestData
            {
                EventData = new EventData
                {
                    EventDateTime = new DateTime(2024, 1, 3, 10, 30, 0),
                    Carbs = 10,
                    ShortActingInsulinUnits = 10,
                    LongActingInsulinUnits = 10,
                    BloodGLucoseLevel = 10
                },
                ExpectedResult = false
            }
        };
        
        [Test]
        [TestCaseSource("testCases")]
        public void IsBloodGlucoseOnlyEvent_WhenCalled_ReturnsExpectedResult(TestData testData)
        {
            RecalculateLongActingOnlyStrategy recalculateLongActingOnlyStrategy = RecalculateLongActingOnlyStrategy.GetInstance();

            bool result = recalculateLongActingOnlyStrategy.IsBloodGlucoseOnlyEvent(testData.EventData);
            
            Assert.AreEqual(expected: testData.ExpectedResult, actual: result);
        }
    }
}