using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Composite_234TL
{
    public class Familia_234TL : IComponente_234TL
    {
        public int IdFamilia { get; set; }
        public string Nombre { get; set; }
        private readonly List<IComponente_234TL> hijos = new();
        public Familia_234TL(string nombre)
        {
            Nombre = nombre;
        }
        public void AgregarHijo(IComponente_234TL hijo)
        {
            hijos.Add(hijo);
        }

        public void EliminarHijo(IComponente_234TL hijo)
        {
            hijos.Remove(hijo);
        }

        public bool EsIgual(IComponente_234TL otro)
        {
            return otro is Familia_234TL f && f.IdFamilia == this.IdFamilia;
        }

        public List<IComponente_234TL> ObtenerHijos()
        {
            return new List<IComponente_234TL>(hijos);
        }

        public List<Permiso_234TL> ObtenerPermisos()
        {
            List<Permiso_234TL> permisos = new();
            foreach (var hijo in hijos)
            {
                permisos.AddRange(hijo.ObtenerPermisos());
            }
            return permisos;
        }
    }
}
