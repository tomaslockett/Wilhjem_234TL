using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class PerfilBLL_234TL : AbstractaBLL_234TL<Perfil_234TL, int>
    {
        private readonly PerfilDAL_234TL _perfilDAL;
        public PerfilBLL_234TL() : base(new PerfilDAL_234TL())
        {
            _perfilDAL = new PerfilDAL_234TL();
        }

        public void AgregarComponenteAPerfil(Perfil_234TL perfil, IComponente_234TL componente)
        {
            if (perfil == null || componente == null)
            {
                throw new ArgumentNullException("El perfil y el componente no pueden ser nulos.");
            }

            var permisosActualesIds = perfil.ObtenerPermisos().Select(p => p.IdPermiso).ToHashSet();

            var permisosNuevos = componente.ObtenerPermisos();
            if (permisosNuevos.Any(p => permisosActualesIds.Contains(p.IdPermiso)))
            {
                throw new InvalidOperationException($"No se puede agregar '{componente.Nombre}' porque el perfil ya posee uno o más de sus permisos.");
            }

            if (componente is Familia_234TL familiaAAgregar)
            {
                var familiasActuales = perfil.ObtenerComponentes().OfType<Familia_234TL>();
                if (familiasActuales.Any(f => FamiliaExisteEnJerarquia(f, familiaAAgregar.IdFamilia)))
                {
                    throw new InvalidOperationException($"No se puede agregar la familia '{familiaAAgregar.Nombre}' porque ya está contenida en otra familia dentro del perfil.");
                }
            }

            perfil.AgregarComponente(componente);
            base.Update(perfil);
        }

        private bool FamiliaExisteEnJerarquia(Familia_234TL familia, int idFamiliaBuscada)
        {
            if (familia.IdFamilia == idFamiliaBuscada) return true;

            foreach (var hijo in familia.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (FamiliaExisteEnJerarquia(hijo, idFamiliaBuscada))
                {
                    return true;
                }
            }
            return false;
        }

        public Perfil_234TL CrearNuevoPerfil(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del perfil no puede estar vacío.");
            }

            if (_perfilDAL.NombreExiste(nombre))
            {
                throw new InvalidOperationException($"Ya existe un perfil con el nombre '{nombre}'.");
            }

            var nuevoPerfil = new Perfil_234TL(nombre);
            base.Guardar(nuevoPerfil);
            return nuevoPerfil;
        }
        public bool PerfilEstaEnUso(int idPerfil)
        {
            var usuarioBLL = new UsuarioBLL_234TL();
            return usuarioBLL.GetAll().Any(u => u.Perfil?.IdPerfil == idPerfil);
        }

        public override void Eliminar(Perfil_234TL entity)
        {
            if (PerfilEstaEnUso(entity.IdPerfil))
            {
                throw new InvalidOperationException("El perfil no se puede eliminar porque está asignado a uno o más usuarios.");
            }
            base.Eliminar(entity); 
        }

    }
}
