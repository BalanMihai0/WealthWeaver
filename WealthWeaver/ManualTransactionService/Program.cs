using Microsoft.Azure.Cosmos;
using ManualTransactionService.Services;
using System.Net.Http;

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
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections - CosmosDB emulator SSL
                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections

                var cosmosClientOptions = new CosmosClientOptions
                {
                    HttpClientFactory = () => new HttpClient(httpClientHandler),
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
