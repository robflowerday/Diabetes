// using System.IO;
//
// using Newtonsoft.Json;
//
// // ToDo: Make sure correct use of singleton pattern, or not and decide how and when this should be called or instantiated along with UserConfiguration objects.
// namespace Diabetes.User
// {
//     public class UserConfigurationHandler
//     {
//         // private IFileIO _fileIO
//         private string _userConfigurationFilePath;
//         // private UserConfiguration _userConfigurationObject;
//
//         public UserConfigurationHandler(string userConfigurationFilePath)
//         {
//             _userConfigurationFilePath = userConfigurationFilePath;
//         }
//
//         public UserConfiguration LoadOrCreateUserConfiguration()
//         {
//             if (File.Exists(_userConfigurationFilePath))
//             {
//                 string userConfigJsonString = File.ReadAllText(_userConfigurationFilePath);
//                 UserConfiguration userConfigurationObject =
//                     JsonConvert.DeserializeObject<UserConfiguration>(value: userConfigJsonString);
//                 // _userConfigurationObject = userConfigurationObject;
//                 return userConfigurationObject;
//             }
//             else
//             {
//                 UserConfiguration userConfigurationObject = new UserConfiguration();
//                 // _userConfigurationObject = userConfigurationObject;
//                 return userConfigurationObject;
//             }
//         }
//
//         // public void SaveUserConfiguration()
//         // {
//         //     string userConfigurationJsonString = JsonConvert.SerializeObject(value: _userConfigurationObject, formatting: Formatting.Indented);
//         //     File.WriteAllText(path: _userConfigurationFilePath, contents: userConfigurationJsonString);
//         // }
//
//         public void SaveUserConfiguration(UserConfiguration userConfigurationObject)
//         {
//             string userConfigurationJsonString = JsonConvert.SerializeObject(value: userConfigurationObject, formatting: Formatting.Indented);
//             File.WriteAllText(path: _userConfigurationFilePath, contents: userConfigurationJsonString);
//         }
//     }
// }