using NUnit.Framework;

namespace Diabetes.Test.HelperFunctions.DateTimeHelperFunctionsTests
{
    [TestFixture]
    public class GetSleepingPeriodStartAndEndDateTimesTests
    {
        /// <summary>
        /// If the start period time span is later than the end period time span, provided there is not
        /// a data error, the timespan must go overnight, it is assumed that it should only go over 1 night.
        /// </summary>
        [Test]
        public void GetSleepingPeriodStartAndEndDateTimes_StartPeriodTimeSpanLaterThanEndPeriodTimeSpan_()
        {
            
        }
    }
}