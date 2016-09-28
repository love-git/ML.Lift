using Microsoft.Extensions.Logging;

namespace ML.Lift.CallBoxes.Abstractions.Constants
{
    public static class EventIds
    {
        public static EventId CreateCallBox = new EventId(1, "CreateCallBox");
        public static EventId UpdateCallBox = new EventId(2, "UpdateCallBox");
        public static EventId PartialUpdateCallBox = new EventId(3, "PartialUpdateCallBox");
        public static EventId GetCallBox = new EventId(4, "GetCallBox");
        public static EventId GetCallBoxes = new EventId(5, "GetCallBoxes");
        public static EventId GetAllCallBoxes = new EventId(6, "GetAllCallBoxes");
        public static EventId DeleteCallBox = new EventId(7, "DeleteCallBox");
        public static EventId PurgeCallBox = new EventId(8, "PurgeCallBox");
        public static EventId HealthCheck = new EventId(9, "HealthCheck");
    }
}
