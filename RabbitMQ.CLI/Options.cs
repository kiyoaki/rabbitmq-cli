using CommandLine;
using System.Collections.Generic;

namespace RabbitMQ.CLI
{
    internal abstract class OptionsBase
    {
        [Option('h', "host", Required = false, Default = "localhost", HelpText = "Input host name.")]
        public string HostName { get; set; }

        [Option(longName: "port", Required = false, Default = 5672, HelpText = "Input host port.")]
        public int Port { get; set; }

        [Option('a', "authmechanisms", Required = false, Default = new[] { "PLAIN" }, HelpText = "Input auth mechanisms.")]
        public IEnumerable<string> AuthMechanisms { get; set; }

        [Option('u', "user", Required = false, Default = "guest", HelpText = "Input auth user.")]
        public string UserName { get; set; }

        [Option(longName: "password", Required = false, Default = "guest", HelpText = "Input auth password.")]
        public string Password { get; set; }

        [Option('v', "virtualhost", Required = false, Default = "/", HelpText = "Input virtual host.")]
        public string VirtualHost { get; set; }

        [Option('q', "queuename", Required = false, Default = "hello", HelpText = "Input queue name.")]
        public string QueueName { get; set; }
    }

    [Verb("send", HelpText = "Send messege to RabbitMQ.")]
    internal class SendOptions : OptionsBase
    {
        [Option('i', "interactive", Required = false, Default = false, HelpText = "Interactive mode. Input \"exit\" to exit.")]
        public bool Interactive { get; set; }
        
        [Option('m', "message", Required = false, Default = "hello world!", HelpText = "Input message.")]
        public string Message { get; set; }
    }

    [Verb("receive", HelpText = "Receive messege from RabbitMQ.")]
    internal class ReceiveOptions : OptionsBase
    {
    }
}
