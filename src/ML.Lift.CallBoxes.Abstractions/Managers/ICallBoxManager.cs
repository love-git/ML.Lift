using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.Common.Abstractions.Utils;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Managers
{
    public interface ICallBoxManager : IPingable, ICheckable
    {
        Task<CreateCallBoxResponse> CreateCallBoxAsync(CreateCallBoxRequest request);

        Task<UpdateCallBoxResponse> UpdateCallBoxAsync(Guid id, UpdateCallBoxRequest request);

        Task<UpdateCallBoxResponse> PartialUpdateCallBoxAsync(Guid id, UpdateCallBoxRequest request);

        Task<GetCallBoxResponse> GetCallBoxAsync(Guid id);

        Task<GetCallBoxesResponse> GetCallBoxesAsync(Guid[] ids);

        Task<GetAllCallBoxesResponse> GetAllCallBoxesAsync(int offset, int limit);

        Task<DeleteCallBoxResponse> DeleteCallBoxAsync(Guid id);

        Task<PurgeCallBoxResponse> PurgeCallBoxAsync(Guid id);
    }
}
