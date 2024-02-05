// using Diabetes.ExternalStorage;
// using Diabetes.ExternalStorage.DataModels;
// using Diabetes.MetricRecalculationStrategies;
// using Diabetes.User.FileIO;
//
//
// namespace Diabetes
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             // Instantiate App
//             // Instantiate user settings
//             string userConfigurationJsonFIlePath = "/home/user/rob/temp/user_configuration.json";
//             IFileIO fileIo = new FileIO();
//             DataIOHandler<UserConfiguration> userConfigurationDataIOHandler = new DataIOHandler<UserConfiguration>(
//                 jsonFilePath: userConfigurationJsonFIlePath,
//                 fileIO: fileIo
//             );
//             IRecalculateMetricsStrategy<UserConfiguration> recalculateMetricsStrategy = new RecalculateLongActingOnlyStrategy();
//             DiabetesManagement diabetesManagement = new DiabetesManagement(
//                 userConfigurationDataIoHandler: userConfigurationDataIOHandler,
//                 recalculateMetricsStrategy: recalculateMetricsStrategy
//             );
//
//             // Add a bunch of historic events
//             Even
//             diabetesManagement.AddEvent();
//
//             // Add an event every 5 minutes
//
//             // Add an event for a meal
//
//             // Continue adding events every 5 minutes
//
//
//             // recalculate long acting insulin
//         }
//     }
// }