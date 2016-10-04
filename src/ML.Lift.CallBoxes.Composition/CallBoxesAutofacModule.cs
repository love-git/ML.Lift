using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ML.Lift.CallBoxes.Abstractions.Engines;
using ML.Lift.CallBoxes.Abstractions.Managers;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Abstractions.Repositories;
using ML.Lift.CallBoxes.Abstractions.Utils;
using ML.Lift.CallBoxes.Domain.Engines;
using ML.Lift.CallBoxes.Domain.Managers;
using ML.Lift.CallBoxes.Repositories;
using ML.Lift.CallBoxes.Utils;
using ML.Lift.Common.Abstractions.Utils;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ML.Lift.CallBoxes.Composition
{
    public class CallBoxesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Autofac registration.
            builder.Register(c => new CallBoxManager(
                c.Resolve<ILogger<CallBoxManager>>(),
                c.Resolve<ICallBoxLocalizer>(),
                c.Resolve<ICallBoxValidator>(),
                c.Resolve<IDateTimeGenerator>(),
                c.Resolve<IEnumerable<ICallBoxFactory>>(),
                c.Resolve<ICallBoxMessageFactory>(),                
                c.Resolve<ICallBoxPublisher>(),
                c.Resolve<ICallBoxIdGenerator>(),
                c.Resolve<ICallBoxAdminRepository>())).As<ICallBoxManager>();

            builder.Register(c => new CallBoxLocalizer()).As<ICallBoxLocalizer>();

            builder.Register(c => new CallBoxValidator()).As<ICallBoxValidator>();

            builder.Register(c => new FancyCallBoxFactory()).As<ICallBoxFactory>();
            builder.Register(c => new SimulationCallBoxFactory()).As<ICallBoxFactory>();

            builder.Register(c => new CallBoxMessageFactory()).As<ICallBoxMessageFactory>();

            builder.Register(c => new CallBoxRabbitPublisher()).As<ICallBoxPublisher>();
            
            builder.Register(c => new CallBoxIdGenerator()).As<ICallBoxIdGenerator>();

            builder.Register(c => new CallBoxAdminRepository(
                c.Resolve<ILogger<MongoRepository>>(),
                c.Resolve<ICallBoxLocalizer>(),
                c.Resolve<IMongoClient>(),
                c.Resolve<IOptions<MongoOptions>>())).As<ICallBoxAdminRepository>();
            
            // IDateTimeGenerator registered in ML.Lift.Common.Compisition.

            // ILogger<CallBoxManager> needs to be registered somewhere.
            // ILogger<MongoRepository> needs to be registered somewhere.
            // IMongoClient needs to be registered somewhere.
            // IOptions<MongoOptions> needs to be registered somewhere.
            // IBus needs to be registered somewhere.
        }
    }
}
