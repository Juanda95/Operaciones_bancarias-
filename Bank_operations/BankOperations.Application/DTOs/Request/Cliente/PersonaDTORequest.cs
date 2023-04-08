using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request.Cliente
{
    public class PersonaDTORequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El nombre debe ser menor a 50 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El genero es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El genero debe ser menor a 50 caracteres")]
        public string Genero { get; set; } = null!;

        public int Edad { get; set; }

        [DisplayName("Identificacón")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Identificacón es requerida")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "La Identificacón debe ser menor a 15 caracteres")]
        public string Identificacion { get; set; } = null!;

        [DisplayName("Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La Dirección es requerida")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(120, ErrorMessage = "La Dirección debe ser menor a 50 caracteres")]
        public string Direccion { get; set; } = null!;

        [DisplayName("Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Teléfono es requerido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "El Teléfono debe ser menor a 15 caracteres")]
        public string Telefono { get; set; } = null!;
    }
}
