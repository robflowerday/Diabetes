using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using Diabetes.ExternalStorage.DataModels;
using Diabetes.User.FileIO;


namespace Diabetes.ExternalStorage
{
    public class DataIOHandler<TDataModel> where TDataModel : IDataModel
    {
        private IFileIO _fileIO;
        private readonly string _jsonFilePath;

        public DataIOHandler(string jsonFilePath, IFileIO fileIO)
        {
            _fileIO = fileIO;
            _jsonFilePath = jsonFilePath;
        }

        public TDataModel LoadOrCreateDataModelInstance()
        {
            // Initialize user configuration object
            TDataModel dataModelInstance;
            
            // Check if file exists
            if (_fileIO.Exists(_jsonFilePath))
            {
                // Read in JSON file
                string jsonString = _fileIO.ReadAllText(_jsonFilePath);
                
                // Convert JSON string to C# object
                dataModelInstance = JsonConvert.DeserializeObject<TDataModel>(value: jsonString);
            }
            
            // If file doesn't exist
            else
            {
                // Create default data model instance
                dataModelInstance = Activator.CreateInstance<TDataModel>();
            }
                
            // Validate property relationships
            dataModelInstance.ValidateDataModelPropertyRelationships();
                
            // If file configuration is valid, return file configuration object
            return dataModelInstance;
        }

        public List<TDataModel> LoadOrCreateDataModelInstanceList()
        {
            // Instantiate data model list object
            List<TDataModel> dataModelListInstanceList;
            
            // Check if file exists
            if (_fileIO.Exists(_jsonFilePath))
            {
                // Read JSON file
                string jsonString = _fileIO.ReadAllText(_jsonFilePath);
                
                // Convert to data model list instance
                dataModelListInstanceList = JsonConvert.DeserializeObject<List<TDataModel>>(jsonString);
            }
            
            // If file doesn't exist, return an empty list instance
            else
            {
                dataModelListInstanceList = new List<TDataModel>();
            }
            
            // Validate data model property relationships
            foreach (TDataModel dataModelInstance in dataModelListInstanceList)
                dataModelInstance.ValidateDataModelPropertyRelationships();
            
            // If the data model instances are valid, return them
            return dataModelListInstanceList;
        }

        public void SaveDataModelInstanceToFile(TDataModel dataModelInstance)
        {
            // Convert data model instance to json string
            string jsonString = JsonConvert.SerializeObject(value: dataModelInstance, formatting: Formatting.Indented);
            
            // Write json string to file
            _fileIO.WriteAllText(path: _jsonFilePath, contents: jsonString);
        }

        public void SaveDataModelInstanceListToFile(List<TDataModel> dataModelInstanceList)
        {
            // Convert data model instance to json string
            string jsonString = JsonConvert.SerializeObject(value: dataModelInstanceList, formatting: Formatting.Indented);
            
            // If file doesn't already exist, create it
            // if (!_fileIO.Exists(_jsonFilePath))
            // {
            //     Console.WriteLine($"JSON file path: {_jsonFilePath} does not exist, creating new file.");
            //     File.Create(_jsonFilePath);
            // }
            // Write json string to file
            _fileIO.WriteAllText(path: _jsonFilePath, contents: jsonString);
        }
    }
}