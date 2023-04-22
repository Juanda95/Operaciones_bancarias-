namespace BankOperations.Domain.Entities
{
    public class Cliente : Persona
    {
        public string Clienteid { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; }
        public virtual ICollection<Movimiento> Movimientos { get; }
    }
}
