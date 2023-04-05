using BankOperations.Application.DTOs.Common;
using BankOperations.Application.DTOs.Request;
using BankOperations.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

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
        public async Task<IActionResult> InsertCliente(ClienteDTORequest ClienteRequest)
        {
            return Ok(await _clienteServices.CreateClienteAsync(ClienteRequest));
        }

        //PUT 
        [HttpPut]
        [Route("UpdateCliente")]
        public async Task<IActionResult> UpdateCliente(ClienteDTOUpdateRequest ClienteUpdateRequest)
        {

            return Ok(await _clienteServices.UpdateClienteAsync(ClienteUpdateRequest));

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
