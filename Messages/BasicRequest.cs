using System;
using MassTransit;

namespace Messages
{
    public class BasicRequest :
        CorrelatedBy<Guid>
    {
        public BasicRequest()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; set; }
        public string Text { get; set; }
    }
}