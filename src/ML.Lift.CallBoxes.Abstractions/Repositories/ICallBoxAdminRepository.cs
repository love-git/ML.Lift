using ML.Lift.CallBoxes.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Repositories
{
    public interface ICallBoxAdminRepository : ICallBoxRepository
    {
        Task<CreateCode> CreateCallBoxAsync(CallBox newCallBox);
        Task<UpdateCode> UpdateCallBoxAsync(Guid id, CallBox callBox);
        Task<DeleteCode> DeleteCallBoxAsync(Guid id, DateTime lastModified);
        Task<PurgeCode> PurgeCallBoxAsync(Guid id);
    }
}
