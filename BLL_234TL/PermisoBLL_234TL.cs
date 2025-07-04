using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ArgumentException("El nombre del permiso no puede estar vacío.");
            }

            var todosLosPermisos = this.GetAll();
            if (todosLosPermisos.Any(p => p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Ya existe un permiso con el nombre '{permiso.Nombre}'.");
            }

            base.Guardar(permiso);
        }
        public override void Update(Permiso_234TL permiso)
        {
            var todosLosPermisos = this.GetAll();
            if (todosLosPermisos.Any(p => p.IdPermiso != permiso.IdPermiso && p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Ya existe otro permiso con el nombre '{permiso.Nombre}'.");
            }

            base.Update(permiso);
        }
        public void EliminarPermiso(Permiso_234TL permiso)
        {
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));

            if (_familiaDAL.PermisoEstaEnUso(permiso.IdPermiso))
                throw new InvalidOperationException("El permiso no se puede eliminar, está en uso por una o más familias.");

            if (_perfilDAL.PermisoEstaEnUso(permiso.IdPermiso))
                throw new InvalidOperationException("El permiso no se puede eliminar, está en uso por uno o más perfiles.");

            base.Eliminar(permiso);
        }

    }
}
