using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Magnum;
using MassTransit;
using Messages;

namespace Responder
{
    public partial class OrderForm : Form,
        Consumes<OrderInsertedMessage>.For<Guid>,
        Consumes<OrderCanceledMessage>.For<Guid>
    {

        public static int _orderId = 0;
        private IServiceBus _bus;
        private Guid _transactionId;
        private UnsubscribeAction _unsubscribeToken;


        private IServiceBus Bus
        {
            get { return _bus; }
        }

        public OrderForm(IServiceBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _transactionId = CombGuid.Generate();

            if (_unsubscribeToken != null)
                _unsubscribeToken();
            _unsubscribeToken = Bus.SubscribeInstance(this);

            var message = new NewOrderMessage
            {
                CorrelationId = _transactionId,
                OrderId = ++_orderId,
                OrderType = "Ordine diretto"
            };
            //Console.WriteLine("Prima del publish!");
            Bus.Publish(message, x => x.SetResponseAddress(Bus.Endpoint.Address.Uri));
        }

        public void Consume(OrderInsertedMessage message)
        {
            MessageBox.Show(string.Format("Hey, your order {0} is inserted.", message.OrderId), "Order Inserted", MessageBoxButtons.OK);
            //Console.WriteLine(string.Format("Hey, your order {0} is inserted.", message.OrderId));

            _unsubscribeToken();
            _unsubscribeToken = null;

            //try
            //{
            //    pulsanteAnnulla.Enabled = true;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //    throw;
            //}
            
        }

        public Guid CorrelationId
        {
            get { return _transactionId; }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (_unsubscribeToken != null)
            {
                _unsubscribeToken();
                _unsubscribeToken = null;
            }
            if (_bus != null)
            {
                _bus.Dispose();
                _bus = null;
            }

            base.OnClosing(e);
        }

        private void pulsanteAnnulla_Click(object sender, EventArgs e)
        {
            _transactionId = CombGuid.Generate();

            if (_unsubscribeToken != null)
                _unsubscribeToken();
            _unsubscribeToken = Bus.SubscribeInstance(this);

            Guid guid = new Guid(textGuidOrder.Text);

            AnnulloOrdineMessage annullo = new AnnulloOrdineMessage
                                               {
                                                   CorrelationId = guid,
                                                   Token = _transactionId,
                                                   Motivazione = "Non lo voglio più"
                                               };
            Bus.Publish(annullo, x => x.SetResponseAddress(Bus.Endpoint.Address.Uri));
        }

        public void Consume(OrderCanceledMessage message)
        {
            MessageBox.Show(string.Format("Hey, your order {0} is inserted has been canceled on date {1}.", message.CorrelationId, message.DataCancellazione), "Order Inserted", MessageBoxButtons.OK);

            _unsubscribeToken();
            _unsubscribeToken = null;

        }
    }
}
