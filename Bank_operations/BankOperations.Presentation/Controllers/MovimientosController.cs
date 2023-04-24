using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankOperations.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        #region Attribute
        private readonly IMovimientoServices _movimientoServices;
        #endregion

        #region Builder
        public MovimientosController(IMovimientoServices movimientoServices)
        {
            _movimientoServices = movimientoServices;
        }
        #endregion

        #region Methods

        // GET

        [HttpGet("GetMovimientoById/{id}")]
        public IActionResult GetMovimientoById(int id)
        {
            return Ok(_movimientoServices.GetMovimientoById(id));
        }

        // GET REPORTE GENERAL
        [HttpGet("GetUltimoMovimientoClieneteGenetal/{id}/{fechaIni}/{fechaFin}")]
        public async Task<IActionResult> GetUltimoMovimientoGeneralCliente(int id, DateTime fechaIni, DateTime fechaFin)
        {
            return Ok(
                await _movimientoServices.GetUltimoReporteClienteGeneral(
                    new ReporteGeneralMovimientoClienteDTORequest
                    {
                        ClienteId = id,
                        FechaInicial = fechaIni,
                        FechaFinal = fechaFin
                    }));
        }

        // GET REPORTE GENERAL
        [HttpGet("GetMovimientoClieneteGenetal/{id}/{fechaIni}/{fechaFin}")]
        public IActionResult GetMovimientoGeneralCliente(int id, DateTime fechaIni, DateTime fechaFin)
        {
            return Ok(
                 _movimientoServices.GetReporteClienteGeneral(
                    new ReporteGeneralMovimientoClienteDTORequest
                    {
                        ClienteId = id,
                        FechaInicial = fechaIni,
                        FechaFinal = fechaFin
                    }));
        }

        // GET ALL
        [HttpGet]
        [Route("GetAllMovimientos")]
        public IActionResult GetAllMovimientos()
        {
            return Ok(_movimientoServices.GetMovimientoAll());
        }

        // POST
        [HttpPost]
        [Route("InsertMovimiento")]
        public async Task<IActionResult> InsertMovimiento(MovimientoDTORequest movimientoRequest)
        {
            return Ok(await _movimientoServices.CreateMovimientoAsync(movimientoRequest));
        }

        //PUT 
        [HttpPut]
        [Route("UpdateMovimiento")]
        public async Task<IActionResult> UpdateCuenta(MovimientoDTOUpdateRequest movimientoUpdateRequest)
        {

            return Ok(await _movimientoServices.UpdateMovimientoAsync(movimientoUpdateRequest));

        }

        //DELETE 
        [HttpDelete("DeleteMovimiento/{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            return Ok(await _movimientoServices.DeleteMovimientoAsync(id));
        }
        #endregion
    }
}
