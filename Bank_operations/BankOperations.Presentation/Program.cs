using BankOperations.Presentation.Handlers;
using Serilog;

namespace BankOperations.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region LogsSerilog
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            #endregion

            #region ServiceExtensions            
            ServiceExtensionsHandler.ServiceExtensionsConfig(builder);
            #endregion       
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            #region migrationsConfig
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            ServiceExtensionsHandler.ServiceExtensionsConfigMigrations(scopeFactory);
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.useErrorHandlingMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}