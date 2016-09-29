using Microsoft.Extensions.Logging;
using ML.Lift.CallBoxes.Abstractions.Constants;
using ML.Lift.CallBoxes.Abstractions.Engines;
using ML.Lift.CallBoxes.Abstractions.Managers;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.CallBoxes.Abstractions.Utils;
using ML.Lift.Common.Abstractions.Models;
using ML.Lift.Common.Abstractions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Domain.Managers
{
    public class CallBoxManager : ICallBoxManager
    {
        private readonly ILogger<CallBoxManager> _logger;
        private readonly ICallBoxLocalizer _localizer;
        private readonly ICallBoxValidator _validator;
        private readonly IDateTimeGenerator _dateTimeGenerator;
        private readonly IDictionary<CallBoxType, ICallBoxFactory> _callBoxFactories;
        private readonly ICallBoxMessageFactory _messageFactory;
        private readonly ICallBoxPublisher _publisher;
        private readonly ICallBoxIdGenerator _idGenerator;
        private readonly ICallBoxAdminRepository _adminRepository;
        
        public CallBoxManager(ILogger<CallBoxManager> logger, ICallBoxLocalizer localizer, ICallBoxValidator validator,
            IDateTimeGenerator dateTimeGenerator, IEnumerable<ICallBoxFactory> callBoxFactories, ICallBoxMessageFactory messageFactory,
            ICallBoxPublisher publisher, ICallBoxIdGenerator idGenerator, ICallBoxAdminRepository adminRepository)
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
            if (validator == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidValidator);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(validator), errorText);
            }
            _validator = validator;
            if (dateTimeGenerator == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidDateTimeGenerator);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(dateTimeGenerator), errorText);
            }
            _dateTimeGenerator = dateTimeGenerator;
            if (callBoxFactories == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidCallBoxFactories);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(callBoxFactories), errorText);
            }
            _callBoxFactories = new Dictionary<CallBoxType, ICallBoxFactory>();
            foreach (var callBoxFactory in callBoxFactories)
            {
                var callBoxTypes = callBoxFactory.GetCallBoxTypes();
                foreach (var callBoxType in callBoxTypes)
                {
                    if (!_callBoxFactories.ContainsKey(callBoxType))
                    {
                        _callBoxFactories.Add(callBoxType, callBoxFactory);
                    }
                    else
                    {
                        var errorText = _localizer.LocalizePhrase(PhraseCode.MultipleCallBoxFactoriesForSameCallBoxType);
                        _logger.LogError(errorText);
                        throw new ArgumentException(nameof(callBoxFactories), errorText);
                    }
                }
            }
            if (messageFactory == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidMessageFactory);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(messageFactory));
            }
            _messageFactory = messageFactory;
            if (publisher == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidPublisher);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(publisher), errorText);
            }
            _publisher = publisher;
            if (idGenerator == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidIdGenerator);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(idGenerator), errorText);
            }
            _idGenerator = idGenerator;
            if (adminRepository == null)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidAdminRepository);
                _logger.LogError(errorText);
                throw new ArgumentNullException(nameof(adminRepository), errorText);
            }
            _adminRepository = adminRepository;
        }

        #region ICallBoxManager

        public virtual async Task<CreateCallBoxResponse> CreateCallBoxAsync(CreateCallBoxRequest request)
        {
            try
            {
                var validateRequestResult = _validator.ValidateCreateCallBoxRequest(request);
                if (validateRequestResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateRequestResult);
                    _logger.LogError(EventIds.CreateCallBox, errorText);
                    return new CreateCallBoxResponse
                    {
                        Code = CreateCode.BadRequest,
                        Description = errorText,
                        LastModified = null
                    };
                }
                
                if (!_callBoxFactories.Keys.Contains(request.CallBoxType))
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidCallBoxType);
                    _logger.LogError(EventIds.CreateCallBox, errorText);
                    return new CreateCallBoxResponse
                    {
                        Code = CreateCode.BadCallBoxType,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the new Id;
                var id = _idGenerator.NewId();

                // Create the lastModified in UTC.
                var lastModified = _dateTimeGenerator.UtcNow();

                // Create the new callBox.
                var newCallBox = _callBoxFactories[request.CallBoxType].BuildCallBox(id, lastModified, request);

                // Save the new callBox.
                var createResult = await _adminRepository.CreateCallBoxAsync(newCallBox);
                if (createResult == CreateCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.CreateCallBox, errorText);
                    return new CreateCallBoxResponse
                    {
                        Code = createResult,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the message.
                var message = _messageFactory.BuildCreateMessage(newCallBox, lastModified);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.CreateCallBox, successText);
                return new CreateCallBoxResponse
                {
                    Code = CreateCode.Success,
                    Description = successText,
                    LastModified = lastModified
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.CreateCallBox, e, errorText);
                return new CreateCallBoxResponse
                {
                    Code = CreateCode.Unknown,
                    Description = errorText,
                    LastModified = null
                };
            }
        }

        public virtual async Task<UpdateCallBoxResponse> UpdateCallBoxAsync(Guid id, UpdateCallBoxRequest request)
        {
            try
            {
                var validateIdResult = _validator.ValidateCallBoxId(id);
                if (validateIdResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdResult);
                    _logger.LogError(EventIds.UpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadId,
                        Description = errorText,
                        LastModified = null
                    };
                }

                var validateRequestResult = _validator.ValidateUpdateCallBoxRequest(request);
                if (validateRequestResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateRequestResult);
                    _logger.LogError(EventIds.UpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadRequest,
                        Description = errorText,
                        LastModified = null
                    };
                }

                if (!_callBoxFactories.Keys.Contains(request.CallBoxType))
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidCallBoxType);
                    _logger.LogError(EventIds.UpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadCallBoxType,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the datetime in UTC.
                var lastModified = _dateTimeGenerator.UtcNow();

                // Create the new callBox.
                var callBox = _callBoxFactories[request.CallBoxType].BuildCallBox(id, lastModified, request);

                // Save the callBox.
                var updateResult = await _adminRepository.UpdateCallBoxAsync(id, callBox);
                if (updateResult == UpdateCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.UpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = updateResult,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the message.
                var message = _messageFactory.BuildUpdateMessage(callBox, lastModified);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.UpdateCallBox, successText);
                return new UpdateCallBoxResponse
                {
                    Code = updateResult,
                    Description = successText,
                    LastModified = (updateResult == UpdateCode.Success) ? lastModified : (DateTime?)null
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.UpdateCallBox, e, errorText);
                return new UpdateCallBoxResponse
                {
                    Code = UpdateCode.Unknown,
                    Description = errorText,
                    LastModified = null
                };
            }
        }

        public virtual async Task<UpdateCallBoxResponse> PartialUpdateCallBoxAsync(Guid id, UpdateCallBoxRequest request)
        {
            try
            {
                var validateIdResult = _validator.ValidateCallBoxId(id);
                if (validateIdResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdResult);
                    _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadId,
                        Description = errorText,
                        LastModified = null
                    };
                }

                var validateRequestResult = _validator.ValidatePartialUpdateCallBoxRequest(request);
                if (validateRequestResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateRequestResult);
                    _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadRequest,
                        Description = errorText,
                        LastModified = null
                    };
                }

                if (!_callBoxFactories.Keys.Contains(request.CallBoxType))
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.InvalidCallBoxType);
                    _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = UpdateCode.BadCallBoxType,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the datetime in UTC.
                var lastModified = _dateTimeGenerator.UtcNow();

                // Get the current callBox.
                var currentCallBox = await _adminRepository.GetCallBoxAsync(id);
                switch (currentCallBox.Code)
                {
                    case GetCallBoxCode.BadId:
                    {
                        var errorText = _localizer.LocalizeValidationCodePhrase(ValidationCode.BadId);
                        _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                        return new UpdateCallBoxResponse
                        {
                            Code = UpdateCode.BadId,
                            Description = errorText,
                            LastModified = null
                        };
                    }
                    case GetCallBoxCode.Missing:
                    {
                        var errorText = _localizer.LocalizePhrase(PhraseCode.Missing);
                        _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                        return new UpdateCallBoxResponse
                        {
                            Code = UpdateCode.Missing,
                            Description = errorText,
                            LastModified = null
                        };
                    }
                    case GetCallBoxCode.Unknown:
                    {
                        var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                        _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                        return new UpdateCallBoxResponse
                        {
                            Code = UpdateCode.Unknown,
                            Description = errorText,
                            LastModified = null
                        };
                    }
                }

                // Create the new callBox.
                var callBox = _callBoxFactories[request.CallBoxType].BuildPartialCallBox(id, lastModified, currentCallBox.CallBox, request);

                // Save the new callBox.
                var updateResult = await _adminRepository.UpdateCallBoxAsync(id, callBox);
                if (updateResult == UpdateCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.PartialUpdateCallBox, errorText);
                    return new UpdateCallBoxResponse
                    {
                        Code = updateResult,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the message.
                var message = _messageFactory.BuildUpdateMessage(callBox, lastModified);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.PartialUpdateCallBox, successText);
                return new UpdateCallBoxResponse
                {
                    Code = updateResult,
                    Description = successText,
                    LastModified = (updateResult == UpdateCode.Success) ? lastModified : (DateTime?)null
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.PartialUpdateCallBox, e, errorText);
                return new UpdateCallBoxResponse
                {
                    Code = UpdateCode.Unknown,
                    Description = errorText,
                    LastModified = null
                };
            }
        }

        public virtual async Task<GetCallBoxResponse> GetCallBoxAsync(Guid id)
        {
            try
            {
                var validateIdResult = _validator.ValidateCallBoxId(id);
                if (validateIdResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdResult);
                    _logger.LogError(EventIds.GetCallBox, errorText);
                    return new GetCallBoxResponse
                    {
                        Code = GetCallBoxCode.BadId,
                        Description = errorText,
                        CallBox = null
                    };
                }

                // Read the callBox.
                // Note: currentCallBox.CallBox may be null if the repo did not find a match.
                var currentCallBox = await _adminRepository.GetCallBoxAsync(id);
                if (currentCallBox.Code == GetCallBoxCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.GetCallBox, errorText);
                    return new GetCallBoxResponse
                    {
                        Code = currentCallBox.Code,
                        Description = errorText,
                        CallBox = null
                    };
                }

                // Create the datetime in UTC.
                var getTime = _dateTimeGenerator.UtcNow();

                // Create the message.
                // Note: the message factory needs to deal with a potential null CallBox.
                var message = _messageFactory.BuildGetMessage(currentCallBox.CallBox, getTime);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetCallBox, successText);
                return new GetCallBoxResponse
                {
                    Code = currentCallBox.Code,
                    Description = currentCallBox.Description,
                    CallBox = currentCallBox.CallBox
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
                var validateIdsResult = _validator.ValidateCallBoxIds(ids);
                if (validateIdsResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdsResult);
                    _logger.LogError(EventIds.GetCallBoxes, errorText);
                    return new GetCallBoxesResponse
                    {
                        Code = GetCallBoxesCode.BadIds,
                        Description = errorText,
                        CallBoxes = new CallBox[0]
                    };
                }

                // Read the callBoxes.
                // Note: currentCallBoxes may not have all of the callBoxes asked for.
                // depending on if the repository finds them or not.
                var currentCallBoxes = await _adminRepository.GetCallBoxesAsync(ids);
                if (currentCallBoxes.Code == GetCallBoxesCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.GetCallBoxes, errorText);
                    return new GetCallBoxesResponse
                    {
                        Code = GetCallBoxesCode.Unknown,
                        Description = errorText,
                        CallBoxes = new CallBox[0]
                    };
                }

                // Create the datetime in UTC.
                var getTime = _dateTimeGenerator.UtcNow();

                // Create the messages.
                var messages = _messageFactory.BuildGetMessages(currentCallBoxes.CallBoxes, getTime);

                // Publish the messages.
                await _publisher.PublishCallBoxMessagesAsync(messages);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetCallBoxes, successText);
                return new GetCallBoxesResponse
                {
                    Code = GetCallBoxesCode.Success,
                    Description = successText,
                    CallBoxes = currentCallBoxes.CallBoxes
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
                var validateOffsetResult = _validator.ValidateOffset(offset);
                if (validateOffsetResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateOffsetResult);
                    _logger.LogError(EventIds.GetAllCallBoxes, errorText);
                    return new GetAllCallBoxesResponse
                    {
                        Code = GetAllCallBoxesCode.BadOffset,
                        Description = errorText,
                        CallBoxes = new CallBox[0]
                    };
                }

                var validateLimitResult = _validator.ValidateLimit(limit);
                if (validateLimitResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateLimitResult);
                    _logger.LogError(EventIds.GetAllCallBoxes, errorText);
                    return new GetAllCallBoxesResponse
                    {
                        Code = GetAllCallBoxesCode.BadLimit,
                        Description = errorText,
                        CallBoxes = new CallBox[0]
                    };
                }

                // Read the callBoxes.
                // Note: currentCallBoxes may not have all of the callBoxes asked for.
                // depending on if the repository finds them or not.
                var currentCallBoxes = await _adminRepository.GetAllCallBoxesAsync(offset, limit);
                if (currentCallBoxes.Code == GetAllCallBoxesCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.GetAllCallBoxes, errorText);
                    return new GetAllCallBoxesResponse
                    {
                        Code = currentCallBoxes.Code,
                        Description = errorText,
                        CallBoxes = new CallBox[0]
                    };
                }

                // Create the datetime in UTC.
                var getTime = _dateTimeGenerator.UtcNow();
                
                // Create the messages.
                var messages = _messageFactory.BuildGetMessages(currentCallBoxes.CallBoxes, getTime);

                // Publish the messages.
                await _publisher.PublishCallBoxMessagesAsync(messages);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.GetAllCallBoxes, successText);
                return new GetAllCallBoxesResponse
                {
                    Code = GetAllCallBoxesCode.Success,
                    Description = successText,
                    CallBoxes = currentCallBoxes.CallBoxes
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

        public virtual async Task<DeleteCallBoxResponse> DeleteCallBoxAsync(Guid id)
        {
            try
            {
                var validateIdResult = _validator.ValidateCallBoxId(id);
                if (validateIdResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdResult);
                    _logger.LogError(EventIds.DeleteCallBox, errorText);
                    return new DeleteCallBoxResponse
                    {
                        Code = DeleteCode.BadId,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the datetime in UTC.
                var lastModified = _dateTimeGenerator.UtcNow();

                // Delete stuff.
                var deleteResult = await _adminRepository.DeleteCallBoxAsync(id, lastModified);
                if (deleteResult == DeleteCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.DeleteCallBox, errorText);
                    return new DeleteCallBoxResponse
                    {
                        Code = DeleteCode.Unknown,
                        Description = errorText,
                        LastModified = null
                    };
                }

                // Create the datetime in UTC.
                var deleteTime = _dateTimeGenerator.UtcNow();

                // Create the message.
                var message = _messageFactory.BuildDeleteMessage(id, deleteTime);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.DeleteCallBox, successText);
                return new DeleteCallBoxResponse
                {
                    Code = deleteResult,
                    Description = successText,
                    LastModified = (deleteResult == DeleteCode.Success) ? lastModified : (DateTime?)null
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.DeleteCallBox, e, errorText);
                return new DeleteCallBoxResponse
                {
                    Code = DeleteCode.Unknown,
                    Description = errorText,
                    LastModified = null
                };
            }
        }

        public virtual async Task<PurgeCallBoxResponse> PurgeCallBoxAsync(Guid id)
        {
            try
            {
                var validateIdResult = _validator.ValidateCallBoxId(id);
                if (validateIdResult != ValidationCode.Valid)
                {
                    var errorText = _localizer.LocalizeValidationCodePhrase(validateIdResult);
                    _logger.LogError(EventIds.PurgeCallBox, errorText);
                    return new PurgeCallBoxResponse
                    {
                        Code = PurgeCode.BadId,
                        Description = errorText
                    };
                }
                
                // Purge Stuff.
                var purgeResult = await _adminRepository.PurgeCallBoxAsync(id);
                if (purgeResult == PurgeCode.Unknown)
                {
                    var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                    _logger.LogError(EventIds.PurgeCallBox, errorText);
                    return new PurgeCallBoxResponse
                    {
                        Code = PurgeCode.Unknown,
                        Description = errorText
                    };
                }

                // Create the datetime in UTC.
                var purgeTime = _dateTimeGenerator.UtcNow();

                // Create the message.
                var message = _messageFactory.BuildPurgeMessage(id, purgeTime);

                // Publish the message.
                await _publisher.PublishCallBoxMessageAsync(message);

                // Return the success.
                var successText = _localizer.LocalizePhrase(PhraseCode.Success);
                _logger.LogInformation(EventIds.PurgeCallBox, successText);
                return new PurgeCallBoxResponse
                {
                    Code = purgeResult,
                    Description = successText
                };
            }
            catch (Exception e)
            {
                var errorText = _localizer.LocalizePhrase(PhraseCode.Unknown);
                _logger.LogError(EventIds.PurgeCallBox, e, errorText);
                return new PurgeCallBoxResponse
                {
                    Code = PurgeCode.Unknown,
                    Description = errorText
                };
            }
        }

        #endregion ICallBoxManager

        #region IPingable

        public virtual string Ping(string data)
        {
            return data;
        }

        #endregion IPingable

        #region ICheckable

        public virtual async Task<HealthCheckResponse> HealthCheckAsync()
        {
            var tasks = new List<Task<HealthCheckResponse>>();
            var publisherTask = _publisher.HealthCheckAsync();
            tasks.Add(publisherTask);
            var adminRepositoryTask = _adminRepository.HealthCheckAsync();
            tasks.Add(adminRepositoryTask);            
            // Run all tasks at the same time.
            await Task.WhenAll(tasks);
            var description = typeof(CallBoxManager).FullName;
            var apiVersion = typeof(CallBoxManager).GetTypeInfo().Assembly.GetName().Version.ToString();
            var dependencies = tasks.Select(x => x.Result).ToArray();
            return new HealthCheckResponse
            {
                Description = description,
                ApiVersion = apiVersion,
                Code = HealthCheckCode.Ok,
                Dependencies = dependencies
            };
        }

        #endregion ICheckable
    }
}
