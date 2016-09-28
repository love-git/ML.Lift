namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class GetCallBoxResponse
    {
        public virtual GetCallBoxCode Code { get; set; }

        public virtual string Description { get; set; }

        public virtual CallBox CallBox { get; set; }
    }
}
