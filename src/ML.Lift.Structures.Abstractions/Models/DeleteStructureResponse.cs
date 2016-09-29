using System;

namespace ML.Lift.Structures.Abstractions.Models
{
    public class DeleteStructureResponse
    {
        public virtual DeleteCode Code { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
