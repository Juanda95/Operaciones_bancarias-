using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankOperations.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        #region Attribute
        private readonly IClienteServices _clienteServices;
        #endregion

        #region Builder
        public ClientesController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }
        #endregion

        #region Methods

        // GET

        [HttpGet("GetClienteById/{id}")]
        public  IActionResult GetClienteById(int id)
        {
            return Ok(_clienteServices.GetClienteById(id));
        }

        // GET ALL
        [HttpGet]
        [Route("GetAllClientes")]
        public IActionResult GetAllClientes()
        {
            return Ok( _clienteServices.GetClienteAll());
        }

        // POST
        [HttpPost]
        [Route("InsertCliente")]
        public async Task<IActionResult> InsertCliente(ClienteDTORequest clienteRequest)
        {
            return Ok(await _clienteServices.CreateClienteAsync(clienteRequest));
        }

        //PUT 
        [HttpPut]
        [Route("UpdateCliente")]
        public async Task<IActionResult> UpdateCliente(ClienteDTOUpdateRequest clienteUpdateRequest)
        {

            return Ok(await _clienteServices.UpdateClienteAsync(clienteUpdateRequest));

        }

        //DELETE 
        [HttpDelete("DeleteCliente/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            return Ok(await _clienteServices.DeleteClienteAsync(id));
        }
        #endregion


    }
}
