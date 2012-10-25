using System;
using MassTransit;

namespace Messages
{
    public class OrderCanceledMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public DateTime DataCancellazione { get; set; }
    }
}