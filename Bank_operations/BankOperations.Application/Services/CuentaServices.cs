using AutoMapper;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Services.Interface;
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
        public async Task<Response<bool>> CreateCuentaAsync(CuentaDTORequest cuentaRequest)
        {
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Cliente cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(cuentaRequest.IdCliente));
                    if (cliente == null)
                        throw new KeyNotFoundException($"Validar que el cliente se encuentre registrado.");

                    Cuenta nuevaCuenta = _mapper.Map<Cuenta>(cuentaRequest);
                    Cuenta data = _unitOfWork.CuentaRepository.Add(nuevaCuenta);
                    bool save = await _unitOfWork.Save() > 0;

                    Movimiento movimientoInicial = new Movimiento()
                    {
                        Fecha = DateTime.Today,
                        TipoMovimiento = "Credito",
                        Valor = cuentaRequest.SaldoInicial,
                        Saldo = cuentaRequest.SaldoInicial,
                        IdCliente = cuentaRequest.IdCliente,
                        IdCuenta = data.Id
                    };
                    _unitOfWork.MovimientoRepository.Add(movimientoInicial);
                    save = await _unitOfWork.Save() > 0;
                    await db.CommitAsync();
                    return save ? new Response<bool>(save) :
                        throw new Exception($"A ocurrido un error en el proceso de guardado");
                }
                catch (Exception)
                {
                    await db.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Response<int>> DeleteCuentaAsync(int id)
        {
            Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(id));

            if (cuenta == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");
            }
            else
            {
                _unitOfWork.CuentaRepository.Delete(id);
                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(id) :
                    throw new Exception($"Acurrido un error en el proceso de Eliminado por favor intente de nuevo");

            }
        }

        public Response<List<CuentaDTOResponse>> GetCuentaAll()
        {
            List<Cuenta> cuentas = _unitOfWork.CuentaRepository.GetAll().ToList();
            List<CuentaDTOResponse> cuentasDTO = _mapper.Map<List<CuentaDTOResponse>>(cuentas);
            return new Response<List<CuentaDTOResponse>>(cuentasDTO);
        }

        public Response<CuentaDTOResponse> GetCuentaById(int id)
        {
            Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(id));
            if (cuenta == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");
            }
            else
            {
                CuentaDTOResponse cuentaDto = _mapper.Map<CuentaDTOResponse>(cuenta);
                return new Response<CuentaDTOResponse>(cuentaDto);
            }
        }

        public async Task<Response<int>> UpdateCuentaAsync(CuentaDTOUpdateRequest cuentaUpdateRequest)
        {
            Cuenta cuenta = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.Id.Equals(cuentaUpdateRequest.Id));

            if (cuenta == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {cuentaUpdateRequest.Id}");
            }
            else
            {
                cuenta = _mapper.Map<Cuenta>(cuentaUpdateRequest);

                _unitOfWork.CuentaRepository.Update(cuenta);

                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(cuenta.Id) :
                    throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
            }
        }
        #endregion
    }
}
