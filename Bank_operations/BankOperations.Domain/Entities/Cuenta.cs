namespace BankOperations.Domain.Entities
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty; 
        public string TipoCuenta { get; set; } = string.Empty;
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int IdCliente { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    }
}
