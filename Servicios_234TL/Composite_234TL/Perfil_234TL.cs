using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Composite_234TL
{
    public class Perfil_234TL 
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }

        private readonly List<IComponente_234TL> Componentes = new();

        public Perfil_234TL(string nombre)
        {
            Nombre = nombre;
        }

        public List<Permiso_234TL> ObtenerPermisos()
        {
            List<Permiso_234TL> permisos = new();
            foreach (var componente in Componentes)
            {
                permisos.AddRange(componente.ObtenerPermisos());
            }
            return permisos.DistinctBy(p => p.Nombre).ToList(); 
            //EHH el Distinctby talvez me da problemas en el futuro, ya no me acuerdo porque pero tene encuenta eso lockett del futuro. te quiero :P
            //AA problemas a la hora de guardar y cargar creo
        }
        public List<IComponente_234TL> ObtenerComponentes()
        {
            return new List<IComponente_234TL>(Componentes);
        }
        public bool EliminarComponente(IComponente_234TL componente)
        {
            var componenteAEliminar = Componentes.FirstOrDefault(c => c.EsIgual(componente));
            return componenteAEliminar != null && Componentes.Remove(componenteAEliminar);
        }

        public void AgregarComponente(IComponente_234TL Componente)
        {
            Componentes.Add(Componente);
        }

    }
}
