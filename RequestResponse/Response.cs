using System;
using MassTransit;
using Messages;

namespace RequestResponse
{
    public class Response
    {
        public Response()
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq(c =>
                {
                    c.VerifyMsmqConfiguration();
                    c.UseMulticastSubscriptionClient();
                });
                sbc.ReceiveFrom("msmq://localhost/resp_queue");
                sbc.Subscribe(subs => subs.Handler<BasicRequest>((cxt, msg) =>
                    {
                        Console.WriteLine("Id of req: {0}", msg.CorrelationId);
                        cxt.Respond(new BasicResponse {CorrelationId = msg.CorrelationId, Text = "RESP " + msg.Text});
                        cxt.Respond(new BasicResponse() /*{CorrelationId = msg.CorrelationId, Text = "RESP " + msg.Text}*/);
                    }));
            });
        }
    }
}