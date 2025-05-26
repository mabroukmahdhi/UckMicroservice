using Attendees.Brokers.DateTimes;
using Attendees.Brokers.Loggings;
using Attendees.Brokers.Storages;
using Attendees.Services.Foundations.Attendees;
using AutoInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Attendees.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ConfigureAttendees(this WebApplicationBuilder builder)
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
                        Title = "Attendees by Prosiria",
                        Version = "v1"
                    }
                );
            });

            // brokers
            builder.Services.AddScoped<IStorageBroker, StorageBroker>();
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
            builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();

            // services
            builder.Services.AddTransient<IAttendeeService, AttendeeService>();
        }

        public static void RunAttendees(this WebApplicationBuilder builder)
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
                    name: "Prosiria: Attendees v1"
                );
            });
            app.Run();
        }
    }
}