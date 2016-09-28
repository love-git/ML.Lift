namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class CreateSimulationCallBoxRequestcs : CreateCallBoxRequest
    {
        public override CallBoxType CallBoxType => CallBoxType.Simulation;
    }
}
