using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.Common.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Repositories
{
    public class CallBoxRepository : ICallBoxRepository
    {
        public CallBoxRepository()
        {

        }

        #region ICallBoxRepository

        public virtual async Task<GetCallBoxResponse> GetCallBoxAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GetCallBoxesResponse> GetCallBoxesAsync(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GetAllCallBoxesResponse> GetAllCallBoxesAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        #endregion ICallBoxRepository

        #region IPingable

        public virtual string Ping(string data)
        {
            return data;
        }

        #endregion IPingable

        #region ICheckable

        public virtual Task<HealthCheckResponse> HealthCheckAsync()
        {
            throw new NotImplementedException();
        }

        #endregion ICheckable
    }
}
