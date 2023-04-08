using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Response
{
    public class CuentaDTOResponse
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int IdCliente { get; set; }
    }
}
