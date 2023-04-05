using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Common
{
    public class IdDTOCommon
    {
        [DisplayName("Cliente id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Cliente id es requerido")]
        public int Id { get; set; }
    }
}
