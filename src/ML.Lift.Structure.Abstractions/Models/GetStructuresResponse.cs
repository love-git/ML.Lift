namespace ML.Lift.Structure.Abstractions.Models
{
    public class GetStructuresResponse
    {
        public virtual GetMultipleCode Code { get; set; }
        public virtual Structure[] Structures { get; set; }
    }
}
