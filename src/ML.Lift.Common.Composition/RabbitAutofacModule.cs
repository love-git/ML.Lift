//using Autofac;
//using MassTransit;
//using System;

//namespace ML.Lift.Common.Composition
//{
//    public class RabbitAutofacModule : Module
//    {
//        public string RabbitMQConnectionString { get; set; }

//        public string RabbitMQUsername { get; set; }

//        public string RabbitMQPassword { get; set; }

//        protected override void Load(ContainerBuilder builder)
//        {
//            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
//            {
//                sbc.Host(new Uri(RabbitMQConnectionString), h =>
//                {
//                    h.Username(RabbitMQUsername);
//                    h.Password(RabbitMQPassword);
//                });
//            });

//            busControl.Start();

//            builder.Register(c => busControl).As<IBus>().SingleInstance().OnRelease(StopBusControl);
//        }

//        public void StopBusControl(IBusControl busControl)
//        {
//            busControl.Stop();
//        }
//    }
//}
