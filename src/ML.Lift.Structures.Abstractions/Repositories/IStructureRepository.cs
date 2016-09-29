using ML.Lift.Common.Abstractions.Utils;
using ML.Lift.Structures.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.Structures.Abstractions.Repositories
{
    public interface IStructureRepository : IPingable, ICheckable
    {
        Task<GetStructureResponse> GetStructureAsync(Guid id);

        Task<GetStructuresResponse> GetStructuresAsync(Guid[] ids);

        Task<GetAllStructuresResponse> GetAllStructuresAsync(int offset, int limit);
    }
}
