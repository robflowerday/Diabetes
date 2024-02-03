using System.IO;
using Diabetes.User;
using Diabetes.User.FileIO;

namespace Diabetes
{
    public class FileIOTestProgram
    {
        public static void Main(string[] args)
        {
            string userConfigurationFilePath = "/home/rob/TempUserConfigDir/userConfigurationFile.json";
            UserConfigurationHandler userConfigurationHandler = new UserConfigurationHandler(userConfigurationFilePath, new FileIO());
            UserConfiguration userConfiguration = userConfigurationHandler.LoadOrCreateUserConfiguration();
            userConfigurationHandler.SaveUserConfiguration(userConfiguration);
        }
    }
}