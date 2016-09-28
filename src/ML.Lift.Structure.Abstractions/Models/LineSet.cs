using System;

namespace ML.Lift.Structure.Abstractions.Models
{
    public class LineSet
    {
        public virtual Guid Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Line[] Lines { get; set; }
        public virtual Floor[] Floors { get; set; }
    }
}
