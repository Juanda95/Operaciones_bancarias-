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
        public string Nombre { get; set; } = null!;

        public string Genero { get; set; } = null!;

        public int Edad { get; set; }

        public string Identificacion { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Clienteid { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
