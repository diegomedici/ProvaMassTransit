using System;
using Magnum.Extensions;
using MassTransit;
using Messages;

namespace RequestResponse
{
    public class Request
    {
        public Request()
        {
            //Bus.Initialize(sbc =>
            //    {
            //        sbc.UseMsmq(c =>
            //            {
            //                c.UseMulticastSubscriptionClient();
            //                c.VerifyMsmqConfiguration();
            //            });                    
            //        sbc.ReceiveFrom("msmq://localhost/test_queue");
            //});

            Bus.Instance.PublishRequest(new BasicRequest {Text = "Ciaobbello"}, x =>
            {
                x.Handle<BasicResponse>(message => Handle(message.Text));
                x.SetTimeout(30.Seconds());
            });
        }

        public void Handle(string message)
        {
            Console.WriteLine(message);
        }
    }  
}