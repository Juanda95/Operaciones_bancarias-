using BankOperations.Application.DTOs.Request;
using BankOperations.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankOperations.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        #region Attribute
        private readonly IClienteServices _clienteServices;
        #endregion

        #region Builder
        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }
        #endregion

        [HttpPost]
        [Route("InsertPet")]
        public async Task<IActionResult> InsertCliente(ClienteDTORequest ClienteRequest)
        {         
            return Ok(await _clienteServices.CreateClienteAsync(ClienteRequest));
        }
    }
}
