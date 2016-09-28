using ML.Lift.Structure.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace ML.Lift.Structure.Abstractions.Repositories
{
    public interface IStructureAdminRepository : IStructureRepository
    {
        Task<CreateCode> CreateStructureAsync(Models.Structure structure);

        Task<UpdateCode> UpdateStructureAsync(Guid id, Models.Structure structure);

        Task<DeleteCode> DeleteStructureAsync(Guid id, DateTime lastModified);

        Task<PurgeCode> PurgeStructureAsync(Guid id);
    }
}
