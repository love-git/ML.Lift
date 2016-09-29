using ML.Lift.Common.Abstractions.Models;
using ML.Lift.Structures.Abstractions.Models;
using ML.Lift.Structures.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace ML.Lift.Structures.Repositories
{
    public class FakeStructureRepository : IStructureRepository
    {
        private const int _structureCount = 10;
        private const int _lineSetCount = 5;
        private const int _lineCount = 6;
        private const int _floorCount = 50;
        private readonly IList<Structure> _structures;
             
        public FakeStructureRepository()
        {
            _structures = new List<Structure>();
            for (var structureIndex = 1; structureIndex <= _structureCount; structureIndex++)
            {
                var s = new Structure();
                s.Description = string.Format("Structure_{0}", structureIndex);
                s.Id = Guid.Parse(string.Format("{0,8:D8}-0000-0000-0000-000000000000", structureIndex));

                var lineSets = new List<LineSet>();
                for (var lineSetIndex = 1; lineSetIndex <= _lineSetCount; lineSetIndex++)
                {
                    var ls = new LineSet();
                    ls.Description = string.Format("LineSet_{0}_{1}", structureIndex, lineSetIndex);
                    ls.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-0000-0000-000000000000", structureIndex, lineSetIndex));
                    var lines = new List<Line>();
                    for (var lineIndex = 1; lineIndex <= _lineCount; lineIndex++)
                    {
                        var l = new Line();
                        l.Description = string.Format("Line_{0}_{1}_{2}", structureIndex, lineSetIndex, lineIndex);
                        l.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-{2,4:D4}-0000-000000000000", structureIndex, lineSetIndex, lineIndex));
                        lines.Add(l);
                    }
                    ls.Lines = lines.ToArray();
                    var floors = new List<Floor>();
                    for (var floorIndex = 1; floorIndex <= _floorCount; floorIndex++)
                    {
                        var f = new Floor();
                        f.Description = string.Format("Floor_{0}_{1}_{2}", structureIndex, lineSetIndex, floorIndex);
                        f.Id = Guid.Parse(string.Format("{0,8:D8}-{1,4:D4}-0000-{2,4:D4}-000000000000", structureIndex, lineSetIndex, floorIndex));
                        floors.Add(f);
                    }
                    ls.Floors = floors.ToArray();
                    lineSets.Add(ls);
                }
                s.LineSets = lineSets.ToArray();
                _structures.Add(s);
            }
        }

        #region IStructureRepository

        public virtual async Task<GetStructureResponse> GetStructureAsync(Guid id)
        {
            var structure = _structures.FirstOrDefault(x => x.Id == id);
            GetStructureResponse result;
            if (structure == null)
            {
                result = new GetStructureResponse
                {
                    Code = GetCode.Missing,
                    Structure = null
                };
            }
            else
            {
                result = new GetStructureResponse
                {
                    Code = GetCode.Success,
                    Structure = structure
                };
            }
            return await Task.FromResult(result);
        }

        public virtual async Task<GetStructuresResponse> GetStructuresAsync(Guid[] ids)
        {
            var structures = _structures.Where(x => ids.Contains(x.Id)).ToArray();
            var result = new GetStructuresResponse
            {
                Code = GetMultipleCode.Success,
                Structures = structures
            };
            return await Task.FromResult(result);
        }

        public virtual async Task<GetAllStructuresResponse> GetAllStructuresAsync(int offset, int limit)
        {
            var structures = _structures.OrderByDescending(x => x.Description).Skip(offset).Take(limit).ToArray();
            var result = new GetAllStructuresResponse
            {
                Code = GetAllCode.Success,
                Structures = structures
            };
            return await Task.FromResult(result);
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
            var typeInfo = this.GetType().GetTypeInfo();
            var description = typeInfo.FullName;
            var apiVersion = typeInfo.Assembly.GetName().Version.ToString();
            var result = new HealthCheckResponse
            {
                Description = "fake description",
                ApiVersion = apiVersion,
                Code = HealthCheckCode.Ok,
                Dependencies = new HealthCheckResponse[0]
            };
            return await Task.FromResult(result);
        }

        #endregion ICheckable
    }
}
