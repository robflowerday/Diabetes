// using System;
// using System.Collections.Generic;
// using Diabetes.DynamicMetricRecalculationStrategies;
//
// using Diabetes.User;
//
//
// namespace Diabetes
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             // Create user configuration handler
//             string userConfigurationFilePath = "/home/rob/TempUserConfigDir/userConfigurationFile.json";
//             UserConfigurationHandler userConfigurationHandler =
//                 new UserConfigurationHandler(userConfigurationFilePath: userConfigurationFilePath);
//             
//             // Decide metric recalculation strategy
//             IDynamicallyRecalculateMetricsStrategy initialStrategy = new RecalculateLongActingOnlyStrategy();
//             
//             // Instantiate app
//             DiabetesManagement dm = new DiabetesManagement(userConfigurationHandler: userConfigurationHandler,
//                 dynamicallyRecalculateMetricsStrategy: initialStrategy);
//         }
//     }
// }