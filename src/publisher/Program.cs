using System.Text;
using RabbitMQ.Client;

var message = "Hello from publisher";

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "test",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "",
    routingKey: "test",
    basicProperties: null,
    body: body);