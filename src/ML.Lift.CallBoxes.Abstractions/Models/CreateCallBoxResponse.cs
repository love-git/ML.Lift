using System;

namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class CreateCallBoxResponse
    {
        public virtual CreateCode Code { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
