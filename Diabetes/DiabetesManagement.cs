using System;
using System.Collections.Generic;

using Diabetes.ExternalStorage;
using Diabetes.ExternalStorage.DataModels;
using Diabetes.MetricRecalculationStrategies;


namespace Diabetes
{
    public class DiabetesManagement
    {
        private List<EventData> _events = new List<EventData>();
        private List<string> _log = new List<string>();
        private IRecalculateMetricsStrategy<UserConfiguration> _recalculateMetricsStrategy;
        private DataIOHandler<UserConfiguration> _userConfigurationDataIOHandler;
        
        // // Initialize metrics
        // private double _isf = 50;
        // private double _cir = 15;
        // private double _longActingInsulinDoseRecommendation = 20;

        public DiabetesManagement(DataIOHandler<UserConfiguration> userConfigurationDataIoHandler,
            IRecalculateMetricsStrategy<UserConfiguration> recalculateMetricsStrategy)
        {
            _recalculateMetricsStrategy = recalculateMetricsStrategy;
            _userConfigurationDataIOHandler = userConfigurationDataIoHandler;
        }

        public void AddEvent(EventData data, bool displayLogs = false)
        {
            _events.Add(data);
            // AddLog(data, displayLogs);
        }

        public void SetRecalculateMetrics(IRecalculateMetricsStrategy<UserConfiguration> recalculateMetricsStrategy)
        {
            _recalculateMetricsStrategy = recalculateMetricsStrategy;
        }

        public void RecalculateMetrics()
        {
            _recalculateMetricsStrategy.RecalculateMetricsStrategy(events: _events,
                dataIOHandler: _userConfigurationDataIOHandler);
        }

        // public void AddLog(EventData data, bool displayLogs = false)
        // {
        //     _log.Add($"data: {data}, isf: {_isf}, cir: {_cir}, long acting insulin recommendation: {_longActingInsulinDoseRecommendation}");
        //     if (displayLogs)
        //     {
        //         Console.WriteLine($"data: {data}");
        //         Console.WriteLine($"EventDateTime: {data.EventDateTime}");
        //         Console.WriteLine($"Carbs: {data.Carbs}");
        //         Console.WriteLine($"ShortActingInsulinUnits: {data.ShortActingInsulinUnits}");
        //         Console.WriteLine($"LongActingInsulinUnits: {data.LongActingInsulinUnits}");
        //         Console.WriteLine($"BloodGLucoseLevel: {data.BloodGLucoseLevel}");
        //         Console.WriteLine($"isf: {_isf}");
        //         Console.WriteLine($"cir: {_cir}");
        //         Console.WriteLine($"Long acting insulin dose recommendation: {_longActingInsulinDoseRecommendation}");
        //     }
        // }

        public double CalculateMealBolus(int carbs)
        {
            return carbs / _cir;
        }
    }
}