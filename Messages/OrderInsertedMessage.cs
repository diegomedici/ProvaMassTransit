using System;
using MassTransit;

namespace Messages
{
    public class OrderInsertedMessage : CorrelatedBy<Guid>
    {
        public int OrderId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}