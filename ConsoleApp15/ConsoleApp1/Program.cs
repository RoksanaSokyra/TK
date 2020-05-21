using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitConsumer consumer = new RabbitConsumer();
            consumer.Connect();
            consumer.ConsumeOutgoingMessages();
            Console.ReadKey();
        }
    }
}
