using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Response
{
    public class MovimientoClienteReporteGeneralDTOResponse
    {
        public string Fecha { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string numeroCuenta { get; set; } = null!;
        public string tipo { get; set; } = null!;
        public int SaldoInicial { get; set; }
        public string Estado { get; set; } = null!;
        public int Movimiento { get; set; }
        public int SaldoDisponible { get; set; }

    }
}
