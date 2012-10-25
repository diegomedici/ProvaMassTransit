using MassTransit;
using MassTransit.Saga;
using Ninject.Modules;

namespace Requester
{
    public class OrderProcessorRegistry : NinjectModule
    {
        public override void Load()
        {
            Bind<ISagaRepository<OrderSaga>>()
                .To<InMemorySagaRepository<OrderSaga>>()
                .InSingletonScope();

            Bind<OrderService>()
                .To<OrderService>()
                .InSingletonScope();

            Bind<IServiceBus>().ToMethod(context =>
                                             {
                                                 return ServiceBusFactory.New(sbc =>
                                                                                  {
                                                                                      sbc.UseMsmq();
                                                                                      sbc.UseMulticastSubscriptionClient();
                                                                                      sbc.ReceiveFrom("msmq://localhost/starbucks_cashier");
                                                                                      sbc.SetConcurrentConsumerLimit(1); //a cashier cannot multi-task

                                                                                      sbc.UseControlBus();
                                                                                      sbc.EnableRemoteIntrospection();
                                                                                  });
                                             })
                .InSingletonScope();
        }
    }
}