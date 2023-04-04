using AutoMapper;
using BankOperations.Application.DTOs.Request;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Interface;
using BankOperations.Domain.Entities;
using BankOperations.Persistence.UnitOfWork.Interface;

namespace BankOperations.Application.Services
{
    public class ClienteServices : IClienteServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public ClienteServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        #endregion
        public async Task<Response<int>> CreateClienteAsync(ClienteDTORequest ClienteRequest)
        {
            Cliente nuevoCliente = _mapper.Map<Cliente>(ClienteRequest);
            Cliente data = _unitOfWork.ClienteRepository.Add(nuevoCliente);
            bool save = await _unitOfWork.Save() > 0;

            return save ? new Response<int>(data.Id): throw new Exception($"No se pudo Guardar el dato");

        } 

        public Task<Response<int>> DeleteClienteAsync()
        {
            throw new NotImplementedException();
        }

        public Response<List<ClienteDTOResponse>> GetClienteAll()
        {
            throw new NotImplementedException();
        }

        public Response<ClienteDTOResponse> GetClienteById()
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> UpdateClienteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
