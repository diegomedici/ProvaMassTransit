
using System;
using System.ComponentModel;
using System.Windows.Forms;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using Messages;
using Container = StructureMap.Container;
using IContainer = StructureMap.IContainer;

namespace Responder
{
    internal static class ResponderProgram
    {
         [STAThread]
        static void Main(string[] args)
        {
            //Console.WriteLine("I'm the Responder");

            Log4NetLogger.Use("responder.log4net.xml");
            IContainer c = BootstrapContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OrderForm(c.GetInstance<IServiceBus>()));

        }


        static IContainer BootstrapContainer()
        {
            var container = new Container(x => { x.AddType(typeof(OrderForm)); });

            container.Configure(cfg =>
            {
                cfg.For<IServiceBus>().Use(context => ServiceBusFactory.New(sbc =>
                {
                    sbc.ReceiveFrom("msmq://localhost/starbucks_customer");
                    sbc.UseMsmq();
                    sbc.UseMulticastSubscriptionClient();

                    sbc.UseControlBus();

                    sbc.Subscribe(subs => { subs.LoadFrom(container); });
                }));
            });

            return container;
        }


    }
}
