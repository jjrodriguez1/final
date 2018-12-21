namespace DLL
{
    public class MedioPago
    {
        public MedioPago()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public static int EFECTIVO = 1;
        public static int TARJETA = 2;
        public static int CHEQUE = 2;
        public static int DIGITAL = 3;
    }
}
