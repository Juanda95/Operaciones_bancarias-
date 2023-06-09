﻿using BankOperations.Application.Services;
using BankOperations.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankOperations.Application
{
    public static class ServiceExtensionsApplication
    {
        public static void AddAplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IClienteServices, ClienteServices>();
            services.AddTransient<ICuentaServices, CuentaServices>();
            services.AddTransient<IMovimientoServices, MovimientoServices>();
        }   
    }
}
