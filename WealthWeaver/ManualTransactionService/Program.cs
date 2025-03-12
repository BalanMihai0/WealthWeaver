using Microsoft.Azure.Cosmos;
using ManualTransactionService.Services;

namespace ManualTransactionService
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure CosmosDB
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var cosmosConfig = builder.Configuration.GetSection("CosmosDb");
            var endpointUri = cosmosConfig["EndpointUri"];
            var primaryKey = cosmosConfig["PrimaryKey"];

            ArgumentException.ThrowIfNullOrEmpty(endpointUri);
            ArgumentException.ThrowIfNullOrEmpty(primaryKey);

            builder.Services.AddSingleton<CosmosClient>(sp =>
            {
                var cosmosClientOptions = new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway
                };
                return new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);
            });
            builder.Services.AddScoped<ITransactionProcessor, TransactionProcessor>();
            
            // Configure services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();

            builder.Services.AddControllers();

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
