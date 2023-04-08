using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request.Movimiento
{
    public class MovimientoDTORequest
    {
        public DateTime Fecha { get; set; }
        public int Valor { get; set; }
        public int IdCliente { get; set; }
        public int IdCuenta { get; set; }

    }
}
