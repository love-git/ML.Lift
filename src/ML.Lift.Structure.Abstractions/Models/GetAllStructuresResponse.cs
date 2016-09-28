namespace ML.Lift.Structure.Abstractions.Models
{
    public class GetAllStructuresResponse
    {
        public virtual GetAllCode Code { get; set; }
        public virtual Structure[] Structures { get; set; }
    }
}
