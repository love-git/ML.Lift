using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.Common.Abstractions.Utils;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Managers
{
    public interface ICallBoxStateManager : IPingable, ICheckable
    {
        Task<UpResponse> UpAsync(Guid id);

        Task<CancelUpResponse> CancelUpAsync(Guid id);

        Task<DownResponse> DownAsync(Guid id);

        Task<CancelDownResponse> CancelDownAsync(Guid id);
    }
}
