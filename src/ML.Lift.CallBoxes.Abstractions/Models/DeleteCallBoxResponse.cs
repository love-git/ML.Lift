using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public class DeleteCallBoxResponse
    {
        public virtual DeleteCode Code { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
