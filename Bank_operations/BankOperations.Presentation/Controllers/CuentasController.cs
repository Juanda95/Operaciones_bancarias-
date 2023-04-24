using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankOperations.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        #region Attribute
        private readonly ICuentaServices _cuentaServices;
        #endregion

        #region Builder
        public CuentasController(ICuentaServices cuentaServices)
        {
            _cuentaServices = cuentaServices;
        }
        #endregion

        #region Methods

        // GET

        [HttpGet("GetCuentaById/{id}")]
        public IActionResult GetCuentaById(int id)
        {
            return Ok(_cuentaServices.GetCuentaById(id));
        }

        // GET ALL
        [HttpGet]
        [Route("GetAllCuentas")]
        public IActionResult GetAllCuentas()
        {
            return Ok(_cuentaServices.GetCuentaAll());
        }

        // POST
        [HttpPost]
        [Route("InsertCuenta")]
        public async Task<IActionResult> InsertCuenta(CuentaDTORequest cuentaRequest)
        {
            return Ok(await _cuentaServices.CreateCuentaAsync(cuentaRequest));
        }

        //PUT 
        [HttpPut]
        [Route("UpdateCuenta")]
        public async Task<IActionResult> UpdateCuenta(CuentaDTOUpdateRequest cuentaUpdateRequest)
        {

            return Ok(await _cuentaServices.UpdateCuentaAsync(cuentaUpdateRequest));

        }

        //DELETE 
        [HttpDelete("DeleteCuenta/{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            return Ok(await _cuentaServices.DeleteCuentaAsync(id));
        }
        #endregion
    }
}
