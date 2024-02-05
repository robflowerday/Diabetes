using System.Collections.Generic;

using Diabetes.User;


namespace Diabetes.MetricRecalculationStrategies
{
    public interface IDynamicallyRecalculateMetricsStrategy
    {
        void DynamicallyRecalculateMetricsStrategy(List<EventData> events,
            UserConfigurationHandler userConfigurationHandler);
    }
}