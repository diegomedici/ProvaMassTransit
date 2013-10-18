using System;
using Magnum.Extensions;
using MassTransit;
using MassTransit.BusConfigurators;
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
            
            

            var basicRequest = new BasicRequest {Text = "Ciaobbello"};
            Console.WriteLine("Guid of req: {0}", basicRequest.CorrelationId);
            Bus.Instance.PublishRequest(basicRequest, x =>
            {
                x.Handle<BasicResponse>(message =>  Handle(message));
                x.SetTimeout(30.Seconds());
            });
        }

        public void Handle(BasicResponse message)
        {
            Console.WriteLine("Guid of response: {0}", message.CorrelationId);
            Console.WriteLine(message.Text);
        }
    }  
}