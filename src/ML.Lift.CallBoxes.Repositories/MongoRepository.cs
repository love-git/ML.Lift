using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Utils;
using MongoDB.Driver;
using System;

namespace ML.Lift.CallBoxes.Repositories
{
    public abstract class MongoRepository
    {
        protected readonly ILogger<MongoRepository> _logger;
        protected readonly ICallBoxLocalizer _localizer;
        protected readonly IMongoClient _client;
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<CallBox> _collection;
        protected readonly MongoOptions _options;

        public MongoRepository(ILogger<MongoRepository> logger, ICallBoxLocalizer localizer, 
            IMongoClient client, IOptions<MongoOptions> optionAccessor)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
            if (localizer == null)
            {
                const string errorText = "invalid localizer";
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(localizer), errorText);
            }
            _localizer = localizer;            
            if (client == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMongoClient);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(client), errorText);
            }
            _client = client;
            if (optionAccessor == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMongoOptionAccessor);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(optionAccessor), errorText);
            }
            if (optionAccessor.Value == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMongoOptions);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(optionAccessor), errorText);
            }
            _options = optionAccessor.Value;
            var database = _client.GetDatabase(_options.Database);
            if (database == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMongoOptionsDatabase);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(optionAccessor), errorText);
            }
            _database = database;
            var collection = _database.GetCollection<CallBox>(_options.Collection);
            if (collection == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMongoOptionsCollection);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(optionAccessor), errorText);
            }
            _collection = collection;
        }
    }
}
