using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "test",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (ch, ea) =>
{
    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
    Console.WriteLine($"Consooomed the {content}");

    channel.BasicAck(ea.DeliveryTag, false);
};

channel.BasicConsume("test", false, consumer);

Console.ReadLine();