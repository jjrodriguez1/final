namespace DLL
{
    public class TempPedidoPorMesa
    {
        public TempPedidoPorMesa()
        {

        }

        public long Id { get; set; }
        public int IdMesa { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public int StockDisp { get; set; }
    }
}
