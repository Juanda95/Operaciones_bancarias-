using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Request
{
    public  class ClienteDTORequest : PersonaDTORequest
    {
        public string Clienteid { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
