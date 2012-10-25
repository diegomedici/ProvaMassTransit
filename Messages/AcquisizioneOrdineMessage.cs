using System;
using MassTransit;

namespace Messages
{
    [Serializable]
    public class AcquisizioneOrdineMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public string FileName { get; set; }
    }
}