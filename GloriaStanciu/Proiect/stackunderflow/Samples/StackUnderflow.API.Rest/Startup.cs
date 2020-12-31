using Access.Primitives.IO;
using Access.Primitives.IO.Extensions;
using Access.Primitives.IO.Mocking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using StackUnderflow.Backoffice.Adapters.CreateTenant;
using StackUnderflow.EF.Models;

namespace FakeSO.API.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOperations(typeof(CreateTenantAdapter).Assembly);
            services.AddSingleton<IExecutionContext, LiveExecutionContext>();
            services.AddTransient<IInterpreterAsync>(sp => new LiveInterpreterAsync(sp));

            services.AddDbContext<DatabaseContext>(builder =>
            {
                var connectionString = "Server=LAPTOP-T3JF4NFL\\MSSQLSERVER01;Database=StackOverflow;Trusted_Connection=true";
                builder.UseSqlServer(connectionString);
            });

            services.AddControllers();
            services.AddSingleton(sp => GetSiloClusterClient());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static IClusterClient GetSiloClusterClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(GrainInterfaces.IHello).Assembly)
                    .WithReferences();
                })
                .AddSimpleMessageStreamProvider("SMSProvider", options => { options.FireAndForgetDelivery = true; })
                .Build();
            client.Connect().Wait();
            return client;
        }
    }
}
