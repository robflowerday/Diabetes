// using System;
// using System.Collections.Generic;
// using System.IO;
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
//             // Instantiate user configuration settings variables
//             string userConfigurationJsonFilePath = "/home/robert/temp/user_configuration.json";
//             string eventDataJsonPath = "/home/robert/temp/event_data.json";
//             
//             // File.WriteAllText(path: eventDataJsonPath, contents: "hello");
//             
//             IFileIO fileIO = new FileIO();
//             DataIOHandler<UserConfiguration> userConfigurationDataIOHandler = new DataIOHandler<UserConfiguration>(
//                 jsonFilePath: userConfigurationJsonFilePath,
//                 fileIO: fileIO
//             );
//             DataIOHandler<EventData> eventDataIOHandler = new DataIOHandler<EventData>(
//                 jsonFilePath: eventDataJsonPath,
//                 fileIO: fileIO
//             );
//             
//             // Instantiate initial metric recalculation strategy
//             IRecalculateMetricsStrategy<UserConfiguration> recalculateMetricsStrategy =
//                 new RecalculateLongActingOnlyStrategyLastNightReadings();
//             
//             // Instantiate diabetes manager
//             DiabetesManagement diabetesManagement = new DiabetesManagement(
//                 recalculateMetricsStrategy: recalculateMetricsStrategy,
//                 userConfigurationDataIOHandler: userConfigurationDataIOHandler,
//                 eventDataIOHandler: eventDataIOHandler
//             );
//             
//             // Create events and lists of events
//             EventData event1 = new EventData { EventDateTime = DateTime.Now.AddHours(-9), BloodGLucoseLevel = 14 };
//             EventData event2 = new EventData { EventDateTime = DateTime.Now.AddHours(-8), BloodGLucoseLevel = 13 };
//             EventData event3 = new EventData { EventDateTime = DateTime.Now.AddHours(-7), BloodGLucoseLevel = 12 };
//             
//             EventData event4 = new EventData { EventDateTime = DateTime.Now.AddHours(-6), BloodGLucoseLevel = 11 };
//             EventData event5 = new EventData { EventDateTime = DateTime.Now.AddHours(-5), BloodGLucoseLevel = 10 };
//             EventData event6 = new EventData { EventDateTime = DateTime.Now.AddHours(-4), BloodGLucoseLevel = 9 };
//             EventData event7 = new EventData { EventDateTime = DateTime.Now.AddHours(-3), BloodGLucoseLevel = 8 };
//             EventData event8 = new EventData { EventDateTime = DateTime.Now.AddHours(-2), BloodGLucoseLevel = 7 };
//             EventData event9 = new EventData { EventDateTime = DateTime.Now.AddHours(-1), BloodGLucoseLevel = 6 };
//
//             EventData event10 = new EventData { EventDateTime = DateTime.Now.AddHours(-10), BloodGLucoseLevel = 11 };
//             EventData event11 = new EventData { EventDateTime = DateTime.Now.AddHours(-11), BloodGLucoseLevel = 11 };
//             EventData event12 = new EventData { EventDateTime = DateTime.Now.AddHours(-12), BloodGLucoseLevel = 11 };
//             EventData event13 = new EventData { EventDateTime = DateTime.Now.AddHours(-13), BloodGLucoseLevel = 11 };
//             EventData event14 = new EventData { EventDateTime = DateTime.Now.AddHours(-14), BloodGLucoseLevel = 11 };
//             EventData event15 = new EventData { EventDateTime = DateTime.Now.AddHours(-15), BloodGLucoseLevel = 11 };
//             EventData event16 = new EventData { EventDateTime = DateTime.Now.AddHours(-16), BloodGLucoseLevel = 11 };
//             EventData event17 = new EventData { EventDateTime = DateTime.Now.AddHours(-17), BloodGLucoseLevel = 11 };
//             EventData event18 = new EventData { EventDateTime = DateTime.Now.AddHours(-18), BloodGLucoseLevel = 11 };
//             EventData event19 = new EventData { EventDateTime = DateTime.Now.AddHours(-19), BloodGLucoseLevel = 11 };
//             EventData event20 = new EventData { EventDateTime = DateTime.Now.AddHours(-20), BloodGLucoseLevel = 11 };
//             EventData event21 = new EventData { EventDateTime = DateTime.Now.AddHours(-21), BloodGLucoseLevel = 11 };
//             EventData event22 = new EventData { EventDateTime = DateTime.Now.AddHours(-22), BloodGLucoseLevel = 11 };
//             EventData event23 = new EventData { EventDateTime = DateTime.Now.AddHours(-23), BloodGLucoseLevel = 11 };
//             EventData event24 = new EventData { EventDateTime = DateTime.Now.AddHours(-24), BloodGLucoseLevel = 11 };
//
//             List<EventData> eventList = new List<EventData>();
//             eventList.Add(event4);
//             eventList.Add(event5);
//             eventList.Add(event6);
//             eventList.Add(event7);
//             eventList.Add(event8);
//             eventList.Add(event9);
//             eventList.Add(event10);
//             eventList.Add(event11);
//             eventList.Add(event12);
//             eventList.Add(event13);
//             eventList.Add(event14);
//             eventList.Add(event15);
//             eventList.Add(event16);
//             eventList.Add(event17);
//             eventList.Add(event18);
//             eventList.Add(event19);
//             eventList.Add(event20);
//             eventList.Add(event21);
//             eventList.Add(event22);
//             eventList.Add(event23);
//             eventList.Add(event24);
//             
//             // Add these events to the diabetes manager
//             diabetesManagement.AddEvent(eventData: event1);
//             diabetesManagement.AddEvent(eventData: event2);
//             diabetesManagement.AddEvent(eventData: event3);
//             
//             diabetesManagement.AddEvents(eventDataList: eventList);
//             
//             // Recalculate long acting insulin
//             diabetesManagement.RecalculateMetrics();
//         }
//     }
// }