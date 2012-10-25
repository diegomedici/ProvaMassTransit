using System;
using MassTransit;

namespace Messages
{
    [Serializable]
    public class EmissioneBollaMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public string NumroBolla { get; set; }
    }
}