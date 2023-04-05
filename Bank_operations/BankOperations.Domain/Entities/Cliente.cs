namespace BankOperations.Domain.Entities
{
    public class Cliente : Persona
    {
        public string Clienteid { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; } = null!;
    }
}
