using AutoMapper;
using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Exceptions;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Interface;
using BankOperations.Domain.Entities;
using BankOperations.Persistence.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankOperations.Application.Services
{
    public class MovimientoServices : IMovimientoServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public MovimientoServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<Response<int>> CreateMovimientoAsync(MovimientoDTORequest MovimientoRequest)
        {
            //try
            //{
            //    Cuenta cuentaEstadoActual = _unitOfWork.CuentaRepository.FirstOrDefault(x => x.IdCliente.Equals(MovimientoRequest.IdCliente) &&
            //                                                                        x.Id.Equals(MovimientoRequest.IdCuenta),
            //                                                                        p => p.Movimientos);
            //    int SaldoUltimoMovimiento = cuentaEstadoActual?.Movimientos?.OrderByDescending(x => x.Id).FirstOrDefault()?.Saldo ?? 0;
            //    if (cuentaEstadoActual == null) throw new KeyNotFoundException($"Validar que el cliente y la cuenta solicitados se encuentren registrados.");

            //    int saldoTotal = 0;
            //    Movimiento nuevaMovimiento = _mapper.Map<Movimiento>(MovimientoRequest);

            //    if (MovimientoRequest.Valor > 0)
            //    {
            //        saldoTotal = SaldoUltimoMovimiento + MovimientoRequest.Valor;
            //        nuevaMovimiento.TipoMovimiento = "Crédito";
            //    }
            //    else
            //    {
            //        int valorResta = MovimientoRequest.Valor * (-1);
            //        nuevaMovimiento.TipoMovimiento = "Debito";
            //        saldoTotal = SaldoUltimoMovimiento - valorResta;
            //        if (saldoTotal < 0)
            //        {
            //            throw new ApiException($"Saldo no disponible");
            //        }
            //    }
            //    nuevaMovimiento.Saldo = saldoTotal;

            //    Movimiento movimiento = _unitOfWork.MovimientoRepository.Add(nuevaMovimiento);
            //    bool save = await _unitOfWork.Save() > 0;
            //    return save ? new Response<int>(movimiento.Id) : throw new Exception($"A ocurrido un error en el proceso de guardado");
            //}
            //catch (KeyNotFoundException)
            //{
            //    throw;
            //}
            //catch (ApiException)
            //{
            //    throw;
            //}
            //catch (Exception)
            //{
            //    // Auditoria de errores 
            //    throw;
            //}

            try
            {
                var cuenta = _unitOfWork.CuentaRepository
                    .SingleOrDefault(c => c.IdCliente == MovimientoRequest.IdCliente && c.Id == MovimientoRequest.IdCuenta, c => c.Movimientos);

                if (cuenta == null) throw new KeyNotFoundException("Validar que el cliente y la cuenta solicitados se encuentren registrados.");
                
                var ultimoSaldo = cuenta.Movimientos?.OrderByDescending(m => m.Id)?.FirstOrDefault()?.Saldo ?? 0;
                var nuevoMovimiento = _mapper.Map<Movimiento>(MovimientoRequest);

                if (MovimientoRequest.Valor > 0)
                {
                    nuevoMovimiento.TipoMovimiento = "Crédito";
                    nuevoMovimiento.Saldo = ultimoSaldo + MovimientoRequest.Valor;
                }
                else
                {
                    var valorResta = MovimientoRequest.Valor * (-1);
                    nuevoMovimiento.TipoMovimiento = "Debito";
                    var nuevoSaldo = ultimoSaldo - valorResta;

                    if (nuevoSaldo < 0) throw new ApiException("Saldo no disponible");                    

                    nuevoMovimiento.Saldo = nuevoSaldo;
                }

                var movimiento = _unitOfWork.MovimientoRepository.Add(nuevoMovimiento);
                var save = await _unitOfWork.Save() > 0;

                return save ? new Response<int>(movimiento.Id) : throw new DbUpdateException("A ocurrido un error en el proceso de guardado");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }
        }


        public async Task<Response<int>> DeleteMovimientoAsync(int Id)
        {
            try
            {
                Movimiento movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.Id.Equals(Id));

                if (movimiento == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");
                }
                else
                {
                    _unitOfWork.MovimientoRepository.Delete(Id);
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

        public Response<List<MovimientoDTOResponse>> GetMovimientoAll()
        {
            try
            {
                List<Movimiento> movimientos = _unitOfWork.MovimientoRepository.GetAll().ToList();
                List<MovimientoDTOResponse> movimientosDTO = _mapper.Map<List<MovimientoDTOResponse>>(movimientos);
                return new Response<List<MovimientoDTOResponse>>(movimientosDTO);
            }
            catch (Exception)
            {
                // Auditoria de errores 
                throw;
            }
        }

        public Response<MovimientoDTOResponse> GetMovimientoById(int Id)
        {
            try
            {
                Movimiento movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.Id.Equals(Id));
                if (movimiento == null) throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");

                MovimientoDTOResponse MovimientoDto = _mapper.Map<MovimientoDTOResponse>(movimiento);
                return new Response<MovimientoDTOResponse>(MovimientoDto);

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
        private MovimientoClienteReporteGeneralDTOResponse GetUltimoReporteByCuenta(ReporteGeneralMovimientoClienteDTORequest ReporteGeneralMovimiento, string TipoCuenta)
        {
            try
            {
                Movimiento movimiento = _unitOfWork.MovimientoRepository.FindAll(x => x.IdCliente.Equals(ReporteGeneralMovimiento.ClienteId) &&
                                                                                       x.Fecha >= ReporteGeneralMovimiento.FechaInicial &&
                                                                                       x.Fecha <= ReporteGeneralMovimiento.FechaFinal &&
                                                                                       x.IdCuentaNavigation.TipoCuenta.Equals(TipoCuenta),
                                                                                       p => p.IdClienteNavigation,
                                                                                       p => p.IdCuentaNavigation).OrderByDescending(x => x.Id).FirstOrDefault() ?? null!;
               
                if (movimiento == null) return null!;

                MovimientoClienteReporteGeneralDTOResponse reporteClienteGeneralDTO = new MovimientoClienteReporteGeneralDTOResponse()
                {
                    Fecha = movimiento.Fecha.ToString(),
                    Cliente = movimiento.IdClienteNavigation.Nombre,
                    numeroCuenta = movimiento?.IdCuentaNavigation?.NumeroCuenta ?? null!,
                    tipo = movimiento?.IdCuentaNavigation?.TipoCuenta ?? null!,
                    SaldoInicial = movimiento?.Valor > 0 ? movimiento?.Saldo - movimiento?.Valor ?? 0 : movimiento?.Valor * (-1) + movimiento?.Saldo ?? 0,
                    Estado = movimiento?.IdCuentaNavigation?.Estado.ToString() ?? null!,
                    Movimiento = movimiento?.Valor ?? 0,
                    SaldoDisponible = movimiento?.Saldo ?? 0
                };

                return reporteClienteGeneralDTO;
            }
            catch (Exception)
            {
                throw;
            }
           

        }

        public Response<List<MovimientoClienteReporteGeneralDTOResponse>> GetReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest ClienteReporteGeneralRequest)
        {
            try
            {
                List<MovimientoClienteReporteGeneralDTOResponse> listaReportesClienteGeneralDTO = new List<MovimientoClienteReporteGeneralDTOResponse>();

                var Reporte = GetUltimoReporteByCuenta(ClienteReporteGeneralRequest, "Ahorros");       
                if (Reporte != null) listaReportesClienteGeneralDTO.Add(Reporte);
                Reporte = GetUltimoReporteByCuenta(ClienteReporteGeneralRequest, "Corriente");
                if (Reporte != null) listaReportesClienteGeneralDTO.Add(Reporte);

                if (listaReportesClienteGeneralDTO.Count().Equals(0)) throw new KeyNotFoundException($"Registro no encontrado, por favor verifique el Id del cliente y las fechas ingresadas");

                return new Response<List<MovimientoClienteReporteGeneralDTOResponse>>(listaReportesClienteGeneralDTO);
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

        public async Task<Response<int>> UpdateMovimientoAsync(MovimientoDTOUpdateRequest ClienteUpdateRequest)
        {
            try
            {
                Movimiento Movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.IdCliente.Equals(ClienteUpdateRequest.Id),
                                                                                  p => p.IdCuentaNavigation);
                if (Movimiento == null) throw new KeyNotFoundException($"Registro no encontrado con el id {ClienteUpdateRequest.Id}");

                if (ClienteUpdateRequest.Valor > 0)
                {
                    int saldoTotal = Movimiento.IdCuentaNavigation.SaldoInicial + ClienteUpdateRequest.Valor;
                    Movimiento.TipoMovimiento = "crédito";
                    Movimiento.Valor = ClienteUpdateRequest.Valor;
                    Movimiento.IdCuentaNavigation.SaldoInicial = saldoTotal;
                    Movimiento.Saldo = saldoTotal;

                }

                _unitOfWork.MovimientoRepository.Update(Movimiento);

                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(Movimiento.Id) : throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
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
