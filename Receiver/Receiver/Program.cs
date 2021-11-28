using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receiver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(
                "amqp://guest:guest@localhost:5672"
                )};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("demo-queue", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-queue", true, consumer);
            Console.ReadLine();
        }
    }
}
