using System;
using MassTransit;

namespace Messages
{
    [Serializable]
    public class AnnulloOrdineMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public string Motivazione { get; set; }
        public Guid Token { get; set; }
    }
}