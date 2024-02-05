using System.Collections.Generic;

using Diabetes.ExternalStorage;
using Diabetes.ExternalStorage.DataModels;
using Diabetes.User.FileIO;

namespace Diabetes
{
    public class ProgramEventDataList
    {
        public static void Main(string[] args)
        {
            string jsonFilePath = "/home/robert/temp/tempfile.json";
            
            DataIOHandler<EventData> eventDataListIOHandler =
                new DataIOHandler<EventData>(
                    jsonFilePath: jsonFilePath,
                    fileIO: new FileIO()
                );

            List<EventData> events = eventDataListIOHandler.LoadOrCreateDataModelInstanceList();
            eventDataListIOHandler.SaveDataModelInstanceListToFile(events);
            
            // File.WriteAllText(path: jsonFilePath, contents: "hello");
            // redundant change
        }
    }
}