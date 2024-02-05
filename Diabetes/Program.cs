using Diabetes.ExternalStorage;
using Diabetes.ExternalStorage.DataModels;
using Diabetes.MetricRecalculationStrategies;
using Diabetes.User.FileIO;

namespace Diabetes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate App
            // Instantiate user settings

            string userConfigurationJsonFilePath = "/home/robert/temp/mynewjson.json";
            
            IFileIO fileIO = new FileIO();
            DataIOHandler<UserConfiguration> userConfigurationDataIOHandler = new DataIOHandler<UserConfiguration>(
                jsonFilePath: userConfigurationJsonFilePath,
                fileIO: fileIO
            );

            IRecalculateMetricsStrategy<UserConfiguration> recalculateMetricsStrategy =
                new RecalculateLongActingOnlyStrategy();
            
            DiabetesManagement diabetesManagement = new DiabetesManagement(
                userConfigurationDataIOHandler: userConfigurationDataIOHandler,
                recalculateMetricsStrategy: recalculateMetricsStrategy
            );

            // Add a bunch of historic events

            // Add an event every 5 minutes

            // Add an event for a meal

            // Continue adding events every 5 minutes


            // recalculate long acting insulin
        }
    }
}