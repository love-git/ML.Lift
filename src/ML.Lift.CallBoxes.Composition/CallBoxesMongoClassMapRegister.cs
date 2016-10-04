using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Composition
{
    public class CallBoxesMongoClassMapRegister
    {
        private static bool _isRegistered = false;

        public static void Register()
        {
            if (!_isRegistered)
            {
                //// Mongo registration.
                //BsonClassMap.RegisterClassMap<Account>(a =>
                //{
                //    a.AutoMap();
                //    a.SetIsRootClass(true);
                //});
                //BsonClassMap.RegisterClassMap<JetPayAccount>();
                //BsonClassMap.RegisterClassMap<ConvergeAccount>();
                //_isRegistered = true;
            }
        }
    }
}
