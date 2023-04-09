using AutoMapper;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Interface;
using BankOperations.Domain.Entities;
using BankOperations.Persistence.UnitOfWork.Interface;

namespace BankOperations.Application.Services
{
    public class CuentaServices : ICuentaServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public CuentaServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<bool>> CreateCuentaAsync(CuentaDTORequest CuentaRequest)
        {
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Cliente Cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(CuentaRequest.IdCliente));
                    if (Cliente == null) throw new KeyNotFoundException($"Validar que el cliente se encuentre registrado.");
                    
                    Cuenta nuevaCuenta = _mapper.Map<Cuenta>(CuentaRequest);
                    Cuenta data = _unitOfWork.CuentaRepository.Add(nuevaCuenta);
                    bool save = await _unitOfWork.Save() > 0;
                 
                   Movimiento MovimientoInicial = new Movimiento()
                    {
                        Fecha = DateTime.Today,
                        TipoMovimiento = "Credito",
                        Valor = CuentaRequest.SaldoInicial,
                        Saldo = CuentaRequest.SaldoInicial,
                        IdCliente = CuentaRequest.IdCliente,
                        IdCuenta = data.Id
                    };                 
                    _unitOfWork.MovimientoRepository.Add(MovimientoInicial);
                    save = await _unitOfWork.Save() > 0;
                    await db.CommitAsync();
                    return save ? new Response<bool>(save) : throw new Exception($"A ocurrido un error en el proceso de guardado");
                }
                catch (KeyNotFoundException)
                {
                    throw;
                }
                catch (Exception)
                {
                    // Auditoria de errores 
                    await db.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Response<int>> DeleteCuentaAsync(int Id)
        {
            try
            {
                Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(Id));

                if (cuenta == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");
                }
                else
                {
                    _unitOfWork.CuentaRepository.Delete(Id);
                    bool save = await _unitOfWork.Save() > 0;
                    return save ? new Response<int>(Id) : throw new Exception($"Acurrido un error en el proceso de Eliminado por favor intente de nuevo");

                }
            }
            catch (KeyNotFoundException)
            {

                throw;
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }

        }

        public Response<List<CuentaDTOResponse>> GetCuentaAll()
        {
            try
            {
                List<Cuenta> Cuentas = _unitOfWork.CuentaRepository.GetAll().ToList();
                List<CuentaDTOResponse> cuentasDTO = _mapper.Map<List<CuentaDTOResponse>>(Cuentas);
                return new Response<List<CuentaDTOResponse>>(cuentasDTO);
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }
        }

        public Response<CuentaDTOResponse> GetCuentaById(int Id)
        {
            try
            {
                Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(Id));
                if (cuenta == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");
                }
                else
                {
                    CuentaDTOResponse CuentaDto = _mapper.Map<CuentaDTOResponse>(cuenta);
                    return new Response<CuentaDTOResponse>(CuentaDto);
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }
        }

        public async Task<Response<int>> UpdateCuentaAsync(CuentaDTOUpdateRequest CuentaUpdateRequest)
        {
            try
            {
                Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(CuentaUpdateRequest.Id));

                if (cuenta == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {CuentaUpdateRequest.Id}");
                }
                else
                {
                    cuenta = _mapper.Map<Cuenta>(CuentaUpdateRequest);

                    _unitOfWork.CuentaRepository.Update(cuenta);

                    bool save = await _unitOfWork.Save() > 0;
                    return save ? new Response<int>(cuenta.Id) : throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }


        }
       
        #endregion
 
    }
}
