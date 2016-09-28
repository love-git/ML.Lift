using ML.Lift.CallBoxes.Abstractions.Models.Messages;
using ML.Lift.CallBoxes.Abstractions.Engines;
using ML.Lift.Common.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Domain.Engines
{
    public class CallBoxRabbitPublisher : ICallBoxPublisher
    {
        #region ICallBoxPublisher

        public virtual async Task PublishCallBoxMessageAsync(CallBoxMessage message)
        {
            throw new NotImplementedException();
        }

        public virtual async Task PublishCallBoxMessagesAsync(CallBoxMessage[] messages)
        {
            throw new NotImplementedException();
        }
        
        #endregion ICallBoxPublisher

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
