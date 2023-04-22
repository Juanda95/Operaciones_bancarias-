using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankOperations.Application.DTOs.Request.Cliente
{
    public class ClienteDTORequest : PersonaDTORequest
    {
        [DisplayName("Cliente id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(10, ErrorMessage = "El Cliente id debe ser menor a 10 caracteres")]
        public string Clienteid { get; set; } 

        [DisplayName("Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "La contraseña debe ser menor a 50 caracteres")]
        public string Contrasena { get; set; } 


        [Required(AllowEmptyStrings = false, ErrorMessage = "El Estado es requerido")]
        public bool Estado { get; set; }
    }
}
