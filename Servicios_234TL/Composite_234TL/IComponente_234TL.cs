using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Composite_234TL
{
    public interface IComponente_234TL
    {
        void AgregarHijo(IComponente_234TL hijo);
        void EliminarHijo(IComponente_234TL hijo);

        bool EsIgual(IComponente_234TL otro);
        List<IComponente_234TL> ObtenerHijos();
        List<Permiso_234TL> ObtenerPermisos();
    }
}
