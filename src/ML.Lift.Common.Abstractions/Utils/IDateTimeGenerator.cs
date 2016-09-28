using System;

namespace ML.Lift.Common.Abstractions.Utils
{
    public interface IDateTimeGenerator
    {
        DateTime Now();
        DateTime UtcNow();
        DateTime UnixEpoch { get; }
    }
}
