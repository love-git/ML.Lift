using ML.Lift.Common.Abstractions.Models;
using System.Threading.Tasks;

namespace ML.Lift.Common.Abstractions.Utils
{
    public interface ICheckable
    {
        Task<HealthCheckResponse> HealthCheckAsync();
    }
}
