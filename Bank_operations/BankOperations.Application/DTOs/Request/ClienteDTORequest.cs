using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request
{
    public  class ClienteDTORequest : PersonaDTORequest
    {
        [DisplayName("Cliente id")]
        [Required(AllowEmptyStrings = false,ErrorMessage ="El nombre es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(10, ErrorMessage = "El Cliente id debe ser menor a 10 caracteres")]
        public string Clienteid { get; set; } = null!;

        [DisplayName("Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "La contraseña debe ser menor a 50 caracteres")]
        public string Contrasena { get; set; } = null!;

        
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
