namespace DLL
{
    public class TipoOperador
    {
        public TipoOperador()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public static int GERENTE = 1;
        public static int MOZO = 2;
        public static int COCINA = 3;
    }
}
