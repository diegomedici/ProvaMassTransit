using System;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Magnum;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using MassTransit.Saga;
using Topshelf;

namespace Requester
{

    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("I'm the order processor");
            Log4NetLogger.Use("orderprocessor.log4net.xml");
            
            var container = new WindsorContainer();
            container.Register(
                Component.For(typeof(ISagaRepository<>))
                    .ImplementedBy(typeof(InMemorySagaRepository<>))
                    .LifeStyle.Singleton,
                Component.For<OrderSaga>(),
                Component.For<OrderService>()
                    .LifeStyle.Singleton,
                Component.For<IServiceBus>().UsingFactoryMethod(() =>
                {
                    return ServiceBusFactory.New(sbc =>
                    {
                        //sbc.ReceiveFrom("msmq://localhost/webdpc_responder");
                        sbc.ReceiveFrom("msmq://localhost/webdpc_orderprocessor");
                        sbc.UseMsmq();
                        sbc.UseMulticastSubscriptionClient();

                        sbc.UseControlBus();

                        sbc.Subscribe(subs => { subs.LoadFrom(container); });
                    });
                }).LifeStyle.Singleton);

            HostFactory.Run(c =>
            {
                c.SetServiceName("OrderProcessor");
                c.SetDisplayName("Order Processor");
                c.SetDescription("WebDPC order processor.");

                c.RunAsLocalSystem();
                c.DependsOnMsmq();
                
                DisplayStateMachine();

                c.Service<OrderService>(s =>
                {
                    s.ConstructUsing(builder => container.Resolve<OrderService>());
                    s.WhenStarted(o => o.Start());
                    s.WhenStopped(o =>
                    {
                        o.Stop();
                        container.Dispose();
                    });
                });
            });

        }

        private static void DisplayStateMachine()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            StateMachineInspector.Trace(new OrderSaga(CombGuid.Generate()));
        }

    }

}
