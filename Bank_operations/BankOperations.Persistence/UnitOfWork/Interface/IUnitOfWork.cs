using BankOperations.Domain.Entities;
using BankOperations.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankOperations.Persistence.UnitOfWork.Interface
{
    public  interface IUnitOfWork
    {
        IRepository<Cliente> ClienteRepository { get; }
        IRepository<Cuenta> CuentaRepository { get; }
        IRepository<Movimiento> MovimientoRepository { get; }
        IRepository<Persona> PersonaRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> Save();
    }
}
