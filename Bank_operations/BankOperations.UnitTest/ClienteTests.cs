using AutoMapper;
using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Mappings;
using BankOperations.Application.Helpers.Wrappers;
using BankOperations.Application.Services;
using BankOperations.Domain.Entities;
using BankOperations.Persistence.UnitOfWork.Interface;
using Moq;

namespace BankOperations.UnitTest
{
    public class ClienteTests
    {
        #region Attribute
        private readonly ClienteServices _clienteServices;
        private readonly Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public ClienteTests()
        {
            var configuration = new MapperConfiguration(cgf =>
            {
                cgf.AddProfile<Mapps>();
            });

            _mapper = configuration.CreateMapper();
            _clienteServices = new ClienteServices(_UnitOfWork.Object, _mapper);

            #region _UnitOfWork.Setup
            _UnitOfWork.Setup(x => x.ClienteRepository.GetAll()).Returns(new List<Cliente>()
                {
                   new Cliente {
                       Id = 1,
                       Nombre = "juan",
                       Genero = "Masculino",
                       Edad = 25,
                       Identificacion = "10949518845",
                       Direccion = "villa MF casa 23",
                       Telefono = "3116707345",
                       Clienteid = "1",
                       Contrasena= "123456789",
                       Estado = true
                   },
                   new Cliente {
                       Id = 2,
                       Nombre = "stefa",
                       Genero = "Femenino",
                       Edad = 24,
                       Identificacion = "109567832",
                       Direccion = "avenida bolivar 45-32",
                       Telefono = "31245678321",
                       Clienteid = "2",
                       Contrasena= "123456789",
                       Estado = true
                   }
                }
           );

            _UnitOfWork.Setup(x => x.ClienteRepository.FirstOrDefault(x => x.Id.Equals(1))).Returns(new Cliente
            {
                Id = 1,
                Nombre = "juan",
                Genero = "Masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345",
                Clienteid = "1",
                Contrasena = "123456789",
                Estado = true
            }
            );          

            _UnitOfWork.Setup(x => x.ClienteRepository.FirstOrDefault(x => x.Id.Equals(2))).Returns(new Cliente
            {
                Id = 2,
                Nombre = "stefa",
                Genero = "Femenino",
                Edad = 24,
                Identificacion = "109567832",
                Direccion = "avenida bolivar 45-32",
                Telefono = "31245678321",
                Clienteid = "2",
                Contrasena = "123456789",
                Estado = true
            }
           );
            #endregion

        }
        #endregion

        #region Methods
        [Fact]
        public void GetAllClientes_TypeResponse()
        {

            //Act
            var ResultClientes = _clienteServices.GetClienteAll();
            //Assert
            Assert.IsType<List<ClienteDTOResponse>>(ResultClientes.Data);
        }

        [Fact]
        public void GetAllClientes_ShouldReturnAllClientes()
        {
            //Arrange
            int cantidadClientesExpected = 2;
            //Act
            var ResultClientes = _clienteServices.GetClienteAll();
            //Assert
            Assert.Equal(cantidadClientesExpected, ResultClientes.Data.Count());
        }


        [Theory]
        [InlineData(1, "juan")]
        [InlineData(2, "stefa")]
        public void GetClienteById_ShouldReturnCorrectCliente(int Id, string ExpectednameClientes)
        {

            //Act
            var ResultClientes = _clienteServices.GetClienteById(Id);
            //Assert
            Assert.Equal(ExpectednameClientes, ResultClientes.Data.Nombre);
        }

        [Fact]
        public void GetClienteById_TypeResponse()
        {
            //Arrange
            int Id = 2;
            //Act
            var ResultClientes = _clienteServices.GetClienteById(Id);
            //Assert
            Assert.IsType<ClienteDTOResponse>(ResultClientes.Data);
        }

        [Fact]
        public void GetClienteById_KeyNotFoundException()
        {
            //Arrange
            int Id = 3;

            //Act
            Action act = () => _clienteServices.GetClienteById(Id);

            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }

        [Theory]
        [InlineData("stefa", "femenino", 27, "1234534e", "avenida bolivar 45-norte", "322456234", "1232132", "23423", true)]
        [InlineData("juan", "masculino", 30, "123412343", "villa1", "322456", "12321", "2342345", true)]
        [InlineData("Carlos", "femenino", 21, "12345123432", "villa1", "322456", "12321", "32452342", true)]
        public async void CreateClienteAsyncTest(string nombre, string genero, int edad, string identificacion, string Direccion, string telefono, string clienteId, string contrasena, bool estado)
        {
            //Arrange
            ClienteDTORequest Cliente = new ClienteDTORequest()
            {
                Nombre = nombre,
                Genero = genero,
                Edad = edad,
                Identificacion = identificacion,
                Direccion = Direccion,
                Telefono = telefono,
                Clienteid = clienteId,
                Contrasena = contrasena,
                Estado = estado
            };

            var nuevoCliente = _mapper.Map<Cliente>(Cliente);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(1);
            _UnitOfWork.Setup(x => x.ClienteRepository.Insert(nuevoCliente));

            //Act
            var Result = await _clienteServices.CreateClienteAsync(Cliente);

            //Assert
            Assert.True(Result.Data);

        }

