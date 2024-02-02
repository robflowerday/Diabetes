using System;
using System.Collections.Generic;
using Diabetes.DynamicMetricRecalculationStrategies;


namespace Diabetes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Decide metric recalculation strategy
            IDynamicallyRecalculateMetricsStrategy initialStrategy = RecalculateLongActingOnlyStrategy.GetInstance();
            
            // Instantiate app
            DiabetesManagement dm = new DiabetesManagement(initialStrategy);
            
            // Get sample events
            List<EventData> sampleEvents = new SampleEvents().Events;

            foreach (EventData evnt in sampleEvents)
            {
                
            }
        }
    }
}