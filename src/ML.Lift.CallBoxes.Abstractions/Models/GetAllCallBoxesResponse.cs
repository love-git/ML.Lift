namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class GetAllCallBoxesResponse
    {
        public virtual GetAllCallBoxesCode Code { get; set; }
        public virtual string Description { get; set; }
        public virtual CallBox[] CallBoxes { get; set; }
    }
}
