namespace ML.Lift.Common.Abstractions.Models
{
    public class HealthCheckResponse
    {
        public virtual string Description { get; set; }

        public virtual string ApiVersion { get; set; }

        public virtual HealthCheckCode Code { get; set; }

        public virtual HealthCheckResponse[] Dependencies { get; set; }
    }
}
