using System;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Saga;
using Messages;

namespace Requester
{
    public class OrderSaga : SagaStateMachine<OrderSaga>, ISaga
    {

        static OrderSaga()
        {
            Define(() =>
                       {
                           Initially(When(InserimentoOrdine)
                                         .Then((saga, message) => saga.OrdineInserito(message))
                                         .TransitionTo(Inserito));

                           During(Inserito,
                                  //When(AnnulloRichiedente).Then((saga, message) => saga.OrdineAnnullatoDalDestinatario(message)).TransitionTo(Annullato).Complete(),
                                  When(AnnulloSpedizione).Then((saga, message) => saga.SpedizioneAnnullataMittente(message)).TransitionTo(Annullato).Complete(),
                                  When(AcquisizioneOrdine).Then((saga, message) => saga.OrdineAcquisitoDalMittente(message)).TransitionTo(PresoInCarico));

                           During(PresoInCarico,
                                  When(AnnulloSpedizione).Then(
                                      (saga, message) => saga.SpedizioneAnnullataMittente(message)).TransitionTo(Annullato).Complete(),
                                  When(EmissioneBolla).Then((saga, message) => saga.OrdineSpedito(message)).TransitionTo(Inviato).Complete());
                       });
        }

        public OrderSaga(Guid correlationId)
		{
			CorrelationId = correlationId;
		}

        private void OrdineSpedito(EmissioneBollaMessage message)
        {
            throw new NotImplementedException();
        }


        private void OrdineAcquisitoDalMittente(AcquisizioneOrdineMessage message)
        {
            throw new NotImplementedException();
        }


        private void SpedizioneAnnullataMittente(AnnulloOrdineMessage message)
        {
            Console.WriteLine("Ordine Annullato {0} {1}", message.CorrelationId, message.Motivazione);
            OrderCanceledMessage orderCanceledMessage = new OrderCanceledMessage
                                               {
                                                   CorrelationId = message.Token,
                                                   DataCancellazione = DateTime.Now
                                               };

            Bus.Context().Respond(orderCanceledMessage);
            
        }


        //private void OrdineAnnullatoDalDestinatario(AnnulloOrdineMessage message)
        //{
        //    throw new NotImplementedException();
        //}

        private void OrdineInserito(NewOrderMessage message)
        {
            Console.WriteLine("OrdineInserito {0} {1} {2}", message.CorrelationId, message.OrderId, message.OrderType);
            var orderInsertedMessage = new OrderInsertedMessage
            {
                CorrelationId = message.CorrelationId,
                OrderId = message.OrderId,
            };
            Bus.Context().Respond(orderInsertedMessage);
        }


        //States
        public static State Initial { get; set; }
        public static State Completed { get; set; }
        
        //States
        public static State Inserito { get; set; }
        public static State Annullato { get; set; }
        public static State PresoInCarico { get; set; }
        public static State Inviato { get; set; }

        public static Event<NewOrderMessage> InserimentoOrdine { get; set; }
        public static Event<AnnulloOrdineMessage> AnnulloRichiedente { get; set; }
        public static Event<AnnulloOrdineMessage> AnnulloSpedizione { get; set; }
        public static Event<AcquisizioneOrdineMessage> AcquisizioneOrdine { get; set; }
        public static Event<EmissioneBollaMessage> EmissioneBolla { get; set; }

        public Guid CorrelationId { get; set; }

        public IServiceBus Bus { get; set; }
    }
}