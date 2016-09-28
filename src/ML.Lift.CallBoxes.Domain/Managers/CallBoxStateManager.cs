using Microsoft.Extensions.Logging;
using ML.Lift.CallBoxes.Abstractions.Managers;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.Common.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Domain.Managers
{
    public class CallBoxStateManager : ICallBoxStateManager
    {
        private readonly ILogger<CallBoxStateManager> _logger;

        public CallBoxStateManager(ILogger<CallBoxStateManager> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
        }

        #region ICallBoxStateManager

        public virtual async Task<UpResponse> UpAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<CancelUpResponse> CancelUpAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<DownResponse> DownAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<CancelDownResponse> CancelDownAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion ICallBoxStateManager

        #region IPingable

        public virtual string Ping(string data)
        {
            return data;
        }

        #endregion IPingable

        #region ICheckable

        public virtual async Task<HealthCheckResponse> HealthCheckAsync()
        {
            throw new NotImplementedException();
        }

        #endregion ICheckable
    }
}
