using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ML.Lift.CallBoxes.Abstractions.Constants;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.CallBoxes.Abstractions.Utils;
using ML.Lift.Common.Abstractions.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Repositories
{
    public class CallBoxRepository : MongoRepository, ICallBoxRepository
    {
        public CallBoxRepository(ILogger<MongoRepository> logger, ICallBoxLocalizer localizer,
            IMongoClient client, IOptions<MongoOptions> optionAccessor)
            : base(logger, localizer, client, optionAccessor)
        {

        }

        #region ICallBoxRepository

        public virtual async Task<GetCallBoxResponse> GetCallBoxAsync(Guid id)
        {
            try
            {
                // Note: Filter out deleted callBoxes.
                var builder = Builders<CallBox>.Filter;
                var filter = builder.Eq(a => a.Id, id) & builder.Eq(x => x.IsDeleted, false);
                var findOptions = new FindOptions<CallBox>
                {
                    Limit = 1
                };
                var callBoxes = new List<CallBox>();
                using (var cursor = await _collection.FindAsync(filter, findOptions))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        callBoxes.AddRange(cursor.Current);
                    }
                }

                var callBox = callBoxes.FirstOrDefault();
                if (callBox == null)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Missing);
                    _logger.LogInformation(EventIds.GetCallBox, errorText);
                    return new GetCallBoxResponse
                    {
                        Code = GetCallBoxCode.Missing,
                        Description = errorText,
                        CallBox = null
                    };
                }

                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetCallBox, successText);
                return new GetCallBoxResponse
                {
                    Code = GetCallBoxCode.Success,
                    Description = successText,
                    CallBox = callBox
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.GetCallBox, e, errorText);
                return new GetCallBoxResponse
                {
                    Code = GetCallBoxCode.Unknown,
                    Description = errorText,
                    CallBox = null
                };
            }
        }

        public virtual async Task<GetCallBoxesResponse> GetCallBoxesAsync(Guid[] ids)
        {
            try
            {
                // Note: Filter out deleted documents.
                var builder = Builders<CallBox>.Filter;
                var filter = builder.In(a => a.Id, ids) & builder.Eq(x => x.IsDeleted, false);
                var callBoxes = new List<CallBox>();
                using (var cursor = await _collection.FindAsync(filter))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        callBoxes.AddRange(cursor.Current);
                    }
                }

                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetCallBoxes, successText);
                return new GetCallBoxesResponse
                {
                    Code = GetCallBoxesCode.Success,
                    Description = successText,
                    CallBoxes = callBoxes.ToArray()
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.GetCallBoxes, e, errorText);
                return new GetCallBoxesResponse
                {
                    Code = GetCallBoxesCode.Unknown,
                    Description = errorText,
                    CallBoxes = new CallBox[0]
                };
            }
        }

        public virtual async Task<GetAllCallBoxesResponse> GetAllCallBoxesAsync(int offset, int limit)
        {
            try
            {
                // Note: Filter out deleted documents.
                var builder = Builders<CallBox>.Filter;
                var filter = builder.Eq(x => x.IsDeleted, false);
                var sort = Builders<CallBox>.Sort.Ascending(x => x.LastModified);
                var findOptions = new FindOptions<CallBox>
                {
                    Sort = sort,
                    Skip = offset,
                    Limit = limit
                };
                var callBoxes = new List<CallBox>();
                using (var cursor = await _collection.FindAsync(filter, findOptions))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        callBoxes.AddRange(cursor.Current);
                    }
                }

                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetAllCallBoxes, successText);
                return new GetAllCallBoxesResponse
                {
                    Code = GetAllCallBoxesCode.Success,
                    Description = successText,
                    CallBoxes = callBoxes.ToArray()
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.GetAllCallBoxes, e, errorText);
                return new GetAllCallBoxesResponse
                {
                    Code = GetAllCallBoxesCode.Unknown,
                    Description = errorText,
                    CallBoxes = new CallBox[0]
                };
            }
        }

        #endregion ICallBoxRepository

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
            try
            {
                // Ping the database.
                await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.HealthCheck, e, errorText);
            }
            return new HealthCheckResponse
            {
                Description = description,
                ApiVersion = apiVersion,
                Code = HealthCheckCode.Ok,
                Dependencies = new HealthCheckResponse[0]
            };
        }

        #endregion ICheckable
    }
}
