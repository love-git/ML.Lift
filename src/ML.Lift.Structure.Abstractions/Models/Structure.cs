using System;

namespace ML.Lift.Structure.Abstractions.Models
{
    public class Structure
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
        //public virtual Floor[] Floors { get; set; }
        public virtual LineSet[] LineSets { get; set; }
    }
}
