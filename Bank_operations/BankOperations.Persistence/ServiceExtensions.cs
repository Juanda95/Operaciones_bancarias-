using BankOperations.Persistence.Contexts;
using BankOperations.Persistence.Repository;
using BankOperations.Persistence.Repository.Interface;
using BankOperations.Persistence.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankOperations.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersitenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("BankOperations.Persistence")));

            #region Repositories

            services.AddTransient<IUnitOfWork, BankOperations.Persistence.UnitOfWork.UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            
            #endregion

        }
    }
}
