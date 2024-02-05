using Diabetes.ExternalStorage.DataModels;

namespace Diabetes.HelperFunctions.EventHelperFunctions
{
    public static class EventPropertyFilterFunctions
    {
        public static bool IsBloodGlucoseOnlyEvent(EventData eventData)
        {
            if (eventData.Carbs == null &&
                eventData.ShortActingInsulinUnits == null &&
                eventData.LongActingInsulinUnits == null)
            {
                return true;
            }

            return false;
        }
    }
}