using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankOperations.Application.DTOs.Request.Cuenta
{
    public class CuentaDTORequest
    {
        [DisplayName("Numero cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Numero cuenta es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El numero cuenta debe ser menor a 50 caracteres")]
        public string NumeroCuenta { get; set; }

        [DisplayName("Tipo cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo cuenta es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El tipo cuenta debe ser menor a 50 caracteres")]
        public string TipoCuenta { get; set; }

        [DisplayName("Saldo inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El saldo inicial es requerido")]
        public int SaldoInicial { get; set; }

        public bool Estado { get; set; }

        [DisplayName("Id cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El id cliente es requerido")]
        public int IdCliente { get; set; }

    }
}
