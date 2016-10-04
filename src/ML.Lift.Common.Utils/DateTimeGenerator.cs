using ML.Lift.Common.Abstractions.Utils;
using System;

namespace ML.Lift.Common.Utils
{
    public class DateTimeGenerator : IDateTimeGenerator
    {
        private static DateTime _unixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                
        #region IDateTimeGenerator

        public virtual DateTime Now()
        {
            return DateTime.Now;
        }

        public virtual DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        public virtual DateTime UnixEpoch => _unixEpoch;

        #endregion IDateTimeGenerator
    }
}
