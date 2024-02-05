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
            // Instantiate data model instance variable
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
            // Instantiate data model list variable
            List<TDataModel> dataModelInstanceList;
            
            // Check if file exists
            if (_fileIO.Exists(_jsonFilePath))
            {
                // Read in JSON file
                string jsonString = _fileIO.ReadAllText(_jsonFilePath);
                
                // Convert JSON string to C# object
                dataModelInstanceList = JsonConvert.DeserializeObject<List<TDataModel>>(value: jsonString);
            }
            // If file doesn't exist
            else
            {
                // No file found so return empty list that can hold data model objects
                Console.WriteLine($"No file found at {_jsonFilePath}, creating empty array for json objects.");
                return new List<TDataModel>();
            }
                
            // Validate property relationships
            foreach (TDataModel dataModelInstance in dataModelInstanceList)
            {
                dataModelInstance.ValidateDataModelPropertyRelationships();
            }
                
            // If file configuration is valid, return file configuration object
            return dataModelInstanceList;
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
            
            // Write json string to file
            _fileIO.WriteAllText(path: _jsonFilePath, contents: jsonString);
        }
    }
}