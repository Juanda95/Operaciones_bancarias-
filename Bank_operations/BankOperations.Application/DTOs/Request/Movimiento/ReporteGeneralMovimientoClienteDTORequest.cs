namespace BankOperations.Application.DTOs.Request.Movimiento
{
    public class ReporteGeneralMovimientoClienteDTORequest
    {
        public int ClienteId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
