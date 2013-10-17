using System;
using MassTransit;
using Messages;

namespace Response
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq( c =>
                    {
                        c.VerifyMsmqConfiguration();
                        c.UseMulticastSubscriptionClient();
                    });
                sbc.ReceiveFrom("msmq://localhost/test_queue");
                sbc.Subscribe(subs =>
                {
                    subs.Handler<BasicRequest>((cxt, msg) => cxt.Respond(new BasicResponse { Text = "RESP" + msg.Text }));
                });
            });
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
