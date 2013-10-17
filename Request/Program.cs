using System;
using Magnum.Extensions;
using MassTransit;
using Messages;

namespace Request
{

 
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
                {
                    sbc.UseMsmq(c =>
                        {
                            c.UseMulticastSubscriptionClient();
                            c.VerifyMsmqConfiguration();
                        });                    
                    sbc.ReceiveFrom("msmq://localhost/test_queue");
            });

            Bus.Instance.PublishRequest(new BasicRequest(), x =>
            {
                x.Handle<BasicResponse>(message => Handle(message.Text));
                x.SetTimeout(30.Seconds());
            });
        }

        public static void Handle(string message)
        {
            Console.WriteLine(message);
        }
    }
}
