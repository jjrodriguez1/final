using DLL;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IEstadoOperadorRepository
    {
        List<EstadoOperador> GetAllEstados();
    }
}
