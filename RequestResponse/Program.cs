using System;
using Magnum.Extensions;
using MassTransit;


namespace RequestResponse
{
    class Program
    {
        static void Main(string[] args)
        {
            //var res = new Response();
            //var req = new Request();
            
            //Console.ReadLine();

            string url = @"rabbitmq://localhost/queue";
            IServiceBus requestorBus = ServiceBusFactory.New(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom(@"rabbitmq://localhost/req_queue");
                    sbc.Validate();
                    /*sbc.UseMsmq(c =>
                        {
                            c.UseMulticastSubscriptionClient();
                            c.Configurator.ReceiveFrom("msmq://localhost/req_queue");
                            c.VerifyMsmqConfiguration();
                        });
                    sbc.Validate();*/
                }

                );


            IServiceBus replierBus = ServiceBusFactory.New(sbc =>
                {

                    {
                        sbc.UseRabbitMq();
                        sbc.ReceiveFrom(@"rabbitmq://localhost/rep_queue");
                        sbc.Validate();
                    }
                    //sbc.UseMsmq(c =>
                    //    {
                    //        c.UseMulticastSubscriptionClient();
                    //        c.Configurator.ReceiveFrom("msmq://localhost/resp_queue");
                    //        c.VerifyMsmqConfiguration();

                    //    });
                    sbc.Subscribe(
                            sbs => sbs.Handler<Request>((ctx, msg) =>
                                {
                                    ctx.Respond(new Response { Text = "Hello, " + msg.Text });
                                    //ctx.Respond(new Response {Text = "Hello again"});
                                }));
                    sbc.Validate();
                });
            

            Console.WriteLine("Initialization is complete");
            requestorBus.PublishRequest(new Request { Text = "John" },
                                        callback =>
                                        {
                                            callback.Handle<Response>(message => Console.WriteLine(message.Text));
                                            callback.SetTimeout(60.Seconds());

                                        });

            
            
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();            
            Console.WriteLine("Disposing...");
            
            requestorBus.Dispose();
            replierBus.Dispose();
            Console.WriteLine("Disposed...");
        }

        public class Request
        {
            public string Text { get; set; }
        }

        public class Response
        {
            public string Text { get; set; }
        }
    }
}
