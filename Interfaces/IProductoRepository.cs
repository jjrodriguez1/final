using DLL;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IProductoRepository
    {
        bool AltaProducto(Producto producto);
        bool ModificarProducto(Producto producto);
        List<ProductosLista> GetProductos();
        Producto GetOperadorById(int Id);
        void DescontarCantidad(int cant, int idprod);
    }
}
