namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class SimulationCallBox : CallBox
    {
        public override CallBoxType CallBoxType => CallBoxType.Simulation;
        
        public virtual string Description { get; set; }
    }
}
