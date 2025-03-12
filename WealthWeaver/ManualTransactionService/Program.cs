using Microsoft.Azure.Cosmos;
using ManualTransactionService.Services;

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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();

            builder.Services.AddControllers();

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();

            builder.Services.AddControllers();
            builder.Services.AddSingleton<CosmosClient>(sp =>
            {
                var cosmosClientOptions = new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                return new CosmosClient("https://localhost:8081", "your_cosmosdb_key", cosmosClientOptions);
            });
            builder.Services.AddScoped<ITransactionProcessor, TransactionProcessor>();


            var app = builder.Build();

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

