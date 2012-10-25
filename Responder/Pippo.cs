using System;
using Magnum;
using MassTransit;
using Requester;

namespace Responder
{
    public class Pippo :
        Consumes<OrderInsertedMessage>.For<Guid>
    {

        private IServiceBus _bus;
        private Guid _transactionId;
        private UnsubscribeAction _unsubscribeToken;


        private IServiceBus Bus
        {
            get { return _bus; }
        }

        public Pippo(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Run()
        {
            Console.WriteLine("Sono nel Run!");
            _transactionId = CombGuid.Generate();

            if (_unsubscribeToken != null)
                _unsubscribeToken();
            _unsubscribeToken = Bus.SubscribeInstance(this);

            var message = new NewOrderMessage
                              {
                                  CorrelationId = _transactionId,
                                  OrderId = 1,
                                  OrderType = "Ordine diretto"
                              };
            Console.WriteLine("Prima del publish!");
            Bus.Publish(message, x => x.SetResponseAddress(Bus.Endpoint.Address.Uri));
        }

        public void Consume(OrderInsertedMessage message)
        {
            Console.WriteLine(string.Format("Hey, your order {0} is inserted.", message.OrderId));

            _unsubscribeToken();
            _unsubscribeToken = null;
        }

        public Guid CorrelationId
        {
            get { return _transactionId; }
        }

    }
}