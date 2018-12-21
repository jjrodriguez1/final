namespace DLL
{
    public class TipoProveedor
    {
        public TipoProveedor()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public static int BEBIDAS = 1;
        public static int CARNE = 2;
        public static int PANADERIA = 3;
    }
}
