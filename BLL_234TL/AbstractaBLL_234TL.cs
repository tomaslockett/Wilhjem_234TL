using DAL_234TL;

namespace BLL_234TL
{
    public abstract class AbstractaBLL_234TL<T, TKey> : RepositorioDAL_234TL<T, TKey>
    {
        protected RepositorioDAL_234TL<T, TKey> _repositorio;

        protected AbstractaBLL_234TL(RepositorioDAL_234TL<T, TKey> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual void Eliminar(T entity)
        {
            _repositorio.Eliminar(entity);
        }

        public virtual IList<T> GetAll()
        {
            return _repositorio.GetAll();
        }

        public virtual T GetbyPrimary(T entity)
        {
            return _repositorio.GetbyPrimary(entity);
        }

        public virtual T GetbyPrimaryKey(TKey id)
        {
            return _repositorio.GetbyPrimaryKey(id);
        }

        public virtual void Guardar(T entity)
        {
            _repositorio.Guardar(entity);
        }

        public virtual void Update(T entity)
        {
            _repositorio.Update(entity);
        }
    }
}