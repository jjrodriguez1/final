using Interfaces;
using log4net;

namespace Repositorios
{
    public class MedioPagoRepository: GenericRepository, IMedioPagoRepository
    {
        public MedioPagoRepository(ILog log) : base(log)
        {

        }
    }
}
