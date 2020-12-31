using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans.Streams;
using Orleans;

namespace Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
        }

        //Task<string> IHello.SayHello(string greeting)
        async Task<string> IHello.SayHello(string greeting)
        {
            IAsyncStream<string> stream = this.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync($"{this.GetPrimaryKeyString()} - {greeting}");

            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            //return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
            return ($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
