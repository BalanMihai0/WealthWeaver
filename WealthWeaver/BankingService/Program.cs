using BankingService.Interfaces;
using BankingService.Services;
using BankingService.Models;
using BankingService.Logic;

namespace BankingService
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration from env.jsonc
            builder.Configuration.AddJsonFile("env.jsonc", optional: false, reloadOnChange: true);

            // Configure services
            builder.Services.Configure<PlaidOptionsCustom>(builder.Configuration.GetSection("PlaidOptions"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IPlaidLinker, PlaidLinker>();
            builder.Services.AddTransient<IBankingServiceClient, BankingServiceClient>();
            builder.Services.AddLogging();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<RabbitMqResponseListener>();
            builder.Services.AddSingleton<RabbitMQClient>();


            builder.Services.AddControllers();

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();

            var app = builder.Build();

            var listener = app.Services.GetRequiredService<RabbitMqResponseListener>();
            listener.StartListening();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

