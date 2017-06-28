# rabbitmq-cli
RabbitMQ command line interface

## Require environment
.NET Core 2.0 runtime
https://www.microsoft.com/net/core/preview

## Usage

### command verbs

```
  send       Send messege to RabbitMQ.

  receive    Receive messege from RabbitMQ.

  help       Display more information on a specific command.

  version    Display version information.
```

### send command arguments

```
  -i, --interactive       (Default: false) Interactive mode. Input "exit" to exit.

  -m, --message           (Default: hello world!) Input message.

  -h, --host              (Default: localhost) Input host name.

  --port                  (Default: 5672) Input host port.

  -a, --authmechanisms    (Default: PLAIN) Input auth mechanisms.

  -u, --user              (Default: guest) Input auth user.

  --password              (Default: guest) Input auth password.

  -v, --virtualhost       (Default: /) Input virtual host.

  -q, --queuename         (Default: hello) Input queue name.

  --help                  Display this help screen.

  --version               Display version information.
```

### receive command arguments

```
  -h, --host              (Default: localhost) Input host name.

  --port                  (Default: 5672) Input host port.

  -a, --authmechanisms    (Default: PLAIN) Input auth mechanisms.

  -u, --user              (Default: guest) Input auth user.

  --password              (Default: guest) Input auth password.

  -v, --virtualhost       (Default: /) Input virtual host.

  -q, --queuename         (Default: hello) Input queue name.

  --help                  Display this help screen.

  --version               Display version information.
```

### send command example

```
rabbitmq-cli send -h example.com -u guest -p guest -q test -i
```

### receive command example

```
rabbitmq-cli receive -h example.com -u guest -p guest -q test
```
