using System.Collections.Generic;

namespace Diabetes.DynamicMetricRecalculationStrategies
{
    public interface IDynamicallyRecalculateMetricsStrategy
    {
        void DynamicallyRecalculateMetricsStrategy(List<EventData> events, ref double isf, ref double cir,
            ref double longActingInsulinDoseRecommendation);
    }
}