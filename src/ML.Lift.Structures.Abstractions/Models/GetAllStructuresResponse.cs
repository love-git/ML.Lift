namespace ML.Lift.Structures.Abstractions.Models
{
    public class GetAllStructuresResponse
    {
        public virtual GetAllCode Code { get; set; }
        public virtual Structure[] Structures { get; set; }
    }
}
