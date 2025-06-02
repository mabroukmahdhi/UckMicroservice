using Talks.Brokers.DateTimes;
using Talks.Brokers.Loggings;
using Talks.Brokers.Storages;
using Talks.Services.Foundations.Talks;
using AutoInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Talks.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureTalks(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StorageBroker>();
            builder.Services.UseAutoInjection();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: new OpenApiInfo
                    {
                        Title = "Talks by Prosiria",
                        Version = "v1"
                    }
                );
            });

            // brokers
            builder.Services.AddScoped<IStorageBroker, StorageBroker>();
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
            builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();

            // services
            builder.Services.AddTransient<ITalkService, TalkService>();
        }

        public static void RunTalks(this WebApplicationBuilder builder)
        {
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "Prosiria: Talks v1"
                );
            });
            app.Run();
        }
    }
}