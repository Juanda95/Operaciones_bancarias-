namespace BankOperations.Application.DTOs.Response
{
    public class MovimientoClienteReporteGeneralDTOResponse
    {
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string numeroCuenta { get; set; }
        public string tipo { get; set; }
        public int SaldoInicial { get; set; }
        public string Estado { get; set; }
        public int Movimiento { get; set; }
        public int SaldoDisponible { get; set; }

    }
}
