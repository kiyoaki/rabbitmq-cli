using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitMQ.CLI
{
    internal static class Commands
    {
        internal static int Send(SendOptions options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
                AuthMechanisms = ParseAuthMechanismFactory(options.AuthMechanisms),
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                VirtualHost = options.VirtualHost
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: options.QueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    if (options.Interactive)
                    {
                        while (true)
                        {
                            Console.Write("> ");

                            var input = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(input))
                            {
                                continue;
                            }

                            if (input == "exit")
                            {
                                break;
                            }

                            var body = Encoding.UTF8.GetBytes(input);

                            channel.BasicPublish(exchange: "",
                                routingKey: options.QueueName,
                                basicProperties: null,
                                body: body);

                            Console.WriteLine("Sent: {0}", input);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(options.Message))
                        {
                            Console.WriteLine("Message is required.");
                        }
                        else
                        {
                            var body = Encoding.UTF8.GetBytes(options.Message);

                            channel.BasicPublish(exchange: "",
                                routingKey: options.QueueName,
                                basicProperties: null,
                                body: body);

                            Console.WriteLine("Sent: {0}", options.Message);
                        }
                    }
                }
            }

            return 0;
        }

        internal static int Receive(ReceiveOptions options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
                AuthMechanisms = ParseAuthMechanismFactory(options.AuthMechanisms),
                Port = options.Port,
                UserName = options.UserName,
                Password = options.Password,
                VirtualHost = options.VirtualHost
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: options.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body);
                Console.WriteLine("Received: {0}", message);
            };
            channel.BasicConsume(queue: options.QueueName,
                autoAck: true,
                consumer: consumer);

            return 0;
        }

        static IList<AuthMechanismFactory> ParseAuthMechanismFactory(IEnumerable<string> authMechanisms)
        {
            var mechanisms = authMechanisms as string[] ?? authMechanisms.ToArray();
            if (authMechanisms == null || !mechanisms.Any())
            {
                return new AuthMechanismFactory[] { new PlainMechanismFactory() };
            }

            return mechanisms
                .Select(x =>
                {
                    switch (x)
                    {
                        case "EXTERNAL":
                            return (AuthMechanismFactory)new ExternalMechanismFactory();
                        default:
                            return new PlainMechanismFactory();
                    }

                })
                .ToList();
        }
    }
}
