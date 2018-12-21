using log4net;

namespace Repositorios
{
    public abstract class GenericRepository
    {
        protected ILog _Log = null;

        public GenericRepository(ILog log)
        {
            _Log = log;
        }
    }
}
