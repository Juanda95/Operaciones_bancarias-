using BankOperations.Application;
using BankOperations.Persistence;

namespace BankOperations.Presentation.Handlers
{
    public class ServiceExtensionsHandler
    {
        public static void ServiceExtensionsConfig(WebApplicationBuilder builder)
        {

            builder.Services.AddPersitenceInfraestructure(builder.Configuration);
            builder.Services.AddAplicationLayer();

        }

        public static void ServiceExtensionsConfigMigrations(IServiceScopeFactory scopeFactory)
        {
            ServiceExtensionsInfraestructure.AddMigrationsInfraestructure(scopeFactory);

        }

    }
}
