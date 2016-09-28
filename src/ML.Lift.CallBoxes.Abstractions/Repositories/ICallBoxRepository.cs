using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.Common.Abstractions.Utils;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Repositories
{
    public interface ICallBoxRepository: IPingable, ICheckable
    {
        Task<GetCallBoxResponse> GetCallBoxAsync(Guid id);
        Task<GetCallBoxesResponse> GetCallBoxesAsync(Guid[] ids);
        Task<GetAllCallBoxesResponse> GetAllCallBoxesAsync(int offset, int limit);
    }
}
