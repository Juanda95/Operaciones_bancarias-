using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.DTOs.Response
{
    public class ClienteDTOResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        public int Edad { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Clienteid { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;
    }
}
