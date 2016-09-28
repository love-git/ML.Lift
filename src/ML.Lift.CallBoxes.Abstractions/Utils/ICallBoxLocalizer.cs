using ML.Lift.CallBoxes.Abstractions.Models;

namespace ML.Lift.CallBoxes.Abstractions.Utils
{
    public interface ICallBoxLocalizer
    {
        string LocalizePhrase(PhraseCode code);
        string LocalizeValidationCodePhrase(ValidationCode code);
    }
}
