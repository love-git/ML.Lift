﻿using System;

namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public abstract class CallBox
    {
        public virtual Guid Id { get; set; }
        public virtual CallBoxState CallBoxState { get; set; }
        public abstract CallBoxType CallBoxType { get; }
    }
}
