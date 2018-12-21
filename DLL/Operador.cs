using System;

namespace DLL
{
    public class Operador
    {
        public Operador() { }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public int EstadoId { get; set; }
		public int IdTipoOperador {get; set;}
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
