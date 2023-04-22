namespace BankOperations.Application.DTOs.Response
{
    public class MovimientoDTOResponse
    {
        public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; }

        public int Valor { get; set; }

        public int Saldo { get; set; }

        public int IdCliente { get; set; }

        public int IdCuenta { get; set; }
    }
}
