using System;

namespace ML.Lift.Structures.Abstractions.Models
{
    public class Structure
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
        public virtual LineSet[] LineSets { get; set; }
    }
}
