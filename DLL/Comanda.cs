using System;

namespace DLL
{
    public class Comanda
    {
        public Comanda()
        {

        }

        public long Id { get; set; }
		public string Menu {get; set;}
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdEstado { get; set; }
		public int IdOperador { get; set; }
    }
}
