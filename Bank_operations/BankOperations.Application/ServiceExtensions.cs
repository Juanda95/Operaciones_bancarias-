using BankOperations.Application.Interface;
using BankOperations.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankOperations.Application
{
    public static class ServiceExtensions
    {
        public static void AddAplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IClienteServices, ClienteServices>();
        }   
    }
}