        [Theory]
        [InlineData("Carlos", "femenino", 21, "12345123432", "villa1", "322456", "12321", "32452342", true)]
        public async void CreateClienteAsync_ExceptionSaveTest(string nombre, string genero, int edad, string identificacion, string Direccion, string telefono, string clienteId, string contrasena, bool estado)
        {
            //Arrange
            ClienteDTORequest Cliente = new ClienteDTORequest()
            {
                Nombre = nombre,
                Genero = genero,
                Edad = edad,
                Identificacion = identificacion,
                Direccion = Direccion,
                Telefono = telefono,
                Clienteid = clienteId,
                Contrasena = contrasena,
                Estado = estado
            };

            var nuevoCliente = _mapper.Map<Cliente>(Cliente);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
            _UnitOfWork.Setup(x => x.ClienteRepository.Insert(nuevoCliente));

            //Act
            Func<Task> act = () => _clienteServices.CreateClienteAsync(Cliente);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public async void DeleteClienteAsync_TypeResponse()
        {
            //Arrange

            int Id = 1;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Delete(Id));
            //Act
            var ResultClientes = await _clienteServices.DeleteClienteAsync(Id);
            //Assert
            Assert.IsType<int>(ResultClientes.Data);
        }


        [Fact]
        public async void DeleteClienteAsync_ShouldReturnCorrectIdDeleted()
        {
            //Arrange

            int Id = 1;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Delete(Id));
            //Act
            var ResultClientes = await _clienteServices.DeleteClienteAsync(Id);
            //Assert
            Assert.Equal(Id, ResultClientes.Data);
        }


        [Fact]
        public async void DeleteClienteAsync_SaveTestException()
        {
            //Arrange

            int Id = 1;
            int responseSave = 0;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Delete(Id));
            //Act
            Func<Task> act = () => _clienteServices.DeleteClienteAsync(Id);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public async void DeleteClienteAsync_KeyNotFoundException()
        {
            //Arrange

            int Id = 4;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Delete(Id));
            //Act
            Func<Task> act = () => _clienteServices.DeleteClienteAsync(Id);

            //Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }


        [Fact]
        public async void UpdateClienteAsync_TypeResponse()
        {
            //Arrange
            ClienteDTOUpdateRequest ClienteUpdateRequest = new ClienteDTOUpdateRequest()
            {
                Id = 1,
                Clienteid = "123456789",
                Contrasena = "987654321",
                Estado = false,
                Nombre = "juan",
                Genero = "masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345"
            };

            _UnitOfWork.Setup(x => x.ClienteRepository.FirstOrDefault(x => x.Id.Equals(ClienteUpdateRequest.Id))).Returns(new Cliente
            {
                Id = 1,
                Nombre = "juan",
                Genero = "Masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345",
                Clienteid = "1",
                Contrasena = "123456789",
                Estado = true
            }
           );

            int responseSave = 1;
            var Cliente = _mapper.Map<Cliente>(ClienteUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Update(Cliente));
            //Act
            var ResultClientes = await _clienteServices.UpdateClienteAsync(ClienteUpdateRequest);
            //Assert
            Assert.IsType<int>(ResultClientes.Data);
        }

        [Fact]
        public async void UpdateClienteAsync_ShouldReturnCorrectIdDeleted()
        {
            //Arrange
            ClienteDTOUpdateRequest ClienteUpdateRequest = new ClienteDTOUpdateRequest()
            {
                Id = 1,
                Clienteid = "123456789",
                Contrasena = "987654321",
                Estado = false,
                Nombre = "juan",
                Genero = "masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345"
            };

            _UnitOfWork.Setup(x => x.ClienteRepository.FirstOrDefault(x => x.Id.Equals(ClienteUpdateRequest.Id))).Returns(new Cliente
            {
                Id = 1,
                Nombre = "juan",
                Genero = "Masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345",
                Clienteid = "1",
                Contrasena = "123456789",
                Estado = true
            }
           );

            int responseSave = 1;
            var Cliente = _mapper.Map<Cliente>(ClienteUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Update(Cliente));
            //Act
            var ResultClientes = await _clienteServices.UpdateClienteAsync(ClienteUpdateRequest);
            //Assert
            Assert.Equal<int>(ClienteUpdateRequest.Id, ResultClientes.Data);
        }

        [Fact]
        public async void UpdateClienteAsync_KeyNotFoundException()
        {
            //Arrange
            ClienteDTOUpdateRequest ClienteUpdateRequest = new ClienteDTOUpdateRequest()
            {
                Id = 4,
                Clienteid = "123456789",
                Contrasena = "987654321",
                Estado = false,
                Nombre = "juan",
                Genero = "masculino",
                Edad = 25,
                Identificacion = "10949518845",
                Direccion = "villa MF casa 23",
                Telefono = "3116707345"
            };


            int responseSave = 1;
            var Cliente = _mapper.Map<Cliente>(ClienteUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.ClienteRepository.Update(Cliente));
            //Act
            Func<Task> act = () => _clienteServices.UpdateClienteAsync(ClienteUpdateRequest);

            //Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }
     
        #endregion
    }
}
