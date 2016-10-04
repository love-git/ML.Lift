namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class FancyCallBox : CallBox
    {
        public override CallBoxType CallBoxType => CallBoxType.Fancy;

        public virtual bool IsFancy => true;
    }
}
