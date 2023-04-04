using BankOperations.Domain.Common;

namespace BankOperations.Domain.Entities
{
    public  class Movimiento : BaseEntiy
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public int Valor { get; set; }
        public int Saldo { get; set; }
        public int IdCliente { get; set; }
        public int IdCuenta { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Cuenta IdCuentaNavigation { get; set; } 
    }
}
