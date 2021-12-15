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
    internal class OrderConsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderConsumer> logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            logger.LogInformation($"Got new message {context.Message.Name}");
        }
    }

    internal class GeneralConsumerDefinition<T> : ConsumerDefinition<T> where T : class, IConsumer
    {
        private readonly ILogger<T> logger;

        public GeneralConsumerDefinition(ILogger<T> logger)
        {
            this.logger = logger;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<T> consumerConfigurator)
        {
            endpointConfigurator.ConfigureConsumeTopology = false;
            //endpointConfigurator.ClearMessageDeserializers();
            endpointConfigurator.UseRawJsonSerializer();

            if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbit)
            {
                rabbit.Bind("device-updates");
            }
        }
    }
}