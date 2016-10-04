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
        
        //[Route("{id}")]
        //[HttpPost]
        //public virtual async Task<Ump.Accounts.Core.Models.CreateAccountResponse> Create(string id,
        //    [FromBody]Ump.Accounts.Core.Models.CreateAccountRequest request)
        //{
        //    var result = await _processor.CreateAccountAsync(id, request);
        //    return result;
        //}

        //[Route("{id}")]
        //[HttpPut]
        //public virtual async Task<Ump.Accounts.Core.Models.UpdateAccountResponse> Update(string id,
        //    [FromBody]Ump.Accounts.Core.Models.UpdateAccountRequest request)
        //{
        //    var result = await _processor.UpdateAccountAsync(id, request);
        //    return result;
        //}

        //[Route("{id}")]
        //[HttpPatch]
        //public virtual async Task<Ump.Accounts.Core.Models.UpdateAccountResponse> PartialUpdate(string id,
        //    [FromBody]Ump.Accounts.Core.Models.UpdateAccountRequest request)
        //{
        //    // This is a simple but limited partial update.  It will just
        //    // compare against default values of the properties to detect which properties
        //    // it needs to upate.  The user will not be able to set the properties to
        //    // the default values in other words.

        //    // The JsonPatchDocument would be better, but will need to come as an update.
        //    // It will change the api to include an "op" telling the api what to do, like remove or something.
        //    var result = await _processor.PartialUpdateAccountAsync(id, request);
        //    return result;
        //}

        //[Route("{id}")]
        //[HttpGet]
        //public virtual async Task<Ump.Accounts.Core.Models.GetAccountResponse> GetAccount(string id)
        //{
        //    var result = await _processor.GetAccountAsync(id);
        //    return result;
        //}

        //[Route("many")]
        //[HttpGet]
        //public virtual async Task<Ump.Accounts.Core.Models.GetAccountsResponse> GetAccounts([FromQuery]string[] ids)
        //{
        //    var result = await _processor.GetAccountsAsync(ids);
        //    return result;
        //}

        //[HttpGet]
        //public virtual async Task<Ump.Accounts.Core.Models.GetAllAccountsResponse> GetAllAccounts(
        //    [FromQuery]int offset, [FromQuery]int limit)
        //{
        //    var result = await _processor.GetAllAccountsAsync(offset, limit);
        //    return result;
        //}

        //[Route("{id}")]
        //[HttpDelete]
        //public virtual async Task<Ump.Accounts.Core.Models.DeleteAccountResponse> Delete(string id)
        //{
        //    var result = await _processor.DeleteAccountAsync(id);
        //    return result;
        //}

        //[Route("[action]/{id}")]
        //[HttpDelete]
        //public virtual async Task<Ump.Accounts.Core.Models.PurgeAccountResponse> Purge(string id)
        //{
        //    var result = await _processor.PurgeAccountAsync(id);
        //    return result;
        //}

        //[Route("[action]/{data}")]
        //[HttpGet]
        //public virtual string Ping(string data)
        //{
        //    return _processor.Ping(data);
        //}

        //[Route("health-check")]
        //[HttpGet]
        //public virtual async Task<Ump.Common.Core.Models.HealthCheckResponse> HealthCheck()
        //{
        //    var result = await _processor.HealthCheckAsync();
        //    return result;
        //}
    }
}
