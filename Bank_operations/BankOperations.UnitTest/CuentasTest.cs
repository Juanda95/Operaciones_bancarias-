using AutoMapper;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Mappings;
using BankOperations.Application.Services;
using BankOperations.Domain.Entities;
using BankOperations.Persistence.UnitOfWork.Interface;
using Moq;

namespace BankOperations.UnitTest
{
    public class CuentasTest
    {
        #region Attribute
        private readonly CuentaServices _cuentaServices;
        private readonly Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;
        #endregion

        #region Builder
        public CuentasTest()
        {
            var configuration = new MapperConfiguration(cgf =>
            {
                cgf.AddProfile<Mapps>();
            });

            _mapper = configuration.CreateMapper();
            _cuentaServices = new CuentaServices(_UnitOfWork.Object, _mapper);

            #region _UnitOfWork.Setup

            _UnitOfWork.Setup(x => x.CuentaRepository.GetAll()).Returns(new List<Cuenta>()
                {
                   new Cuenta {
                       Id = 2,
                      NumeroCuenta = "987654",
                      TipoCuenta ="Corriente",
                      SaldoInicial = 400,
                      Estado = true,
                      IdCliente = 2,

                   },
                   new Cuenta {
                       Id = 1,
                      NumeroCuenta = "123456789",
                      TipoCuenta ="Ahorros",
                      SaldoInicial = 200,
                      Estado = true,
                      IdCliente = 1,

                   }
                }
           );

            _UnitOfWork.Setup(x => x.CuentaRepository.FirstOrDefault(x => x.Id.Equals(2))).Returns(new Cuenta
            {
                Id = 2,
                NumeroCuenta = "987654",
                TipoCuenta = "Corriente",
                SaldoInicial = 400,
                Estado = true,
                IdCliente = 2,

            }
            );

            _UnitOfWork.Setup(x => x.CuentaRepository.FirstOrDefault(x => x.Id.Equals(1))).Returns(new Cuenta
            {
                Id = 1,
                NumeroCuenta = "123456789",
                TipoCuenta = "Ahorros",
                SaldoInicial = 200,
                Estado = true,
                IdCliente = 1,

            }
           );
            #endregion

        }
        #endregion

        #region Methods
        [Fact]
        public void GetCuentaAll_TypeResponse()
        {

            //Act
            var ResultCuentas = _cuentaServices.GetCuentaAll();
            //Assert
            Assert.IsType<List<CuentaDTOResponse>>(ResultCuentas.Data);
        }

        [Fact]
        public void GetAllCuentass_ShouldReturnAllCuentas()
        {
            //Arrange
            int cantidadCuentassExpected = 2;
            //Act
            var ResultCuentas = _cuentaServices.GetCuentaAll();
            //Assert
            Assert.Equal(cantidadCuentassExpected, ResultCuentas.Data.Count());
        }


        [Theory]
        [InlineData(1, "123456789")]
        public void GetCuentaById_ShouldReturnCorrectCuenta(int Id, string Expectednumerocuenta)
        {

            //Act
            var ResultCuenta = _cuentaServices.GetCuentaById(Id);
            //Assert
            Assert.Equal(Expectednumerocuenta, ResultCuenta.Data.NumeroCuenta);
        }

        [Fact]
        public void GetCuentaById_TypeResponse()
        {
            //Arrange
            int Id = 2;
            //Act
            var ResultCuenta = _cuentaServices.GetCuentaById(Id);
            //Assert
            Assert.IsType<CuentaDTOResponse>(ResultCuenta.Data);
        }

        [Fact]
        public void GetCuentaById_KeyNotFoundException()
        {
            //Arrange
            int Id = 3;

            //Act
            Action act = () => _cuentaServices.GetCuentaById(Id);

            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }

        [Fact]
        public async void DeleteCuentaAsync_TypeResponse()
        {
            //Arrange

            int Id = 1;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Delete(Id));
            //Act
            var ResultCuentas = await _cuentaServices.DeleteCuentaAsync(Id);
            //Assert
            Assert.IsType<int>(ResultCuentas.Data);
        }


