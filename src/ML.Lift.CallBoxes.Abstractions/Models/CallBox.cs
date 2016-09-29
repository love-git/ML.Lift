using System;

namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public abstract class CallBox
    {
        public virtual Guid Id { get; set; }
        public abstract CallBoxType CallBoxType { get; }
        public virtual CallBoxState CallBoxState { get; set; }        
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual Guid StructureId { get; set; }
        public virtual Guid LineSetId { get; set; }
        public virtual Guid FloorId { get; set; }
    }
}
