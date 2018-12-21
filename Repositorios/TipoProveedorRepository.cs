using Interfaces;
using log4net;

namespace Repositorios
{
    public class TipoProveedorRepository: GenericRepository, ITipoProveedorRepository
    {
        public TipoProveedorRepository(ILog log) : base(log)
        {

        }
    }
}
