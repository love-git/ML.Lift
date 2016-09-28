using ML.Lift.CallBoxes.Abstractions.Models;
using System;

namespace ML.Lift.CallBoxes.Abstractions.Engines
{
    public interface ICallBoxFactory
    {
        CallBoxType[] GetCallBoxTypes();
        CallBox BuildCallBox(Guid id, DateTime lastModified, CreateCallBoxRequest request);
        CallBox BuildCallBox(Guid id, DateTime lastModified, UpdateCallBoxRequest request);
        CallBox BuildPartialCallBox(Guid id, DateTime lastModified, CallBox currentCallBox, UpdateCallBoxRequest request);
    }
}
