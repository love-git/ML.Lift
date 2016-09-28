using ML.Lift.CallBoxes.Abstractions.Constants;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Utils;
using System.Collections.Generic;

namespace ML.Lift.CallBoxes.Utils
{
    public class CallBoxLocalizer : ICallBoxLocalizer
    {
        private readonly IDictionary<PhraseCode, string> _phraseDictionary;

        public CallBoxLocalizer()
        {
            _phraseDictionary = new Dictionary<PhraseCode, string>
            {
                { PhraseCode.InvalidValidator, Localization.InvalidValidatorText},
                { PhraseCode.InvalidDateTimeGenerator, Localization.InvalidDateTimeGeneratorText},
                { PhraseCode.InvalidCallBoxFactories, Localization.InvalidCallBoxFactoriesText},
                { PhraseCode.MultipleCallBoxFactoriesForSameCallBoxType, Localization.MultipleCallBoxFactoriesForSameCallBoxTypeText},
                { PhraseCode.InvalidMessageFactory, Localization.InvalidMessageFactoryText},
                { PhraseCode.InvalidPublisher, Localization.InvalidPublisherText},
                { PhraseCode.InvalidIdGenerator, Localization.InvalidIdGeneratorText},
                { PhraseCode.InvalidAdminRepository, Localization.InvalidAdminRepositoryText},
                { PhraseCode.InvalidCallBoxType, Localization.InvalidCallBoxTypeText},
                { PhraseCode.Unknown, Localization.UnknownText},
                { PhraseCode.ValidationCode_BadCreateCallBoxRequest, Localization.BadCreateCallBoxRequest},
                { PhraseCode.ValidationCode_Valid, Localization.Valid},
                { PhraseCode.Success, Localization.SuccessText}
            };
        }

        #region ICallBoxLocalizer

        public virtual string LocalizePhrase(PhraseCode code)
        {
            if (!_phraseDictionary.ContainsKey(code))
            {
                return _phraseDictionary[PhraseCode.Unknown];
            }
            else
            {
                return _phraseDictionary[code];
            }
        }
        
        public virtual string LocalizeValidationCodePhrase(ValidationCode code)
        {
            switch(code)
            {
                case ValidationCode.BadCreateCallBoxRequest:
                {
                    return _phraseDictionary[PhraseCode.ValidationCode_BadCreateCallBoxRequest];
                }
                default:
                case ValidationCode.Valid:
                {
                    return _phraseDictionary[PhraseCode.ValidationCode_Valid];
                }
            }
        }

        #endregion ICallBoxLocalizer
    }
}
