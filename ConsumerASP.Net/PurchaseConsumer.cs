using Common;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsumerASP.Net
{
    internal class PurchaseConsumer : IConsumer<Purchase>
    {
        private readonly ILogger<PurchaseConsumer> logger;

        public PurchaseConsumer(ILogger<PurchaseConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<Purchase> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            logger.LogInformation($"Got new message {context.Message.Name}");
        }
    }

}