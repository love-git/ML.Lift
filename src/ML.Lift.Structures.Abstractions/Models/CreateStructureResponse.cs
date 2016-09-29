using System;

namespace ML.Lift.Structures.Abstractions.Models
{
    public class CreateStructureResponse
    {
        public virtual CreateCode Code { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
