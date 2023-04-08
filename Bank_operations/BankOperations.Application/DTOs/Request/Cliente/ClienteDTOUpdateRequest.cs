using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request.Cliente
{
    public class ClienteDTOUpdateRequest : ClienteDTORequest
    {
        [DisplayName("Cliente id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cliente id es requerido")]
        public virtual int Id { get; set; }
    }
}
