using DLL;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IMesaRepository
    {
        bool AltaMesa(Mesa mesa);
        bool ExisteMesa(int nroMesa);
        List<MesasLista> GetMesas();
        bool EliminarMesa(int id);
        Mesa GetMesa(int Id);
        bool AsignarMesa(int idMesaOp, int idMesa, int idOp);
        List<GenericCombo> GetMesasDispOpe(int idOp);
        bool OcuparMesa(int idMesa);
        List<MesasLista> GetMesasOcupadasOperador(int id);
        bool CerrarMesa(int idMesa);
    }
}
