﻿using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;

namespace BankOperations.Application.Services.Interface
{
    public interface IClienteServices
    {
        Response<List<ClienteDTOResponse>> GetClienteAll();
        Response<ClienteDTOResponse> GetClienteById(int Id);
        Task<Response<bool>> CreateClienteAsync(ClienteDTORequest ClienteRequest);
        Task<Response<int>> DeleteClienteAsync(int Id);
        Task<Response<int>> UpdateClienteAsync(ClienteDTOUpdateRequest ClienteUpdateRequest);
    }
}
