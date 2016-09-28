using System;

namespace ML.Lift.Structure.Abstractions.Models
{
    public class Floor
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
        //public virtual LineSet[] LineSets { get; set; }
    }
}
