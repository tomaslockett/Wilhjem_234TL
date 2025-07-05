using DAL_234TL;
using Servicios_234TL.Composite_234TL;

namespace BLL_234TL
{
    public class FamiliaBLL_234TL : AbstractaBLL_234TL<Familia_234TL, int>
    {
        private readonly PerfilDAL_234TL _perfilDAL;
        private readonly FamiliaDAL_234TL _FamiliaDAL;
        public FamiliaBLL_234TL() : base(new FamiliaDAL_234TL())
        {
            _perfilDAL = new PerfilDAL_234TL();
            _FamiliaDAL = new FamiliaDAL_234TL();
        }

        public Familia_234TL CrearNuevaFamilia(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");
            }

            if (_FamiliaDAL.NombreExiste(nombre))
            {
                throw new InvalidOperationException($"Ya existe una familia con el nombre '{nombre}'.");
            }

            var nuevaFamilia = new Familia_234TL(nombre);

            base.Guardar(nuevaFamilia);

            return nuevaFamilia;
        }

        public void EliminarFamilia(Familia_234TL familia)
        {
            if (familia == null)
            {
                throw new ArgumentNullException(nameof(familia), "La familia a eliminar no puede ser nula.");
            }

            if (familia.ObtenerHijos().Any())
            {
                throw new InvalidOperationException("La familia no se puede eliminar porque no está vacía. Primero debe quitar todos sus componentes.");
            }

            if (_perfilDAL.FamiliaEstaEnUso(familia.IdFamilia))
            {
                throw new InvalidOperationException("La familia no se puede eliminar porque está asignada a uno o más perfiles.");
            }

            base.Eliminar(familia);
        }

        public void AgregarHijo(Familia_234TL padre, IComponente_234TL hijo)
        {

            if (hijo is Familia_234TL familiaHija)
            {
                if (padre.IdFamilia == familiaHija.IdFamilia)
                {
                    throw new InvalidOperationException("Una familia no puede contenerse a sí misma.");
                }

                if (EsCircular(padre, familiaHija))
                {
                    throw new InvalidOperationException("Error de jerarquía circular: La familia que intenta agregar ya contiene a la familia padre.");
                }
            }

            bool yaExiste;
            if (hijo is Familia_234TL f)
            {
                yaExiste = padre.ObtenerHijos().Any(h => h is Familia_234TL fh && fh.IdFamilia == f.IdFamilia);
            }
            else if (hijo is Permiso_234TL p)
            {
                yaExiste = padre.ObtenerHijos().Any(h => h is Permiso_234TL ph && ph.IdPermiso == p.IdPermiso);
            }
            else
            {
                throw new ArgumentException("Tipo de componente no válido.");
            }

            if (yaExiste)
            {
                throw new InvalidOperationException($"La familia '{padre.Nombre}' ya contiene el componente '{hijo.Nombre}'.");
            }

            padre.AgregarHijo(hijo);
            this.Update(padre);
        }

        private bool EsCircular(Familia_234TL padre, Familia_234TL posibleHijo)
        {
            if (posibleHijo.ObtenerHijos().Any(h => h.Id == padre.Id))
            {
                return true;
            }
            return posibleHijo.ObtenerHijos().OfType<Familia_234TL>().Any(subFamilia => EsCircular(padre, subFamilia));
        }

        public void QuitarHijo(Familia_234TL padre, IComponente_234TL hijo)
        {
            padre.EliminarHijo(hijo);
            this.Update(padre);
        }
        public void ValidarPermisoNoRedundante(Familia_234TL familiaDestino, Permiso_234TL permisoAAgregar)
        {
            var todasLasFamiliasRaiz = this.GetAll(); 

            foreach (var familiaRaiz in todasLasFamiliasRaiz)
            {
                if (BuscarRedundanciaEnAncestro(familiaRaiz, familiaDestino, permisoAAgregar.IdPermiso))
                {
                    throw new InvalidOperationException($"El permiso '{permisoAAgregar.Nombre}' ya es heredado de una familia de nivel superior.");
                }
            }
        }

        private bool BuscarRedundanciaEnAncestro(Familia_234TL ancestroActual, Familia_234TL familiaDestino, int idPermisoBuscado)
        {
            bool contieneDestino = ancestroActual.ObtenerHijos().OfType<Familia_234TL>().Any(hijo => FamiliaExisteEnJerarquia(hijo, familiaDestino.IdFamilia));

            if (contieneDestino)
            {
                if (PermisoExisteEnJerarquia(ancestroActual, idPermisoBuscado))
                {
                    return true; 
                }
            }
            foreach (var subFamilia in ancestroActual.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (BuscarRedundanciaEnAncestro(subFamilia, familiaDestino, idPermisoBuscado))
                {
                    return true;
                }
            }
            return false;
        }

        private bool PermisoExisteEnJerarquia(Familia_234TL familia, int idPermiso)
        {
            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Permiso_234TL permiso && permiso.IdPermiso == idPermiso) return true;
                if (hijo is Familia_234TL subfamilia && PermisoExisteEnJerarquia(subfamilia, idPermiso)) return true;
            }
            return false;
        }

        private bool FamiliaExisteEnJerarquia(Familia_234TL familia, int idFamiliaBuscada)
        {
            if (familia.IdFamilia == idFamiliaBuscada) return true;
            foreach (var hijo in familia.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (FamiliaExisteEnJerarquia(hijo, idFamiliaBuscada)) return true;
            }
            return false;
        }

    }
}
