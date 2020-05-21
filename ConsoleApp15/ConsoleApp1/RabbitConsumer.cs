using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

busing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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
            channel.ExchangeDeclare("Alicja", ExchangeType.Fanout, false, false, null);
            channel.QueueDeclare("Alicja", true, false, false, null);
            channel.QueueBind("Alicja", "Alicja", "", null);
            channel.ExchangeBind("Alicja", ConnectionConstants.OutgoingConvExchange, "ala_jan", null);
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
            };
            channel.BasicConsume(queue: "Alicja", autoAck: true, consumer: consumer);
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
