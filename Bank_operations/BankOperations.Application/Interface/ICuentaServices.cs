using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.Interface
{
    public interface ICuentaServices
    {
        Response<List<CuentaDTOResponse>> GetCuentaAll();
        Response<CuentaDTOResponse> GetCuentaById(int Id);
        Task<Response<bool>> CreateCuentaAsync(CuentaDTORequest CuentaRequest);
        Task<Response<int>> DeleteCuentaAsync(int Id);
        Task<Response<int>> UpdateCuentaAsync(CuentaDTOUpdateRequest CuentaUpdateRequest);
    }
} 
