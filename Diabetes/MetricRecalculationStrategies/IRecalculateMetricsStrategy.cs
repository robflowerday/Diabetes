using System.Collections.Generic;
using Diabetes.ExternalStorage;
using Diabetes.ExternalStorage.DataModels;


namespace Diabetes.MetricRecalculationStrategies
{
    public interface IRecalculateMetricsStrategy<TDataType> where TDataType : IDataModel
    {
        void RecalculateMetricsStrategy(List<EventData> events, DataIOHandler<TDataType> dataIOHandler);
    }
}