// using System;
// using System.Collections.Generic;
//
// using Diabetes.DynamicMetricRecalculationStrategies;
// using Diabetes.User;
//
//
// namespace Diabetes
// {
//     public class DiabetesManagement
//     {
//         private List<EventData> _events = new List<EventData>();
//         private List<string> _log = new List<string>();
//         private IDynamicallyRecalculateMetricsStrategy _dynamicallyRecalculateMetricsStrategy;
//         private UserConfigurationHandler _userConfigurationHandler;
//         
//         // Initialize metrics
//         private double _isf = 50;
//         private double _cir = 15;
//         private double _longActingInsulinDoseRecommendation = 20;
//
//         public DiabetesManagement(UserConfigurationHandler userConfigurationHandler, IDynamicallyRecalculateMetricsStrategy dynamicallyRecalculateMetricsStrategy)
//         {
//             _dynamicallyRecalculateMetricsStrategy = dynamicallyRecalculateMetricsStrategy;
//             _userConfigurationHandler = userConfigurationHandler;
//         }
//
//         public void AddEvent(EventData data, bool displayLogs = false)
//         {
//             _events.Add(data);
//             // _dynamicallyRecalculateMetricsStrategy.DynamicallyRecalculateMetricsStrategy(_events, ref _isf, ref _cir,
//             //     ref _longActingInsulinDoseRecommendation);
//             AddLog(data, displayLogs);
//         }
//
//         public void SetDynamicallyRecalculateMetrics(IDynamicallyRecalculateMetricsStrategy dynamicallyRecalculateMetricsStrategy)
//         {
//             _dynamicallyRecalculateMetricsStrategy = dynamicallyRecalculateMetricsStrategy;
//         }
//
//         public void RecalculateMetrics()
//         {
//             _dynamicallyRecalculateMetricsStrategy.DynamicallyRecalculateMetricsStrategy(events: _events,
//                 userConfigurationHandler: _userConfigurationHandler);
//         }
//
//         public void AddLog(EventData data, bool displayLogs = false)
//         {
//             _log.Add($"data: {data}, isf: {_isf}, cir: {_cir}, long acting insulin recommendation: {_longActingInsulinDoseRecommendation}");
//             if (displayLogs)
//             {
//                 Console.WriteLine($"data: {data}");
//                 Console.WriteLine($"EventDateTime: {data.EventDateTime}");
//                 Console.WriteLine($"Carbs: {data.Carbs}");
//                 Console.WriteLine($"ShortActingInsulinUnits: {data.ShortActingInsulinUnits}");
//                 Console.WriteLine($"LongActingInsulinUnits: {data.LongActingInsulinUnits}");
//                 Console.WriteLine($"BloodGLucoseLevel: {data.BloodGLucoseLevel}");
//                 Console.WriteLine($"isf: {_isf}");
//                 Console.WriteLine($"cir: {_cir}");
//                 Console.WriteLine($"Long acting insulin dose recommendation: {_longActingInsulinDoseRecommendation}");
//             }
//         }
//
//         public double CalculateMealBolus(int carbs)
//         {
//             return carbs / _cir;
//         }
//     }
// }