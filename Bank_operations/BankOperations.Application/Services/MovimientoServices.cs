using AutoMapper;
using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.constants;
using BankOperations.Application.Helpers.Exceptions;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Services.Interface;
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

        public async Task<Response<int>> CreateMovimientoAsync(MovimientoDTORequest movimientoRequest)
        {
            var cuenta = _unitOfWork.CuentaRepository
                .SingleOrDefault(c => c.IdCliente == movimientoRequest.IdCliente && c.Id == movimientoRequest.IdCuenta, c => c.Movimientos);

            if (cuenta == null)
                throw new KeyNotFoundException("Validar que el cliente y la cuenta solicitados se encuentren registrados.");

            var ultimoSaldo = cuenta.Movimientos?.OrderByDescending(m => m.Id)?.FirstOrDefault()?.Saldo ?? 0;
            var nuevoMovimiento = _mapper.Map<Movimiento>(movimientoRequest);

            if (movimientoRequest.Valor > 0)
            {
                nuevoMovimiento.TipoMovimiento = "Crédito";
                nuevoMovimiento.Saldo = ultimoSaldo + movimientoRequest.Valor;
            }
            else
            {
                var valorResta = movimientoRequest.Valor * (-1);
                nuevoMovimiento.TipoMovimiento = "Debito";
                var nuevoSaldo = ultimoSaldo - valorResta;

                if (nuevoSaldo < 0)
                    throw new ApiException("Saldo no disponible");

                nuevoMovimiento.Saldo = nuevoSaldo;
            }

            var movimiento = _unitOfWork.MovimientoRepository.Add(nuevoMovimiento);
            var save = await _unitOfWork.Save() > 0;

            return save ? new Response<int>(movimiento.Id) :
                throw new DbUpdateException("A ocurrido un error en el proceso de guardado");
        }

        public async Task<Response<int>> DeleteMovimientoAsync(int id)
        {

            Movimiento movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.Id.Equals(id));

            if (movimiento == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");
            }
            else
            {
                _unitOfWork.MovimientoRepository.Delete(id);
                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(id) :
                    throw new Exception($"Acurrido un error en el proceso de Eliminado por favor intente de nuevo");

            }
        }

        public Response<List<MovimientoDTOResponse>> GetMovimientoAll()
        {
            List<Movimiento> movimientos = _unitOfWork.MovimientoRepository.GetAll().ToList();
            List<MovimientoDTOResponse> movimientosDTO = _mapper.Map<List<MovimientoDTOResponse>>(movimientos);
            return new Response<List<MovimientoDTOResponse>>(movimientosDTO);
        }

        public Response<MovimientoDTOResponse> GetMovimientoById(int id)
        {
            Movimiento movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.Id.Equals(id));
            if (movimiento == null)
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");

            MovimientoDTOResponse movimientoDto = _mapper.Map<MovimientoDTOResponse>(movimiento);
            return new Response<MovimientoDTOResponse>(movimientoDto);
        }

        private MovimientoClienteReporteGeneralDTOResponse GetUltimoReporteByCuenta(ReporteGeneralMovimientoClienteDTORequest reporteGeneralMovimiento, string tipoCuenta)
        {
            Movimiento movimiento = _unitOfWork.MovimientoRepository.FindAll(x => x.IdCliente.Equals(reporteGeneralMovimiento.ClienteId) &&
                                                                                   x.Fecha >= reporteGeneralMovimiento.FechaInicial &&
                                                                                   x.Fecha <= reporteGeneralMovimiento.FechaFinal &&
                                                                                   x.IdCuentaNavigation.TipoCuenta.Equals(tipoCuenta),
                                                                                   p => p.IdClienteNavigation,
                                                                                   p => p.IdCuentaNavigation).OrderByDescending(x => x.Id).FirstOrDefault();

            if (movimiento == null) 
                return null;

            return new MovimientoClienteReporteGeneralDTOResponse()
            {
                Fecha = movimiento.Fecha.ToString(),
                Cliente = movimiento.IdClienteNavigation.Nombre,
                numeroCuenta = movimiento?.IdCuentaNavigation?.NumeroCuenta ?? null!,
                tipo = movimiento?.IdCuentaNavigation?.TipoCuenta ?? null!,
                SaldoInicial = movimiento?.Valor > 0 ? movimiento?.Saldo - movimiento?.Valor ?? 0 : movimiento?.Valor * (-1) + movimiento?.Saldo ?? 0,
                Estado = movimiento?.IdCuentaNavigation?.Estado.ToString() ?? null!,
                Movimiento = movimiento.Valor,
                SaldoDisponible = movimiento.Saldo
            };

            

        }

        public async Task<Response<List<MovimientoClienteReporteGeneralDTOResponse>>> GetUltimoReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest clienteReporteGeneralRequest)
        {

            List<MovimientoClienteReporteGeneralDTOResponse> listaReportesClienteGeneralDTO = new List<MovimientoClienteReporteGeneralDTOResponse>();
            var ahorros = await Task.Run(() => GetUltimoReporteByCuenta(clienteReporteGeneralRequest, Constants.TIPOCUENTAAHORROS));
            var corriente = await Task.Run(() => GetUltimoReporteByCuenta(clienteReporteGeneralRequest, Constants.TIPOCUENTACORRIENTE));

            if (ahorros != null)
                listaReportesClienteGeneralDTO.Add(ahorros);

            if (corriente != null)
                listaReportesClienteGeneralDTO.Add(corriente);
           

            if (!listaReportesClienteGeneralDTO.Any())
                throw new KeyNotFoundException($"Registro no encontrado, por favor verifique el Id del cliente y las fechas ingresadas");

            return new Response<List<MovimientoClienteReporteGeneralDTOResponse>>(listaReportesClienteGeneralDTO);

        }

        
        public async Task<Response<int>> UpdateMovimientoAsync(MovimientoDTOUpdateRequest clienteUpdateRequest)
        {
            Movimiento movimiento = _unitOfWork.MovimientoRepository.FirstOrDefault(x => x.IdCliente.Equals(clienteUpdateRequest.Id),
                                                                              p => p.IdCuentaNavigation);
            if (movimiento == null)
                throw new KeyNotFoundException($"Registro no encontrado con el id {clienteUpdateRequest.Id}");

            if (clienteUpdateRequest.Valor > 0)
            {
                int saldoTotal = movimiento.IdCuentaNavigation.SaldoInicial + clienteUpdateRequest.Valor;
                movimiento.TipoMovimiento = "crédito";
                movimiento.Valor = clienteUpdateRequest.Valor;
                movimiento.IdCuentaNavigation.SaldoInicial = saldoTotal;
                movimiento.Saldo = saldoTotal;
            }

            _unitOfWork.MovimientoRepository.Update(movimiento);

            bool save = await _unitOfWork.Save() > 0;
            return save ? new Response<int>(movimiento.Id) :
                throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
        }

        public Response<List<MovimientoClienteReporteGeneralDTOResponse>> GetReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest clienteReporteGeneralRequest)
        {
            List<Movimiento> movimientos = _unitOfWork.MovimientoRepository.FindAll(x => x.IdCliente.Equals(clienteReporteGeneralRequest.ClienteId) &&
                                                                                  x.Fecha >= clienteReporteGeneralRequest.FechaInicial &&
                                                                                  x.Fecha <= clienteReporteGeneralRequest.FechaFinal,
                                                                                  p => p.IdClienteNavigation,
                                                                                  p => p.IdCuentaNavigation).ToList();

            List<MovimientoClienteReporteGeneralDTOResponse> listaReportesClienteGeneralDTO = new List<MovimientoClienteReporteGeneralDTOResponse>();

            foreach (var movimiento in movimientos)
            {
                MovimientoClienteReporteGeneralDTOResponse movimientoResponse = new MovimientoClienteReporteGeneralDTOResponse()
                {
                    Fecha = movimiento.Fecha.ToString(),
                    Cliente = movimiento.IdClienteNavigation.Nombre,
                    numeroCuenta = movimiento?.IdCuentaNavigation?.NumeroCuenta ?? null!,
                    tipo = movimiento?.IdCuentaNavigation?.TipoCuenta ?? null!,
                    SaldoInicial = movimiento?.Valor > 0 ? movimiento?.Saldo - movimiento?.Valor ?? 0 : movimiento?.Valor * (-1) + movimiento?.Saldo ?? 0,
                    Estado = movimiento?.IdCuentaNavigation?.Estado.ToString() ?? null!,
                    Movimiento = movimiento.Valor,
                    SaldoDisponible = movimiento.Saldo
                };

                listaReportesClienteGeneralDTO.Add(movimientoResponse);
            }
           
            if (!listaReportesClienteGeneralDTO.Any())
                throw new KeyNotFoundException($"Registro no encontrado, por favor verifique el Id del cliente y las fechas ingresadas");

            return new Response<List<MovimientoClienteReporteGeneralDTOResponse>>(listaReportesClienteGeneralDTO);
        }

        #endregion
    }
}
