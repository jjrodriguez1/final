using Interfaces;
using log4net;

namespace Repositorios
{
    public class ProveedorRepository : GenericRepository, IProveedorRepository
    {
        public ProveedorRepository(ILog log) : base(log)
        {

        }
    }
}
