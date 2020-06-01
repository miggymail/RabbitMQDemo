using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    System.Console.WriteLine("Send Message : ");
                    string message = Console.ReadLine();

                    while (!string.IsNullOrWhiteSpace(message))
                    {
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);

                        System.Console.WriteLine("\t[x] Sent {0}", message);

                        message = System.Console.ReadLine();
                    }
                }
            }
        }
    }
}
