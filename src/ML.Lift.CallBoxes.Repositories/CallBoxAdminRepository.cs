using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ML.Lift.CallBoxes.Abstractions.Constants;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.CallBoxes.Abstractions.Utils;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Repositories
{
    public class CallBoxAdminRepository : CallBoxRepository, ICallBoxAdminRepository
    {
        public CallBoxAdminRepository(ILogger<MongoRepository> logger, ICallBoxLocalizer localizer,
            IMongoClient client, IOptions<MongoOptions> optionAccessor)
            : base(logger, localizer, client, optionAccessor)
        {

        }

        #region ICallBoxAdminRepository

        public virtual async Task<CreateCode> CreateCallBoxAsync(CallBox callBox)
        {
            try
            {
                await _collection.InsertOneAsync(callBox);
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.CreateCallBox, successText);
                return CreateCode.Success;
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.CreateCallBox, e, errorText);
                return CreateCode.Unknown;
            }
        }

        public virtual async Task<UpdateCode> UpdateCallBoxAsync(Guid id, CallBox callBox)
        {
            try
            {
                // Note: Filter out deleted documents.
                var builder = Builders<CallBox>.Filter;
                var filter = builder.Eq("Id", id) & builder.Eq(x => x.IsDeleted, false);
                var result = await _collection.ReplaceOneAsync(filter, callBox);
                // todo: validate the result, convert it to a known result.
                if (result.MatchedCount == 0)
                {
                    var missingText = _localizer.LocalizePhrase(PhraseCode.Missing);
                    _logger.LogInformation(EventIds.UpdateCallBox, missingText);
                    return UpdateCode.Missing;
                }
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.UpdateCallBox, successText);
                return UpdateCode.Success;
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.UpdateCallBox, e, errorText);
                return UpdateCode.Unknown;
            }
        }

        public virtual async Task<DeleteCode> DeleteCallBoxAsync(Guid id, DateTime lastModified)
        {
            try
            {
                var filter = Builders<CallBox>
                    .Filter
                    .Eq("Id", id);
                var update = Builders<CallBox>
                    .Update
                    .Set(x => x.LastModified, lastModified)
                    .Set(x => x.IsDeleted, true);
                var result = await _collection.UpdateOneAsync(filter, update);
                // todo: validate the result, convert it to a known result.
                if (result.MatchedCount == 0)
                {
                    var missingText = _localizer.LocalizePhrase(PhraseCode.Missing);
                    _logger.LogInformation(EventIds.DeleteCallBox, missingText);
                    return DeleteCode.Missing;
                }
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.DeleteCallBox, successText);
                return DeleteCode.Success;
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.DeleteCallBox, e, errorText);
                return DeleteCode.Unknown;
            }
        }

        public virtual async Task<PurgeCode> PurgeCallBoxAsync(Guid id)
        {
            try
            {
                var filter = Builders<CallBox>
                    .Filter
                    .Eq("Id", id);
                var result = await _collection.DeleteOneAsync(filter);
                // todo: validate the result, convert it to a known result.
                if (result.DeletedCount == 0)
                {
                    var missingText = _localizer.LocalizePhrase(PhraseCode.Missing);
                    _logger.LogInformation(EventIds.PurgeCallBox, missingText);
                    return PurgeCode.Missing;
                }
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.PurgeCallBox, successText);
                return PurgeCode.Success;
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.PurgeCallBox, e, errorText);
                return PurgeCode.Unknown;
            }
        }

        #endregion ICallBoxAdminRepository
    }
}
