using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request.Cuenta
{
    public class CuentaDTOUpdateRequest
    {
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "El id es requerido")]
        public int Id { get; set; }

        [DisplayName("Numero Cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Numero Cuenta es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El Numero Cuenta debe ser menor a 50 caracteres")]
        public string NumeroCuenta { get; set; } = null!;

        [DisplayName("Tipo cuenta")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo cuenta es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El tipo cuenta debe ser menor a 50 caracteres")]
        public string TipoCuenta { get; set; } = null!;


        public bool Estado { get; set; }
    }
}
