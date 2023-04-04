using BankOperations.Application.DTOs.Request;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.Interface
{
    public  interface IClienteServices
    {
        Response<List<ClienteDTOResponse>> GetClienteAll();
        Response<ClienteDTOResponse> GetClienteById();
        Task<Response<int>> CreateClienteAsync(ClienteDTORequest ClienteRequest);
        Task<Response<int>> DeleteClienteAsync();
        Task<Response<int>> UpdateClienteAsync();
    }
}
