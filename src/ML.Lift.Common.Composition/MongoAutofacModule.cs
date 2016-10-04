using Autofac;
using MongoDB.Driver;

namespace ML.Lift.Common.Composition
{
    public class MongoAutofacModule : Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var client = new MongoClient(ConnectionString);

            builder.Register(c => client).As<IMongoClient>().SingleInstance();
        }
    }
}
