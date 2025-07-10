using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;

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
                throw new ValidacionesException_234TL("FamiliaNombreVacio", nameof(nombre));
            }

            if (_FamiliaDAL.NombreExiste(nombre))
            {
                throw new ValidacionesException_234TL("FamiliaYaExiste", nameof(nombre), nombre);
            }

            var nuevaFamilia = new Familia_234TL(nombre);

            base.Guardar(nuevaFamilia);

            return nuevaFamilia;
        }
        private List<Familia_234TL> ObtenerSubFamiliasRecursivo(Familia_234TL familia)
        {
            var listaFamilias = new List<Familia_234TL>();
            foreach (var hijo in familia.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (!listaFamilias.Any(f => f.IdFamilia == hijo.IdFamilia))
                {
                    listaFamilias.Add(hijo);
                }

                var familiasNietas = ObtenerSubFamiliasRecursivo(hijo);
                foreach (var nieta in familiasNietas)
                {
                    if (!listaFamilias.Any(f => f.IdFamilia == nieta.IdFamilia))
                    {
                        listaFamilias.Add(nieta);
                    }
                }
            }
            return listaFamilias;
        }

        public void EliminarFamilia(Familia_234TL familia)
        {
           
            if (familia == null)
            {
                throw new ValidacionesException_234TL("FamiliaNula", "General");
            }

            if (familia.ObtenerHijos().Any())
            {
                throw new ValidacionesException_234TL("FamiliaNoVacia", "General");
            }

            if (_FamiliaDAL.FamiliaEstaAsignadaComoHijo(familia.IdFamilia))
            {
                throw new ValidacionesException_234TL("FamiliaAsignadaComoHija", "General");
            }

            if (_perfilDAL.FamiliaEstaEnUso(familia.IdFamilia))
            {
                throw new ValidacionesException_234TL("FamiliaEnUsoPorPerfil", "General");
            }

            base.Eliminar(familia);
        }

        public void AgregarHijo(Familia_234TL padre, IComponente_234TL hijo)
        {

            if (hijo is Familia_234TL familiaHija)
            {
                if (padre.IdFamilia == familiaHija.IdFamilia)
                {
                    throw new ValidacionesException_234TL("FamiliaContenidaASiMisma", "General");
                }

                if (EsCircular(padre, familiaHija))
                {
                    throw new ValidacionesException_234TL("ErrorJerarquiaCircular", "General");
                }

                if (JerarquiaYaContieneFamilia(padre, familiaHija.IdFamilia))
                {
                    throw new ValidacionesException_234TL("FamiliaYaExisteEnJerarquia", "General", familiaHija.Nombre, padre.Nombre);
                }

                ValidarFamiliaNoRedundante(padre, familiaHija);
            }
            else if (hijo is Permiso_234TL permisoHijo)
            {
                ValidarPermisoNoRedundante(padre, permisoHijo);
            }

            bool yaExiste;
            if (hijo is Familia_234TL f)
            {
                yaExiste = padre.ObtenerHijos().Any(h => h is Familia_234TL familiahija && familiahija.IdFamilia == f.IdFamilia);
            }
            else if (hijo is Permiso_234TL p)
            {
                yaExiste = padre.ObtenerHijos().Any(h => h is Permiso_234TL permisohija && permisohija.IdPermiso == p.IdPermiso);
            }
            else
            {
                throw new ValidacionesException_234TL("TipoComponenteInvalido", "General");
            }

            if (yaExiste)
            {
                throw new ValidacionesException_234TL("FamiliaYaContieneComponente", "General", padre.Nombre, hijo.Nombre);
            }

            padre.AgregarHijo(hijo);
            this.Update(padre);
        }

        private bool EsCircular(Familia_234TL padre, Familia_234TL posibleHijo)
        {
            if (posibleHijo.IdFamilia == padre.IdFamilia)
            {
                return true;
            }

            foreach (var hijo in posibleHijo.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (hijo.IdFamilia == padre.IdFamilia)
                {
                    return true;
                }

                if (EsCircular(padre, hijo))
                {
                    return true;
                }
            }

            return false;
        }

        public void QuitarHijo(Familia_234TL padre, IComponente_234TL hijo)
        {
            padre.EliminarHijo(hijo);
            this.Update(padre);
        }
        #region Permiso
        public void ValidarPermisoNoRedundante(Familia_234TL familiaDestino, Permiso_234TL permisoAAgregar)
        {
            if (PermisoExisteEnJerarquia(familiaDestino, permisoAAgregar.IdPermiso))
            {
                throw new ValidacionesException_234TL("PermisoDuplicado", "General", permisoAAgregar.Nombre);
            }

            var todasLasFamiliasRaiz = this.GetAll();

            foreach (var familiaRaiz in todasLasFamiliasRaiz)
            {
                if (AncestroYaContienePermiso(familiaRaiz, familiaDestino, permisoAAgregar.IdPermiso))
                {
                    throw new ValidacionesException_234TL("PermisoDuplicadoEnHijo", "General", permisoAAgregar.Nombre);
                }
            }
        }
        private bool AncestroYaContienePermiso(Familia_234TL HijoActual, Familia_234TL familiaDestino, int idPermisoBuscado)
        {
            bool contieneDestino = false;

            foreach (var hijo in HijoActual.ObtenerHijos())
            {
                if (hijo is Familia_234TL familiaHijo)
                {
                    if (FamiliaExisteEnJerarquia(familiaHijo, familiaDestino.IdFamilia))
                    {
                        contieneDestino = true;
                        break;
                    }
                }
            }

            if (contieneDestino)
            {
                if (PermisoExisteEnJerarquia(HijoActual, idPermisoBuscado))
                {
                    return true;
                }
            }
            foreach (var subFamilia in HijoActual.ObtenerHijos().OfType<Familia_234TL>())
            {
                if (AncestroYaContienePermiso(subFamilia, familiaDestino, idPermisoBuscado))
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
                if (hijo is Permiso_234TL permiso && permiso.IdPermiso == idPermiso)
                {
                    return true;
                }
                if (hijo is Familia_234TL subfamilia && PermisoExisteEnJerarquia(subfamilia, idPermiso))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Familia

        public void ValidarFamiliaNoRedundante(Familia_234TL padre, Familia_234TL familiaHija)
        {
            var permisosDeLaHija = ObtenerPermisosDeJerarquia(familiaHija);
            foreach (var permiso in permisosDeLaHija)
            {
                if (PermisoExisteEnJerarquia(padre, permiso.IdPermiso))
                {
                    throw new ValidacionesException_234TL("PermisoRedundanteEnFamilia", "General", permiso.Nombre, familiaHija.Nombre);
                }
            }

            var subFamiliasDeLaHija = ObtenerSubFamiliasRecursivo(familiaHija);

            subFamiliasDeLaHija.Add(familiaHija);

            foreach (var subFamilia in subFamiliasDeLaHija)
            {
                if (JerarquiaYaContieneFamilia(padre, subFamilia.IdFamilia))
                {
                    throw new ValidacionesException_234TL("FamiliaRedundanteEnJerarquia", "General", subFamilia.Nombre, padre.Nombre);
                }
            }
        }

        private bool JerarquiaYaContieneFamilia(Familia_234TL familiaActual, int idFamiliaBuscada)
        {
            foreach (var hijo in familiaActual.ObtenerHijos())
            {
                if (hijo is Familia_234TL familiaHijo)
                {
                    if (familiaHijo.IdFamilia == idFamiliaBuscada)
                    {
                        return true; 
                    }
                    if (JerarquiaYaContieneFamilia(familiaHijo, idFamiliaBuscada))
                    {
                        return true; 
                    }
                }
            }

            return false; 
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

        #endregion


        private List<Permiso_234TL> ObtenerPermisosDeJerarquia(Familia_234TL familia)
        {
            var permisosUnicos = new List<Permiso_234TL>();

            foreach (var permiso in familia.ObtenerHijos().OfType<Permiso_234TL>())
            {
                if (!permisosUnicos.Any(p => p.IdPermiso == permiso.IdPermiso))
                {
                    permisosUnicos.Add(permiso);
                }
            }

            foreach (var subFamilia in familia.ObtenerHijos().OfType<Familia_234TL>())
            {
                var permisosHijos = ObtenerPermisosDeJerarquia(subFamilia);
                foreach (var permisoHijo in permisosHijos)
                {
                    if (!permisosUnicos.Any(p => p.IdPermiso == permisoHijo.IdPermiso))
                    {
                        permisosUnicos.Add(permisoHijo);
                    }
                }
            }

            return permisosUnicos;
        }
    }
}
