namespace ConsumerConsole
{
    using MassTransit;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                //cfg.ClearMessageDeserializers();
                cfg.UseRawJsonSerializer();

                cfg.ReceiveEndpoint("device-updates", e =>
                {  
                    //e.ConfigureConsumeTopology = false;
                                  
                    e.Consumer<OrderConsumer>();
                });
            });


            
           
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}