namespace ML.Lift.Structure.Abstractions.Models
{
    public class UpdateStructureRequest
    {
        public virtual string Description { get; set; }
        public virtual Floor[] Floors { get; set; }
        public virtual LineSet[] LineSets { get; set; }
    }
}
