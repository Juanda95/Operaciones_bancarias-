using BankOperations.Domain.Common;

namespace BankOperations.Domain.Entities
{
    public  class Movimiento : BaseEntiy
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!; 
        public int Valor { get; set; }
        public int Saldo { get; set; }
        public int IdCliente { get; set; }
        public int IdCuenta { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; } = null!; 
        public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
    }
}
