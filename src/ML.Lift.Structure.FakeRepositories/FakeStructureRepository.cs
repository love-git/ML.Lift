using ML.Lift.Common.Abstractions.Models;
using ML.Lift.Common.Abstractions.Utils;
using ML.Lift.Structure.Abstractions.Constants;
using ML.Lift.Structure.Abstractions.Models;
using ML.Lift.Structure.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ML.Lift.Structure.Repositories
{
    public class FakeStructureRepository : IStructureRepository
    {
        private readonly IList<Abstractions.Models.Structure> _structures;
             
        public FakeStructureRepository()
        {
            _structures = new List<Abstractions.Models.Structure>();
            //_structures.Add(new Abstractions.Models.Structure
            //{
            //    Description = "Awesome Skyscraper",
            //    Id = TestIds.AwesomeSkyscraperId,
            //    LineSets = new[]
            //    {
            //        new LineSet
            //        {
            //            Description = "North Elevators",
            //            Id = TestIds.AwesomeNorthSetId,

            //        }
            //    },
            //    Floors = new []
            //    {
            //        new Floor
            //        {
            //            Description = "Ground Floor",
            //            Id = TestIds.AwesomeFloor00Id,
            //            LineSets = null
            //        }
            //    }
            //});
        }

        #region IStructureRepository

        public virtual async Task<GetStructureResponse> GetStructureAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GetStructuresResponse> GetStructuresAsync(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<GetAllStructuresResponse> GetAllStructuresAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        #endregion IStructureRepository

        #region IPingable

        public virtual string Ping(string data)
        {
            return data;
        }

        #endregion IPingable

        #region ICheckable

        public virtual async Task<HealthCheckResponse> HealthCheckAsync()
        {
            var result = new HealthCheckResponse
            {
                ApiVersion = "fake version",
                Description = "fake description",
                Code = HealthCheckCode.Ok,
                Dependencies = null
            };
            return await Task.FromResult(result);
        }

        #endregion ICheckable
    }
}
