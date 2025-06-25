using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Composite_234TL
{
    public class Permiso_234TL : IComponente_234TL
    {
        public int IdPermiso { get; set; }
        public string Nombre { get; set; }

        public Permiso_234TL(string nombre)
        {
            Nombre = nombre;
        }

        public void AgregarHijo(IComponente_234TL hijo)
        {
            throw new InvalidOperationException("No se puede agregar hijos a un permiso.");
        }

        public void EliminarHijo(IComponente_234TL hijo)
        {
            throw new InvalidOperationException("No se puede eliminar hijos de un permiso.");
        }

        public List<IComponente_234TL> ObtenerHijos()
        {
            return new List<IComponente_234TL>();
        }

        public bool EsIgual(IComponente_234TL otro)
        {
            return otro is Permiso_234TL p && p.IdPermiso == this.IdPermiso;
        }

        public List<Permiso_234TL> ObtenerPermisos()
        {
            return new List<Permiso_234TL> { this };
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
