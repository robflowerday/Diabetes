using System;
using Diabetes.ExternalStorage.DataModels;


namespace Diabetes.ExternalStorage.DataModels
{
    public class EventData : IDataModel
    {
        public DateTime EventDateTime { get; set; }
        public int? Carbs { get; set; } = null;
        public int? ShortActingInsulinUnits { get; set; } = null;
        public int? LongActingInsulinUnits { get; set; } = null;
        public double? BloodGLucoseLevel { get; set; } = null;


        public void ValidateDataModelPropertyRelationships()
        {
            // throw new NotImplementedException();
        }
    }
}