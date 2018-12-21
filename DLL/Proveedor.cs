namespace DLL
{
    public class Proveedor
    {
        public Proveedor()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoProveedor { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
