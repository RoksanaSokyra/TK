using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class RabbitConsumer
    {
        private IConnection connection;
        private IModel channel;

        public void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = ConnectionConstants.User,
                Password = ConnectionConstants.Password,
                VirtualHost = ConnectionConstants.VirtualHostName,
            HostName = ConnectionConstants.HostName
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(ConnectionConstants.OutgoingConvExchange, ExchangeType.Fanout, true, false, null);
            channel.ExchangeDeclare(ConnectionConstants.IncomingConvExchange, ExchangeType.Topic, true, false, null);
            channel.QueueDeclare(ConnectionConstants.MainQueue, true, false, false, null);
            channel.QueueBind(ConnectionConstants.MainQueue, ConnectionConstants.OutgoingConvExchange, "", null);
            Console.WriteLine("starting");
        }

        public void ConsumeOutgoingMessages()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine("received {0}", message);
                channel.BasicPublish(ConnectionConstants.IncomingConvExchange, "ala_janek", null, body);
            };
            channel.BasicConsume(queue: ConnectionConstants.MainQueue, autoAck: true, consumer: consumer);
        }

        public void Disconnect()
        {
            channel = null;
            if (connection.IsOpen)
            {
                connection.Close();
            }
            connection.Dispose();
            connection = null;
        }
    }
}
