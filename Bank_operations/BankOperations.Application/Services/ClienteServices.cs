using AutoMapper;
using BankOperations.Application.DTOs.Request.Cliente;
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
        public async Task<Response<int>> CreateClienteAsync(ClienteDTORequest ClienteRequest)
        {
            try
            {
                Cliente nuevoCliente = _mapper.Map<Cliente>(ClienteRequest);
                Cliente data = _unitOfWork.ClienteRepository.Add(nuevoCliente);
                bool save = await _unitOfWork.Save() > 0;
                return save ? new Response<int>(data.Id) : throw new Exception($"Acurrido un error en el proceso de guardado");
            }
            catch (Exception ex)
            {
                //crear auditoria 
                throw new Exception(ex.Message);
            }

        }

        public async Task<Response<int>> DeleteClienteAsync(int Id)
        {
          
            try
            {
                Cliente Cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(Id));

                if (Cliente == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");
                }
                else
                {
                    _unitOfWork.ClienteRepository.Delete(Id);
                    bool save = await _unitOfWork.Save() > 0;
                    return save ? new Response<int>(Id) : throw new Exception($"Acurrido un error en el proceso de Eliminado por favor intente de nuevo");

                }

            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Response<List<ClienteDTOResponse>> GetClienteAll()
        {
            try
            {
                List<Cliente> Clientes = _unitOfWork.ClienteRepository.GetAll().ToList();
                List<ClienteDTOResponse> clientesDTO = _mapper.Map<List<ClienteDTOResponse>>(Clientes);
                return new Response<List<ClienteDTOResponse>>(clientesDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public Response<ClienteDTOResponse> GetClienteById(int Id)
        {
            try
            {
                Cliente cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(Id));
                if (cliente == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {Id}");
                }
                else
                {
                    ClienteDTOResponse ClienteDto = _mapper.Map<ClienteDTOResponse>(cliente);
                    return new Response<ClienteDTOResponse>(ClienteDto);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<Response<int>> UpdateClienteAsync(ClienteDTOUpdateRequest ClienteUpdateRequest)
        {
            try
            {
                Cliente Cliente = _unitOfWork.ClienteRepository.FirstOrDefault(x => x.Id.Equals(ClienteUpdateRequest.Id));

                if (Cliente == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {ClienteUpdateRequest.Id}");
                }
                else
                {
                     Cliente = _mapper.Map<Cliente>(ClienteUpdateRequest);

                    _unitOfWork.ClienteRepository.Update(Cliente);

                    bool save = await _unitOfWork.Save() > 0;
                    return save ? new Response<int>(Cliente.Id) : throw new Exception($"Acurrido un error en el proceso de Actualizacion intente nuevamente");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        #endregion

    }
}
