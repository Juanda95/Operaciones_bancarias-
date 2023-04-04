using BankOperations.Persistence;

namespace BankOperations.Presentation.Handlers
{
    public class ServiceExtensionsHandler
    {
        public static void ServiceExtensionsConfig(WebApplicationBuilder builder)
        {

            builder.Services.AddPersitenceInfraestructure(builder.Configuration);

        }

    }
}
