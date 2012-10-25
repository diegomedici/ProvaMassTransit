using MassTransit;
using MassTransit.Saga;

namespace Requester
{
    public class OrderService
    {
        private readonly IServiceBus _bus;

        public OrderService(IServiceBus bus)
		{
			_bus = bus;
		}

		public void Start()
		{
		}

		public void Stop()
		{
			_bus.Dispose();
		}
    }
}