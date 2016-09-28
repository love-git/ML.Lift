using ML.Lift.Common.Abstractions.Utils;
using ML.Lift.Structure.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.Structure.Abstractions.Repositories
{
    public interface IStructureRepository : IPingable, ICheckable
    {
        Task<GetStructureResponse> GetStructureAsync(Guid id);

        Task<GetStructuresResponse> GetStructuresAsync(Guid[] ids);

        Task<GetAllStructuresResponse> GetAllStructuresAsync(int offset, int limit);
    }
}
