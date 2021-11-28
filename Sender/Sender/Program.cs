using RabbitMQ.Client;
using System;
using System.Text;

namespace Sender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(
                "amqp://guest:guest@localhost:5672"
                )
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("demo-queue", false, false, false, null);

            var message = "Hello Akash!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", "demo-queue", null, body);

        }
    }
}
