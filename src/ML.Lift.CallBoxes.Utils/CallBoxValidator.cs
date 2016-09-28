using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Utils;
using System;

namespace ML.Lift.CallBoxes.Utils
{
    public class CallBoxValidator : ICallBoxValidator
    {
        #region ICallBoxValidator

        public virtual ValidationCode ValidateCreateCallBoxRequest(CreateCallBoxRequest request)
        {
            if (request == null)
            {
                return ValidationCode.BadCreateCallBoxRequest;
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidateCallBoxId(Guid id)
        {
            if (id == Guid.Empty)
            {
                return ValidationCode.BadId;
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidateCallBoxIds(Guid[] ids)
        {
            if (ids == null)
            {
                return ValidationCode.BadIds;
            }
            foreach (var id in ids)
            {
                var validationResult = ValidateCallBoxId(id);
                if (validationResult != ValidationCode.Valid)
                {
                    return validationResult;
                }
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidateUpdateCallBoxRequest(UpdateCallBoxRequest request)
        {
            if (request == null)
            {
                return ValidationCode.BadUpdateCallBoxRequest;
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidatePartialUpdateCallBoxRequest(UpdateCallBoxRequest request)
        {
            if (request == null)
            {
                return ValidationCode.BadPartialUpdateCallBoxRequest;
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidateOffset(int offset)
        {
            if (offset < 0)
            {
                return ValidationCode.BadOffset;
            }
            return ValidationCode.Valid;
        }

        public virtual ValidationCode ValidateLimit(int limit)
        {
            if (limit <= 0)
            {
                return ValidationCode.BadLimit;
            }
            return ValidationCode.Valid;
        }

        #endregion ICallBoxValidator
    }
}
