using DLL;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IComandaRepository
    {
        bool InsertComanda(Comanda comanda);
        bool InsertComandaOperador(int IdOperador, int IdComanda);
        void InsertarTicket(int idmesa,int nromesa, Operador operador, decimal total, string descripcion);
        List<Comanda> GetComandasUno();
        List<Comanda> GetComandasDos();
        void CambiarEstado(Comanda com, string estado);
        List<ComandaReport> ComandaReporte(DateTime desde, DateTime hasta);
    }
}
