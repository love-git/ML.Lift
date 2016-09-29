using ML.Lift.Common.Abstractions.Utils;
using ML.Lift.Structures.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.Structures.Abstractions.Managers
{
    public interface IStructureManager : IPingable, ICheckable
    {
        Task<CreateStructureResponse> CreateStructureAsync(CreateStructureRequest request);

        Task<UpdateStructureResponse> UpdateStructureAsync(Guid id, UpdateStructureRequest request);

        Task<UpdateStructureResponse> PartialUpdateStructureAsync(Guid id, UpdateStructureRequest request);

        Task<GetStructureResponse> GetStructureAsync(Guid id);

        Task<GetStructuresResponse> GetStructuresAsync(Guid[] ids);

        Task<GetAllStructuresResponse> GetAllStructuresAsync(int offset, int limit);

        Task<DeleteStructureResponse> DeleteStructureAsync(Guid id);

        Task<PurgeStructureResponse> PurgeStructureAsync(Guid id);
    }
}
