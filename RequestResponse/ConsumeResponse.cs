using Magnum.Extensions;
using MassTransit;
using Messages;

namespace RequestResponse
{
    public class ConsumeResponse : Consumes<BasicResponse>.Selected
    {

        public ConsumeResponse()
        {
            var basicRequest = new BasicRequest { Text = "Ciaobbello" };
            Bus.Instance.PublishRequest(basicRequest, x =>
            {
                x.SetTimeout(30.Seconds());
            });  
        }
          

        public void Consume(BasicResponse message)
        {
            throw new System.NotImplementedException();
        }

        public bool Accept(BasicResponse message)
        {
            throw new System.NotImplementedException();
        }
    }
}