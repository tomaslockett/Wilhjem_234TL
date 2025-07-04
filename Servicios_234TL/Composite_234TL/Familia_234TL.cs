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
        public int Id => IdFamilia; 
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
        public bool PuedeAgregarHijo(IComponente_234TL componente, out string motivoFallo)
        {
            if (this.ObtenerHijos().Any(h => h.EsIgual(componente)))
            {
                motivoFallo = $"El componente '{componente.Nombre}' ya es miembro de esta familia.";
                return false;
            }

            if (componente is Permiso_234TL permiso)
            {
                if (this.ObtenerPermisos().Any(p => p.IdPermiso == permiso.IdPermiso))
                {
                    motivoFallo = $"El permiso '{permiso.Nombre}' ya está incluido en la jerarquía de esta familia.";
                    return false;
                }
            }

            if (componente is Familia_234TL familia)
            {
                if (familia.Id == this.Id || familia.ContieneEnJerarquia(this.Id))
                {
                    motivoFallo = "No se puede agregar esta familia porque crearía una dependencia circular.";
                    return false;
                }
            }

            motivoFallo = string.Empty;
            return true;
        }
        public bool ContieneEnJerarquia(int idFamiliaBuscada)
        {
            return this.ObtenerHijos().OfType<Familia_234TL>().Any(hijo => hijo.Id == idFamiliaBuscada || hijo.ContieneEnJerarquia(idFamiliaBuscada));
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
