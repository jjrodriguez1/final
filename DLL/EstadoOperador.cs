namespace DLL
{
    public class EstadoOperador
    {
        public EstadoOperador()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public static int ACTIVO = 1;
		public static int BAJA = 2;
        public static int SUSPENDIDO = 3;
    }
}
