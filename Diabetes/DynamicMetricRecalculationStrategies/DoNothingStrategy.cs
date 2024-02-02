using System.Collections.Generic;

namespace Diabetes.DynamicMetricRecalculationStrategies
{
    public class DoNothingStrategy : IDynamicallyRecalculateMetricsStrategy
    {
        public void DynamicallyRecalculateMetricsStrategy(List<EventData> events, ref double isf, ref double cir,
            ref double longActingInsulinDoseRecommendation)
        {
            // Do nothing
        }
    }
}