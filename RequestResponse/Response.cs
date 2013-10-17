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
                sbc.ReceiveFrom("msmq://localhost/queue");
                sbc.Subscribe(subs =>
                {
                    subs.Handler<BasicRequest>((cxt, msg) => cxt.Respond(new BasicResponse { Text = "RESP " + msg.Text }));
                });
            });
        }
    }
}