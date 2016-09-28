using System;

namespace ML.Lift.CallBoxes.Abstractions.Engines
{
    public interface ICallBoxIdGenerator
    {
        Guid NewId();
    }
}
