using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Models.Messages;
using System;

namespace ML.Lift.CallBoxes.Abstractions.Engines
{
    public interface ICallBoxMessageFactory
    {
        CallBoxMessage BuildCreateMessage(CallBox callBox);
        CallBoxMessage BuildUpdateMessage(CallBox callBox);
        CallBoxMessage BuildGetMessage(CallBox callBox, DateTime getTime);
        CallBoxMessage[] BuildGetMessages(CallBox[] callBoxes, DateTime getTime);
        CallBoxMessage BuildDeleteMessage(Guid id, DateTime deleteTime);
        CallBoxMessage BuildPurgeMessage(Guid id, DateTime purgeTime);
    }
}
