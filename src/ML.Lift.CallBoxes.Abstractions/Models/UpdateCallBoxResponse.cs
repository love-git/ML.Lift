using System;

namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class UpdateCallBoxResponse
    {
        public virtual UpdateCode Code { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
