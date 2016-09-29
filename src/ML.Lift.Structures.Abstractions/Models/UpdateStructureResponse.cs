using System;

namespace ML.Lift.Structures.Abstractions.Models
{
    public class UpdateStructureResponse
    {
        public virtual UpdateCode Code { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
