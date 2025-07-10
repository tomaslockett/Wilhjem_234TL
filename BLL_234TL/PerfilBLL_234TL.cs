using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;
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
                throw new ValidacionesException_234TL("PerfilComponenteNulos", "General");
            }

            var permisosActualesIds = perfil.ObtenerPermisos().Select(permiso => permiso.IdPermiso).ToHashSet();

            var permisosNuevos = componente.ObtenerPermisos();
            if (permisosNuevos.Any(permiso => permisosActualesIds.Contains(permiso.IdPermiso)))
            {
                throw new ValidacionesException_234TL("PerfilPermisoDuplicado", "General", componente.Nombre);
            }

            if (componente is Familia_234TL familiaAAgregar)
            {
                var familiasActuales = perfil.ObtenerComponentes().OfType<Familia_234TL>();
                if (familiasActuales.Any(familia => FamiliaExisteEnJerarquia(familia, familiaAAgregar.IdFamilia)))
                {
                    throw new ValidacionesException_234TL("PerfilFamiliaAnidada", "General", familiaAAgregar.Nombre);
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
                throw new ValidacionesException_234TL("PerfilNombreVacio", nameof(nombre));
            }

            if (_perfilDAL.NombreExiste(nombre))
            {
                throw new ValidacionesException_234TL("PerfilYaExiste", nameof(nombre), nombre);
            }

            var nuevoPerfil = new Perfil_234TL(nombre);
            base.Guardar(nuevoPerfil);
            return nuevoPerfil;
        }
        public bool PerfilEstaEnUso(int idPerfil)
        {
            var usuarioBLL = new UsuarioBLL_234TL();
            foreach (var usuario in usuarioBLL.GetAll())
            {
                if (usuario.Perfil != null && usuario.Perfil.IdPerfil == idPerfil)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Eliminar(Perfil_234TL entity)
        {
            if (entity.ObtenerComponentes().Any())
            {
                throw new ValidacionesException_234TL("PerfilNoVacioNoEliminable", "General");
            }
            if (PerfilEstaEnUso(entity.IdPerfil))
            {
                throw new ValidacionesException_234TL("PerfilEnUso", "General");
            }
            base.Eliminar(entity); 
        }

    }
}
