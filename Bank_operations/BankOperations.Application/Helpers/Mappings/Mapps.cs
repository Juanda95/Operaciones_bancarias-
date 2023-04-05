using AutoMapper;
using BankOperations.Application.DTOs.Request;
using BankOperations.Application.DTOs.Response;
using BankOperations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.Helpers.Mappings
{
    public class Mapps : Profile
    {
        public Mapps()
        {
            #region Entity
            CreateMap<ClienteDTORequest, Cliente>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.Cuenta, opt => opt.Ignore());

            CreateMap<ClienteDTOUpdateRequest, Cliente>()
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.Cuenta, opt => opt.Ignore());
            //CreateMap<CreateCuentaCommand, Cuenta>()
            //    .ForMember(x => x.Id, opt => opt.Ignore())
            //    .ForMember(x => x.Movimientos, opt => opt.Ignore())
            //    .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore());

            //CreateMap<CreateMovimientoCommand, Movimiento>()
            //    .ForMember(x => x.Id, opt => opt.Ignore())
            //    .ForMember(x => x.TipoMovimiento, opt => opt.Ignore())
            //    .ForMember(x => x.Saldo, opt => opt.Ignore())
            //    .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore())
            //    .ForMember(x => x.IdCuentaNavigation, opt => opt.Ignore());
            #endregion


            #region DTOs
            CreateMap<Cliente, ClienteDTOResponse>();
            //CreateMap<Cuenta, CuentaDTO>();
            //CreateMap<Movimiento, MovimientoDTO>();
            #endregion
        }

    }
}