        [Fact]
        public async void DeleteCuentaAsync_ShouldReturnCorrectIdDeleted()
        {
            //Arrange

            int Id = 1;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Delete(Id));
            //Act
            var ResultCuentas = await _cuentaServices.DeleteCuentaAsync(Id);
            //Assert
            Assert.Equal(Id, ResultCuentas.Data);
        }


        [Fact]
        public async void DeleteCuentaAsync_SaveTestException()
        {
            //Arrange

            int Id = 1;
            int responseSave = 0;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Delete(Id));
            //Act
            Func<Task> act = () => _cuentaServices.DeleteCuentaAsync(Id);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public async void DeleteCuentaAsync_KeyNotFoundException()
        {
            //Arrange

            int Id = 4;
            int responseSave = 1;
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Delete(Id));
            //Act
            Func<Task> act = () => _cuentaServices.DeleteCuentaAsync(Id);

            //Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }


        [Fact]
        public async void UpdateCuentaAsync_TypeResponse()
        {
            //Arrange
            CuentaDTOUpdateRequest CuentaUpdateRequest = new CuentaDTOUpdateRequest()
            {
                Id = 2,
                NumeroCuenta = "987654",
                TipoCuenta = "Corriente",
                Estado = true,
            };

            _UnitOfWork.Setup(x => x.CuentaRepository.FirstOrDefault(x => x.Id.Equals(CuentaUpdateRequest.Id))).Returns(new Cuenta
            {
                Id = 2,
                NumeroCuenta = "987654",
                TipoCuenta = "Corriente",
                SaldoInicial = 400,
                Estado = true,
                IdCliente = 2
                
            }
           );

            int responseSave = 1;
            var cuenta = _mapper.Map<Cuenta>(CuentaUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Update(cuenta));
            //Act
            var ResultCuentas = await _cuentaServices.UpdateCuentaAsync(CuentaUpdateRequest);
            //Assert
            Assert.IsType<int>(ResultCuentas.Data);
        }

        [Fact]
        public async void UpdateCuentaAsync_ShouldReturnCorrectIdDeleted()
        {
            //Arrange
            CuentaDTOUpdateRequest CuentaUpdateRequest = new CuentaDTOUpdateRequest()
            {
                Id = 2,
                NumeroCuenta = "987654",
                TipoCuenta = "Corriente",
                Estado = true,
            };

            _UnitOfWork.Setup(x => x.CuentaRepository.FirstOrDefault(x => x.Id.Equals(CuentaUpdateRequest.Id))).Returns(new Cuenta
            {
                Id = 2,
                NumeroCuenta = "987654",
                TipoCuenta = "Corriente",
                SaldoInicial = 400,
                Estado = true,
                IdCliente = 2

            }
           );

            int responseSave = 1;
            var cuenta = _mapper.Map<Cuenta>(CuentaUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Update(cuenta));
            //Act
            var ResultCuentas = await _cuentaServices.UpdateCuentaAsync(CuentaUpdateRequest);
            //Assert
            Assert.Equal<int>(ResultCuentas.Data, ResultCuentas.Data);
        }

        [Fact]
        public async void UpdateCuentaAsync_KeyNotFoundException()
        {
            //Arrange
            CuentaDTOUpdateRequest CuentaUpdateRequest = new CuentaDTOUpdateRequest()
            {
                Id = 2,
                NumeroCuenta = "12321",
                TipoCuenta = "Ahorros",
                Estado = true,
            };


            int responseSave = 1;
            var Cuenta = _mapper.Map<Cuenta>(CuentaUpdateRequest);
            _UnitOfWork.Setup(x => x.Save()).ReturnsAsync(responseSave);
            _UnitOfWork.Setup(x => x.CuentaRepository.Update(Cuenta));
            //Act
            Func<Task> act = () => _cuentaServices.UpdateCuentaAsync(CuentaUpdateRequest);

            //Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(act);
            Assert.IsType<KeyNotFoundException>(exception);
        }

        #endregion
    }
}
