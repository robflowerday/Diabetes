using System.IO;

using Newtonsoft.Json;

using Diabetes.User.FileIO;


// ToDo: Make sure correct use of singleton pattern, or not and decide how and when this should be called or instantiated along with UserConfiguration objects.
namespace Diabetes.User
{
    public class UserConfigurationHandler
    {
        private IFileIO _fileIO;
        private string _userConfigurationFilePath;
        // private UserConfiguration _userConfigurationObject;

        public UserConfigurationHandler(string userConfigurationFilePath, IFileIO fileIO)
        {
            _fileIO = fileIO;
            _userConfigurationFilePath = userConfigurationFilePath;
        }

        public UserConfiguration LoadOrCreateUserConfiguration()
        {
            if (_fileIO.Exists(_userConfigurationFilePath))
            {
                string userConfigJsonString = _fileIO.ReadAllText(_userConfigurationFilePath);
                UserConfiguration userConfigurationObject =
                    JsonConvert.DeserializeObject<UserConfiguration>(value: userConfigJsonString);
                // _userConfigurationObject = userConfigurationObject;
                return userConfigurationObject;
            }
            else
            {
                UserConfiguration userConfigurationObject = new UserConfiguration();
                // _userConfigurationObject = userConfigurationObject;
                return userConfigurationObject;
            }
        }

        public void SaveUserConfiguration(UserConfiguration userConfigurationObject)
        {
            string userConfigurationJsonString = JsonConvert.SerializeObject(value: userConfigurationObject, formatting: Formatting.Indented);
            _fileIO.WriteAllText(path: _userConfigurationFilePath, contents: userConfigurationJsonString);
        }
    }
}