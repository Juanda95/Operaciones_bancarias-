using BankOperations.Domain.Entities;
using BankOperations.Persistence.Contexts;
using BankOperations.Persistence.Repository;
using BankOperations.Persistence.Repository.Interface;
using BankOperations.Persistence.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankOperations.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Attributes

        private readonly DataContext _dataContext;

        #endregion

        #region builder

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Properties

        private IRepository<Cliente> _clienteRepository;
        private IRepository<Cuenta> _cuentaRepository;
        private IRepository<Movimiento> _movimientoRepository;
        private IRepository<Persona> _personaRepository;

        #endregion

        public IRepository<Cliente> ClienteRepository
        {
            get
            {
                return _clienteRepository == null ?
                       _clienteRepository = new Repository<Cliente>(_dataContext) :
                       _clienteRepository;
            }
        }

        public IRepository<Cuenta> CuentaRepository
        {
            get
            {

                return _cuentaRepository == null ?
                       _cuentaRepository = new Repository<Cuenta>(_dataContext) :
                       _cuentaRepository;
            }
        }

        public IRepository<Movimiento> MovimientoRepository
        {
            get
            {
                return _movimientoRepository == null ?
                       _movimientoRepository = new Repository<Movimiento>(_dataContext) :
                       _movimientoRepository;
            }
        }

        public IRepository<Persona> PersonaRepository
        {
            get
            {
                return _personaRepository == null ?
                         _personaRepository = new Repository<Persona>(_dataContext) :
                         _personaRepository;
            }
        }

        public async Task<int> Save() => await _dataContext.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync() { return await _dataContext.Database.BeginTransactionAsync(); }
    }
}
