using CommandLine;
using System;

namespace RabbitMQ.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<SendOptions, ReceiveOptions>(args)
                .MapResult(
                    (SendOptions options) => Commands.Send(options),
                    (ReceiveOptions options) => Commands.Receive(options),
                    errs => 1);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
