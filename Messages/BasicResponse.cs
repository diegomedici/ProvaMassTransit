using System;
using MassTransit;

namespace Messages
{
    public class BasicResponse : CorrelatedBy<Guid>
    {

        public BasicResponse()
        {
            //CorrelationId = Guid.NewGuid();
        }
        
        public string Text { get; set; }
        public Guid CorrelationId { get; set; }
    }
}