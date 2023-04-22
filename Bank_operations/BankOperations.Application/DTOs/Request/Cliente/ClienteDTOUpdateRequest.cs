using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankOperations.Application.DTOs.Request.Cliente
{
    public class ClienteDTOUpdateRequest : ClienteDTORequest
    {
        [DisplayName("Cliente id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cliente id es requerido")]
        public virtual int Id { get; set; }
    }
}
