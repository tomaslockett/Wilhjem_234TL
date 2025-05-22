using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public interface RepositorioDAL_234TL<T, Tkey>
    {
        T GetbyPrimary(T entity);

        T GetbyPrimaryKey(Tkey Key);

        IList<T> GetAll();

        void Guardar(T entity);

        void Update(T entity);

        void Eliminar(T entity);
    }
}