using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Diabetes.User.FileIO;


// ToDo: Make sure correct use of singleton pattern, or not and decide how and when this should be called or instantiated along with UserConfiguration objects.
namespace Diabetes.User
{
    public class UserConfigurationHandler
    {
        private IFileIO _fileIO;
        private string _userConfigurationFilePath;

        public UserConfigurationHandler(string userConfigurationFilePath, IFileIO fileIO)
        {
            _fileIO = fileIO;
            _userConfigurationFilePath = userConfigurationFilePath;
        }

        public (UserConfiguration, List<string>, List<string>) LoadOrCreateUserConfiguration()
        {
            // Initialize user configuration object and potential error lists
            UserConfiguration userConfigurationObject;
            List<string> existingUserConfigurationFileRelationshipValidationErrors = new List<string>();
            List<string> defaultUserConfigurationFileRelationshipValidationErrors = new List<string>();
            
            // Check if file exists
            if (_fileIO.Exists(_userConfigurationFilePath))
            {
                // Read in file and convert to C# object
                string userConfigJsonString = _fileIO.ReadAllText(_userConfigurationFilePath);
                userConfigurationObject =
                    JsonConvert.DeserializeObject<UserConfiguration>(value: userConfigJsonString);
                (bool, List<string>) userConfigurationObjectValidationResponse =
                    userConfigurationObject.ValidateConfigurationObjectPropertyRelationships();
                
                // If file configuration is valid, return file configuration object
                if (userConfigurationObjectValidationResponse.Item1 == true)
                    return (userConfigurationObject, existingUserConfigurationFileRelationshipValidationErrors,
                        defaultUserConfigurationFileRelationshipValidationErrors);
                
                // Otherwise populate existing file error list
                else
                {
                    existingUserConfigurationFileRelationshipValidationErrors =
                        userConfigurationObjectValidationResponse.Item2;
                }
            }
            
            // Either file doesn't exist or existing file property relationships are invalid
            userConfigurationObject = new UserConfiguration();
            if (userConfigurationObject.ValidateConfigurationObjectPropertyRelationships().Item1)
                return (userConfigurationObject, existingUserConfigurationFileRelationshipValidationErrors,
                    defaultUserConfigurationFileRelationshipValidationErrors);
            else
            {
                throw new Exception($"Existing and default user configuration object are invalid.");
            }
        }

        public void SaveUserConfiguration(UserConfiguration userConfigurationObject)
        {
            string userConfigurationJsonString = JsonConvert.SerializeObject(value: userConfigurationObject, formatting: Formatting.Indented);
            _fileIO.WriteAllText(path: _userConfigurationFilePath, contents: userConfigurationJsonString);
        }
    }
}