using ML.Lift.CallBoxes.Abstractions.Engines;
using ML.Lift.CallBoxes.Abstractions.Models;
using System;

namespace ML.Lift.CallBoxes.Domain.Engines
{
    public class FancyCallBoxFactory : ICallBoxFactory
    {
        #region ICallBoxFactory

        public virtual CallBoxType[] GetCallBoxTypes()
        {
            throw new NotImplementedException();
        }

        public virtual CallBox BuildCallBox(Guid id, DateTime lastModified, CreateCallBoxRequest request)
        {
            throw new NotImplementedException();
        }

        public virtual CallBox BuildCallBox(Guid id, DateTime lastModified, UpdateCallBoxRequest request)
        {
            throw new NotImplementedException();
        }

        public virtual CallBox BuildPartialCallBox(Guid id, DateTime lastModified, CallBox currentCallBox, UpdateCallBoxRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion ICallBoxFactory
    }
}
