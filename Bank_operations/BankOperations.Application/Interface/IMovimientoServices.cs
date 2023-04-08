using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;

namespace BankOperations.Application.Interface
{
    public interface IMovimientoServices
    {
        Response<List<MovimientoDTOResponse>> GetMovimientoAll();
        Response<MovimientoDTOResponse> GetMovimientoById(int Id);
        Task<Response<int>> CreateMovimientoAsync(MovimientoDTORequest MovimientoRequest);
        Task<Response<int>> DeleteMovimientoAsync(int Id);
        Task<Response<int>> UpdateMovimientoAsync(MovimientoDTOUpdateRequest MovimientoUpdateRequest);
        Response<List<MovimientoClienteReporteGeneralDTOResponse>> GetReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest ClienteReporteGeneralRequest);
    }
}
