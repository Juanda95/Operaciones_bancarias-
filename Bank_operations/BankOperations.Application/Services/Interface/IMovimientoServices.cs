using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;

namespace BankOperations.Application.Services.Interface
{
    public interface IMovimientoServices
    {
        Response<List<MovimientoDTOResponse>> GetMovimientoAll();
        Response<MovimientoDTOResponse> GetMovimientoById(int Id);
        Task<Response<int>> CreateMovimientoAsync(MovimientoDTORequest MovimientoRequest);
        Task<Response<int>> DeleteMovimientoAsync(int Id);
        Task<Response<int>> UpdateMovimientoAsync(MovimientoDTOUpdateRequest MovimientoUpdateRequest);
        Task<Response<List<MovimientoClienteReporteGeneralDTOResponse>>> GetUltimoReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest ClienteReporteGeneralRequest);
        Response<List<MovimientoClienteReporteGeneralDTOResponse>> GetReporteClienteGeneral(ReporteGeneralMovimientoClienteDTORequest ClienteReporteGeneralRequest);
    }
}
