using AutoMapper;
using BankOperations.Application.DTOs.Request.Cliente;
using BankOperations.Application.DTOs.Request.Cuenta;
using BankOperations.Application.DTOs.Request.Movimiento;
using BankOperations.Application.DTOs.Response;
using BankOperations.Domain.Entities;

namespace BankOperations.Application.Helpers.Mappings
{
    public class Mapps : Profile
    {
        public Mapps()
        {
            #region Entity Request
            CreateMap<ClienteDTORequest, Cliente>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.Cuenta, opt => opt.Ignore());

            CreateMap<ClienteDTOUpdateRequest, Cliente>()
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.Cuenta, opt => opt.Ignore());

            CreateMap<CuentaDTORequest, Cuenta>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore());

            CreateMap<CuentaDTOUpdateRequest, Cuenta>()
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore())
                .ForMember(x => x.SaldoInicial, opt => opt.Ignore())
                .ForMember(x => x.IdCliente, opt => opt.Ignore());

            CreateMap<MovimientoDTORequest, Movimiento>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.TipoMovimiento, opt => opt.Ignore())
                .ForMember(x => x.Saldo, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore())
                .ForMember(x => x.IdCuentaNavigation, opt => opt.Ignore());

            CreateMap<MovimientoDTOUpdateRequest, Movimiento>()
                .ForMember(x => x.TipoMovimiento, opt => opt.Ignore())
                .ForMember(x => x.Saldo, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore())
                .ForMember(x => x.IdCuentaNavigation, opt => opt.Ignore())
                .ForMember(x => x.Fecha, opt => opt.Ignore())
                .ForMember(x => x.IdCliente, opt => opt.Ignore())
                .ForMember(x => x.IdCuenta, opt => opt.Ignore());
            #endregion


            #region DTOs Response
            CreateMap<Cliente, ClienteDTOResponse>();
            CreateMap<Cuenta, CuentaDTOResponse>();
            CreateMap<Movimiento, MovimientoDTOResponse>();
            #endregion
        }

    }
}
