using ML.Lift.CallBoxes.Abstractions.Models;
using System;

namespace ML.Lift.CallBoxes.Abstractions.Utils
{
    public interface ICallBoxValidator
    {
        ValidationCode ValidateCreateCallBoxRequest(CreateCallBoxRequest request);
        ValidationCode ValidateCallBoxId(Guid id);
        ValidationCode ValidateCallBoxIds(Guid[] ids);
        ValidationCode ValidateUpdateCallBoxRequest(UpdateCallBoxRequest request);
        ValidationCode ValidatePartialUpdateCallBoxRequest(UpdateCallBoxRequest request);
        ValidationCode ValidateOffset(int offset);
        ValidationCode ValidateLimit(int limit);
    }
}
