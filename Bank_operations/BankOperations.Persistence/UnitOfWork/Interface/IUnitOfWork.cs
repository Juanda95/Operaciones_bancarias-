using BankOperations.Domain.Entities;
using BankOperations.Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Persistence.UnitOfWork.Interface
{
    public  interface IUnitOfWork
    {
        IRepository<Cliente> ClienteRepository { get; }
        IRepository<Cuenta> CuentaRepository { get; }
        IRepository<Movimiento> MovimientoRepository { get; }
        IRepository<Persona> PersonaRepository { get; }


        Task<int> Save();
    }
}
