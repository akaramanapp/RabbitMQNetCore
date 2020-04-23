using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQSample.Consumer
{
    class Program
    {


        static void Main(string[] args)
        {
            string url = "" // rmq.cloudamqp.com user and password
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(url.Replace("amqp://", "amqps://"));

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Customer",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    Console.WriteLine(" [x] Received {0}", ea.Body);
                };
                channel.BasicConsume(queue: "Customer",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
