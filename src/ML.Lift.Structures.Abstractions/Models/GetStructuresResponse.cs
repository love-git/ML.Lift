namespace ML.Lift.Structures.Abstractions.Models
{
    public class GetStructuresResponse
    {
        public virtual GetMultipleCode Code { get; set; }
        public virtual Structure[] Structures { get; set; }
    }
}
