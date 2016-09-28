using ML.Lift.CallBoxes.Abstractions.Engines;
using System;

namespace ML.Lift.CallBoxes.Domain.Engines
{
    public class CallBoxIdGenerator : ICallBoxIdGenerator
    {
        #region ICallBoxIdGenerator

        public virtual Guid NewId()
        {
            return Guid.NewGuid();
        }

        #endregion ICallBoxIdGenerator
    }
}
