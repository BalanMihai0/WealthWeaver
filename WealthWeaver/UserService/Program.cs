using UserService.Interfaces;
using UserService.Services;

namespace UserService
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ITokenVerifier, TokenVerifier>();
            builder.Services.AddSingleton<RabbitMqListener>();

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();

            var app = builder.Build();

            // Start RabbitMQ listener
            var listener = app.Services.GetRequiredService<RabbitMqListener>();
            listener.StartListening();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

