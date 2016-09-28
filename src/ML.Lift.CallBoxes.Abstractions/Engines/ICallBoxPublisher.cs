using ML.Lift.CallBoxes.Abstractions.Models.Messages;
using ML.Lift.Common.Abstractions.Utils;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Engines
{
    public interface ICallBoxPublisher : IPingable, ICheckable
    {
        Task PublishCallBoxMessageAsync(CallBoxMessage message);
        Task PublishCallBoxMessagesAsync(CallBoxMessage[] messages);
    }
}
