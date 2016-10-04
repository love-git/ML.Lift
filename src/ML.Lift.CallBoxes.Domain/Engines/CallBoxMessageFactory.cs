using ML.Lift.CallBoxes.Abstractions.Engines;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Models.Messages;
using System;

namespace ML.Lift.CallBoxes.Domain.Engines
{
    public class CallBoxMessageFactory : ICallBoxMessageFactory
    {
        #region ICallBoxMessageFactory

        public virtual CallBoxMessage BuildCreateMessage(CallBox callBox, DateTime createTime)
        {
            throw new NotImplementedException();
        }

        public virtual CallBoxMessage BuildUpdateMessage(CallBox callBox, DateTime updateTime)
        {
            throw new NotImplementedException();
        }

        public virtual CallBoxMessage BuildGetMessage(CallBox callBox, DateTime getTime)
        {
            throw new NotImplementedException();
        }

        public virtual CallBoxMessage[] BuildGetMessages(CallBox[] callBoxes, DateTime getTime)
        {
            throw new NotImplementedException();
        }

        public virtual CallBoxMessage BuildDeleteMessage(Guid id, DateTime deleteTime)
        {
            throw new NotImplementedException();
        }

        public virtual CallBoxMessage BuildPurgeMessage(Guid id, DateTime purgeTime)
        {
            throw new NotImplementedException();
        }

        #endregion ICallBoxMessageFactory
    }
}
