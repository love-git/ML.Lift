using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ML.Lift.CallBoxes.Abstractions.Managers;

namespace ML.Lift.CallBoxes.Api.v1.Controllers
{
    [Route("api/[controller]")]
    public class CallBoxesController : Controller
    {
        private readonly ICallBoxManager _manager;

        public CallBoxesController(ICallBoxManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            _manager = manager;
        }

        [Route("{id}")]
        [HttpPost]
        public virtual async Task<Abstractions.Models.CreateCallBoxResponse> Create(
            [FromBody]Abstractions.Models.CreateCallBoxRequest request)
        {
            var result = await _manager.CreateCallBoxAsync(request);
            return result;
        }

        [Route("{id}")]
        [HttpPut]
        public virtual async Task<Abstractions.Models.UpdateCallBoxResponse> Update(Guid id,
            [FromBody]Abstractions.Models.UpdateCallBoxRequest request)
        {
            var result = await _manager.UpdateCallBoxAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpPatch]
        public virtual async Task<Abstractions.Models.UpdateCallBoxResponse> PartialUpdate(Guid id,
            [FromBody]Abstractions.Models.UpdateCallBoxRequest request)
        {
            // This is a simple but limited partial update.  It will just
            // compare against default values of the properties to detect which properties
            // it needs to upate.  The user will not be able to set the properties to
            // the default values in other words.

            // The JsonPatchDocument would be better, but will need to come as an update.
            // It will change the api to include an "op" telling the api what to do, like remove or something.
            var result = await _manager.PartialUpdateCallBoxAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpGet]
        public virtual async Task<Abstractions.Models.GetCallBoxResponse> GetCallBox(Guid id)
        {
            var result = await _manager.GetCallBoxAsync(id);
            return result;
        }

        [Route("many")]
        [HttpGet]
        public virtual async Task<Abstractions.Models.GetCallBoxesResponse> GetCallBoxes([FromQuery]Guid[] ids)
        {
            var result = await _manager.GetCallBoxesAsync(ids);
            return result;
        }

        [HttpGet]
        public virtual async Task<Abstractions.Models.GetAllCallBoxesResponse> GetAllCallBoxes(
            [FromQuery]int offset, [FromQuery]int limit)
        {
            var result = await _manager.GetAllCallBoxesAsync(offset, limit);
            return result;
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual async Task<Abstractions.Models.DeleteCallBoxResponse> Delete(Guid id)
        {
            var result = await _manager.DeleteCallBoxAsync(id);
            return result;
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public virtual async Task<Abstractions.Models.PurgeCallBoxResponse> Purge(Guid id)
        {
            var result = await _manager.PurgeCallBoxAsync(id);
            return result;
        }

        [Route("[action]/{data}")]
        [HttpGet]
        public virtual string Ping(string data)
        {
            return _manager.Ping(data);
        }

        [Route("health-check")]
        [HttpGet]
        public virtual async Task<Common.Abstractions.Models.HealthCheckResponse> HealthCheck()
        {
            var result = await _manager.HealthCheckAsync();
            return result;
        }
    }
}
