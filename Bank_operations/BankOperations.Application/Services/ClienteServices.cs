using AutoMapper;
using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Services.Interface;
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
        public async Task<Response<bool>> CreateClienteAsync(ClienteDTORequest clienteRequest)
        {
            Cliente nuevoCliente = _mapper.Map<Cliente>(clienteRequest);
            _unitOfWork.ClienteRepository.Insert(nuevoCliente);
            bool save = await _unitOfWork.Save() > 0;
            return save ? new Response<bool>(save) :
                throw new Exception($"Acurrido un error en el proceso de guardado");
        }

        public async Task<Response<int>> DeleteClienteAsync(int id)
        {
            Cliente cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(id));

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");
            }
            else
            {
                _unitOfWork.ClienteRepository.Delete(id);
                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(id) :
                    throw new Exception($"Acurrido un error en el proceso de Eliminado por favor intente de nuevo");

            }
        }

        public Response<List<ClienteDTOResponse>> GetClienteAll()
        {
            List<Cliente> clientes = _unitOfWork.ClienteRepository.GetAll().ToList();
            List<ClienteDTOResponse> clientesDTO = _mapper.Map<List<ClienteDTOResponse>>(clientes);
            return new Response<List<ClienteDTOResponse>>(clientesDTO);
        }

        public Response<ClienteDTOResponse> GetClienteById(int id)
        {
            Cliente cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(id));
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {id}");
            }
            else
            {
                ClienteDTOResponse clienteDto = _mapper.Map<ClienteDTOResponse>(cliente);
                return new Response<ClienteDTOResponse>(clienteDto);
            }
        }

        public async Task<Response<int>> UpdateClienteAsync(ClienteDTOUpdateRequest clienteUpdateRequest)
        {

            Cliente cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(clienteUpdateRequest.Id));

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {clienteUpdateRequest.Id}");
            }
            else
            {
                cliente = _mapper.Map<Cliente>(clienteUpdateRequest);

                _unitOfWork.ClienteRepository.Update(cliente);

                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(cliente.Id) :
                    throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
            }
        }
    }
    #endregion
}

