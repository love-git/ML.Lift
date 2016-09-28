using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.Common.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Repositories
{
    public class CallBoxAdminRepository : CallBoxRepository, ICallBoxAdminRepository
    {
        public CallBoxAdminRepository()
        {

        }

        #region ICallBoxAdminRepository

        public virtual async Task<CreateCode> CreateCallBoxAsync(CallBox newCallBox)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<UpdateCode> UpdateCallBoxAsync(Guid id, CallBox callBox)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DeleteCode> DeleteCallBoxAsync(Guid id, DateTime lastModified)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PurgeCode> PurgeCallBoxAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion ICallBoxAdminRepository
    }
}
