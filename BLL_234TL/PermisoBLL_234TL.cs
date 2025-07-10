using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;

namespace BLL_234TL
{
    public class PermisoBLL_234TL : AbstractaBLL_234TL<Permiso_234TL, int>
    {
        private readonly FamiliaDAL_234TL _familiaDAL;
        private readonly PerfilDAL_234TL _perfilDAL;
        public PermisoBLL_234TL() : base(new PermisoDAL_234TL())
        {
            _familiaDAL = new FamiliaDAL_234TL();
            _perfilDAL = new PerfilDAL_234TL();
        }
        public override void Guardar(Permiso_234TL permiso)
        {
            if (string.IsNullOrWhiteSpace(permiso.Nombre))
            {
                throw new ValidacionesException_234TL("PermisoNombreVacio", nameof(permiso.Nombre));
            }

            var todosLosPermisos = this.GetAll();
            if (todosLosPermisos.Any(p => p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidacionesException_234TL("PermisoYaExiste", nameof(permiso.Nombre), permiso.Nombre);
            }

            base.Guardar(permiso);
        }
        public override void Update(Permiso_234TL permiso)
        {
            var todosLosPermisos = this.GetAll();
            if (todosLosPermisos.Any(p => p.IdPermiso != permiso.IdPermiso && p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidacionesException_234TL("PermisoYaExiste", nameof(permiso.Nombre), permiso.Nombre);
            }

            base.Update(permiso);
        }
        public void EliminarPermiso(Permiso_234TL permiso)
        {
            if (permiso == null)
            {
                throw new ValidacionesException_234TL("PermisoNulo", "General");
            }

            if (_familiaDAL.PermisoEstaEnUso(permiso.IdPermiso))
            {
                throw new ValidacionesException_234TL("PermisoEnUsoPorFamilia", "General");
            }

            if (_perfilDAL.PermisoEstaEnUso(permiso.IdPermiso))
            {
                throw new ValidacionesException_234TL("PermisoEnUsoPorPerfil", "General");
            }

            base.Eliminar(permiso);
        }

    }
}
