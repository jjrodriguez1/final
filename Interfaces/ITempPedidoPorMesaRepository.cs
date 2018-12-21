using DLL;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ITempPedidoPorMesaRepository
    {
        bool InsertTempPedidoMesa(int idmesa, int idprod);
        List<TempPedidoPorMesa> GetAllByIdMesa(int id);
        void CerrarMesaPedidos(int idmesa);
        void RemoverItem(TempPedidoPorMesa item);
    }
}
