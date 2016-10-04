using Autofac;
using ML.Lift.Common.Abstractions.Utils;
using ML.Lift.Common.Utils;

namespace ML.Lift.Common.Composition
{
    public class CommonAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Autofac registration.
            builder.Register(c => new DateTimeGenerator()).As<IDateTimeGenerator>();
        }
    }
}
