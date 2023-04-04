namespace BankOperations.Domain.Entities
{
    public class Cliente : Persona
    {
        public string Clienteid { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; } = new List<Cuenta>();
        public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    }
}
