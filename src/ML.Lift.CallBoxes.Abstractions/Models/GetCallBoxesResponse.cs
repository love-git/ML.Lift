namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class GetCallBoxesResponse
    {
        public virtual GetCallBoxesCode Code { get; set; }

        public virtual string Description { get; set; }

        public virtual CallBox[] CallBoxes { get; set; }
    }
}
