using ML.Lift.Structures.Abstractions.Models;
using System;

namespace ML.Lift.Structures.Abstractions.Utils
{
    public interface IStructureValidator
    {
        ValidationCode ValidateCreateRequest(CreateStructureRequest request);
        ValidationCode ValidateUpdateRequest(UpdateStructureRequest request);
        ValidationCode ValidateId(Guid id);
        ValidationCode ValidateIds(Guid[] ids);
        ValidationCode ValidateOffset(int offset);
        ValidationCode ValidateLimit(int limit);
    }
}
