using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.Interface;
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
        [HttpGet("GetMovimientoClieneteGenetal/{id}/{FechaIni}/{FechaFin}")]
        public IActionResult GetMovimientoGeneralCliente(int id, DateTime FechaIni, DateTime FechaFin)
        {
            return Ok(_movimientoServices.GetReporteClienteGeneral( new ReporteGeneralMovimientoClienteDTORequest { ClienteId = id, FechaInicial = FechaIni, FechaFinal = FechaFin }));
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
        public async Task<IActionResult> InsertMovimiento(MovimientoDTORequest MovimientoRequest)
        {
            return Ok(await _movimientoServices.CreateMovimientoAsync(MovimientoRequest));
        }

        //PUT 
        [HttpPut]
        [Route("UpdateMovimiento")]
        public async Task<IActionResult> UpdateCuenta(MovimientoDTOUpdateRequest MovimientoUpdateRequest)
        {

            return Ok(await _movimientoServices.UpdateMovimientoAsync(MovimientoUpdateRequest));

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
