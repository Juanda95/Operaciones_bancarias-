﻿namespace BankOperations.Application.DTOs.Response
{
    public class ClienteDTOResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 

        public string Genero { get; set; } 

        public int Edad { get; set; }

        public string Identificacion { get; set; }

        public string Direccion { get; set; } 

        public string Telefono { get; set; } 

        public string Clienteid { get; set; } 

        public string Contrasena { get; set; } 

        public bool Estado { get; set; }
    }
}
