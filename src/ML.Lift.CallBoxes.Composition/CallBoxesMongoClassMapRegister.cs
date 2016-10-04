using MongoDB.Bson.Serialization;
using ML.Lift.CallBoxes.Abstractions.Models;

namespace ML.Lift.CallBoxes.Composition
{
    public class CallBoxesMongoClassMapRegister
    {
        private static bool _isRegistered = false;

        public static void Register()
        {
            if (!_isRegistered)
            {
                // Mongo registration.
                BsonClassMap.RegisterClassMap<CallBox>(a =>
                {
                    a.AutoMap();
                    a.SetIsRootClass(true);
                });
                BsonClassMap.RegisterClassMap<SimulationCallBox>();
                BsonClassMap.RegisterClassMap<FancyCallBox>();
                _isRegistered = true;
            }
        }
    }
}
