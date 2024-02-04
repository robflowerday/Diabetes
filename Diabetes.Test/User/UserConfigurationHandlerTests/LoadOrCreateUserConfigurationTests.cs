using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Moq;
using NUnit.Framework;

using Diabetes.User;
using Diabetes.User.FileIO;


namespace Diabetes.Test.User.UserConfigurationHandlerTests
{
    [TestFixture]
    public class LoadOrCreateUserConfigurationTests
    {
        private UserConfigurationHandler _userConfigurationHandler;
        private Mock<IFileIO> _fileIOMock;

        [SetUp]
        public void Setup()
        {
            _fileIOMock = new Mock<IFileIO>();
            _userConfigurationHandler = new UserConfigurationHandler("fakePath.json", _fileIOMock.Object);
        }
        
        [Test]
        public void LoadOrCreateUserConfiguration_FileExistsValidIncompleteConfigObject_ReturnsDeserializedObject()
        {
            // Arrange
            // Set up Mock objects return values
            _fileIOMock.Setup(f => f.Exists("fakePath.json")).Returns(true);
            _fileIOMock.Setup(f => f.ReadAllText("fakePath.json")).Returns(@"{
                ""InsulinSensitivityFactor"": 2.0,
                ""CarbToInsulinRatio"": 15.0,
                ""LongActingInsulinDoesRecommendation"": 25
            }");
            
            // Act
            UserConfiguration userConfiguration = _userConfigurationHandler.LoadOrCreateUserConfiguration();
            
            // Assert
            Assert.IsNotNull(anObject: userConfiguration);
            Assert.AreEqual(expected: 2.0, actual: userConfiguration.InsulinSensitivityFactor);
            Assert.AreEqual(expected: 15.0, actual: userConfiguration.CarbToInsulinRatio);
            Assert.AreEqual(expected: 25, actual: userConfiguration.LongActingInsulinDoesRecommendation);
            Assert.AreEqual(expected: 4.0, actual: userConfiguration.TargetIsolationHours);
            Assert.AreEqual(expected: 0.0, actual: userConfiguration.MinIsolationHours);
            Assert.AreEqual(expected: 0.0, actual: userConfiguration.MaxIsolationHours);
            Assert.AreEqual(expected: new TimeSpan(20, 0, 0), actual: userConfiguration.OvernightStartTime);
            Assert.AreEqual(expected: new TimeSpan(6, 0, 0), actual: userConfiguration.OvernightEndTime);
            Assert.AreEqual(expected: 7.5, actual: userConfiguration.MinHoursOvernightWithoutAction);
            Assert.AreEqual(expected: 8.5, actual: userConfiguration.MaxHoursOvernightWithoutAction);
            // Assert.AreEqual(expected: 0, actual: existingFileErrors.Count);
            // Assert.AreEqual(expected: 0, actual: defaultFileErrors.Count);
        }
        
        [Test]
        public void LoadOrCreateUserConfiguration_FileExistsValidCompleteConfigObject_ReturnsDeserializedObject()
        {
            // Arrange
            // Set up Mock objects return values
            _fileIOMock.Setup(f => f.Exists("fakePath.json")).Returns(true);
            _fileIOMock.Setup(f => f.ReadAllText("fakePath.json")).Returns(@"{
                ""InsulinSensitivityFactor"": 2.0,
                ""CarbToInsulinRatio"": 15.0,
                ""LongActingInsulinDoesRecommendation"": 25,
                ""TargetIsolationHours"": 6.7,
                ""MinIsolationHours"": 4.2,
                ""MaxIsolationHours"": 8.0,
                ""OvernightStartTime"": ""19:30:12"",
                ""OvernightEndTime"": ""21:40:30"",
                ""MinHoursOvernightWithoutAction"": 6.9,
                ""MaxHoursOvernightWithoutAction"": 9.9   
            }");
            
            // Act
            UserConfiguration userConfiguration = _userConfigurationHandler.LoadOrCreateUserConfiguration();
            
            // Assert
            Assert.IsNotNull(anObject: userConfiguration);
            Assert.AreEqual(expected: 2.0, actual: userConfiguration.InsulinSensitivityFactor);
            Assert.AreEqual(expected: 15.0, actual: userConfiguration.CarbToInsulinRatio);
            Assert.AreEqual(expected: 25, actual: userConfiguration.LongActingInsulinDoesRecommendation);
            Assert.AreEqual(expected: 6.7, actual: userConfiguration.TargetIsolationHours);
            Assert.AreEqual(expected: 4.2, actual: userConfiguration.MinIsolationHours);
            Assert.AreEqual(expected: 5.9, actual: userConfiguration.MaxIsolationHours);
            Assert.AreEqual(expected: new TimeSpan(19, 30, 12), actual: userConfiguration.OvernightStartTime);
            Assert.AreEqual(expected: new TimeSpan(21, 40, 30), actual: userConfiguration.OvernightEndTime);
            Assert.AreEqual(expected: 6.9, actual: userConfiguration.MinHoursOvernightWithoutAction);
            Assert.AreEqual(expected: 9.9, actual: userConfiguration.MaxHoursOvernightWithoutAction);
            // Assert.AreEqual(expected: 0, actual: existingFileErrors.Count);
            // Assert.AreEqual(expected: 0, actual: defaultFileErrors.Count);
        }

        [Test]
        [TestCase(@"{""InsulinSensitivityFactor"": -2.0}")]
        [TestCase(@"{""CarbToInsulinRatio"": -2.0}")]
        [TestCase(@"{""LongActingInsulinDoesRecommendation"": -2}")]
        [TestCase(@"{""TargetIsolationHours"": -2.0}")]
        [TestCase(@"{""MinIsolationHours"": -2.0}")]
        [TestCase(@"{""MaxIsolationHours"": -2.0}")]
        public void LoadOrCreateUserConfiguration_FileExistsInvalidNegativeNumber_ThrowsError(
            string jsonString)
        {
            // Arrange
            // Set up Mock objects return values
            _fileIOMock.Setup(f => f.Exists("fakePath.json")).Returns(true);
            _fileIOMock.Setup(f => f.ReadAllText("fakePath.json")).Returns(jsonString);
            
            // Act + Assert
            Assert.Throws<ArgumentException>(() => _userConfigurationHandler.LoadOrCreateUserConfiguration());
        }

        [Test]
        [TestCase(@"{""InsulinSensitivityFactor"": ""invalid-string-value""}")]
        [TestCase(@"{""CarbToInsulinRatio"": false}")]
        [TestCase(@"{""LongActingInsulinDoesRecommendation"": 2.1}")]
        public void LoadOrCreateUserConfiguration_FileExistsInvalidType_ThrowsError(
            string jsonString)
        {
            // Arrange
            // Set up Mock objects return values
            _fileIOMock.Setup(f => f.Exists("fakePath.json")).Returns(true);
            _fileIOMock.Setup(f => f.ReadAllText("fakePath.json")).Returns(jsonString);
            
            // Act + Assert
            Assert.Throws<JsonReaderException>(() => _userConfigurationHandler.LoadOrCreateUserConfiguration());
        }
    }
}