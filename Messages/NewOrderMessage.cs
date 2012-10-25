using System;
using MassTransit;

namespace Messages
{
    [Serializable]
    public class NewOrderMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public string OrderType { get; set; }
        //public string Name { get; set; }

        //public string Size { get; set; }
        //public string Item { get; set; }
    }
}