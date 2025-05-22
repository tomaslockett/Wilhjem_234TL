namespace DAL_234TL
{
    public abstract class AbstractDAL_234TL<T, TKey> : RepositorioDAL_234TL<T, TKey>
    {
        public abstract T GetbyPrimary(T entity);

        public abstract T GetbyPrimaryKey(TKey id);

        public abstract IList<T> GetAll();

        public abstract void Eliminar(T entity);

        public abstract void EliminarKey(TKey key);

        public abstract void Guardar(T entity);

        public abstract void Update(T entity);
    }
}