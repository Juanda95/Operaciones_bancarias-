using AutoMapper;
using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Application.Helpers.Mappings;
using BankOperations.Domain.Entities;
using System.Runtime.Serialization;

namespace BankOperations.UnitTest
{
    public class MappingTest
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public MappingTest()
        {
            _configurationProvider = new MapperConfiguration(cgf =>
            {
                cgf.AddProfile<Mapps>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public void ShouldBevaleConfiguration()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }


        [Theory]
        [InlineData(typeof(ClienteDTORequest), typeof(Cliente))]
        [InlineData(typeof(ClienteDTOUpdateRequest), typeof(Cliente))]
        [InlineData(typeof(CuentaDTORequest), typeof(Cuenta))]
        [InlineData(typeof(CuentaDTOUpdateRequest), typeof(Cuenta))]
        [InlineData(typeof(MovimientoDTORequest), typeof(Movimiento))]
        [InlineData(typeof(MovimientoDTOUpdateRequest), typeof(Movimiento))]
        [InlineData(typeof(Cliente), typeof(ClienteDTOResponse))]
        [InlineData(typeof(Cuenta), typeof(CuentaDTOResponse))]
        [InlineData(typeof(Movimiento), typeof(MovimientoDTOResponse))]
        public void Map_SorceToDestination_ExistConfiguration(Type origin, Type destino)
        {
            var instance = FormatterServices.GetSafeUninitializedObject(origin);

            _mapper.Map(instance, origin, destino);

        }
    }
}
