using RabbitMQ.Client;
using System;
using System.Text;

internal class Program
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            while (true)
            {

                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;

                string message = "{\"name\":\"" + value + "\"}";
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.ContentType = "application/json";
                channel.BasicPublish(exchange: "device-updates",
                                     routingKey: "Order",
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

       // Console.WriteLine(" Press [enter] to exit.");
        //Console.ReadLine();
    }
}