using Common;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ConsumerConsole
{
    internal class OrderConsumer : IConsumer<Order>
    {
        public OrderConsumer()
        {
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync($"Got new message {context.Message.Name}");
        }
    }
}