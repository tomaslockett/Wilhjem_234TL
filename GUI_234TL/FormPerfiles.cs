using BLL_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Observer_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormPerfiles : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private PerfilBLL_234TL perfilBLL = new PerfilBLL_234TL();
        private FamiliaBLL_234TL familiaBLL = new FamiliaBLL_234TL();
        private PermisoBLL_234TL permisoBLL = new PermisoBLL_234TL();
        private Familia_234TL temporal;
        public FormPerfiles()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
            PermisosListbox.SelectionMode = SelectionMode.One;

            CargarPermisos();
            CargarFamilias();
            CargarPerfiles();
        }
        #region Idioma
        private void FormPerfiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            try
            {
                this.Text = Traduccion["FormPerfiles_Title"];
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorEnUpdateIdioma", ex);
            }
        }
        private void AgregarAFamiliaPermisoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode == null)
                {
                    Utilitarios_234TL.MensajeError("DebeSeleccionarUnaFamilia");
                    return;
                }
                if (PermisosListbox.SelectedItems.Count == 0)
                {
                    Utilitarios_234TL.MensajeError("DebeSeleccionarAlMenosUnPermiso");
                    return;
                }

                var Padre = FamiliasTreeView.SelectedNode.Tag as Familia_234TL;
                if (Padre == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
                    return;
                }
                bool algunAgregado = false;

                foreach (var selectedItem in PermisosListbox.SelectedItems)
                {
                    if (selectedItem is Permiso_234TL permiso)
                    {
                        bool permisoYaExiste = Padre.ObtenerHijos().Any(h => h is Permiso_234TL p && p.IdPermiso == permiso.IdPermiso);
                        if (!permisoYaExiste)
                        {
                            if (PermisoExisteEnJerarquia(Padre, permiso.IdPermiso))
                            {
                                Utilitarios_234TL.MensajeAdvertencia("PermisoYaExisteEnJerarquia");
                                continue;
                            }
                            Padre.AgregarHijo(permiso);
                            algunAgregado = true;
                        }
                        else
                        {
                            Utilitarios_234TL.MensajeAdvertencia("ElPermisoYaExisteEnEstaFamilia");
                        }
                    }
                }

                if (algunAgregado)
                {
                    familiaBLL.Update(Padre);
                    CargarFamilias();
                    CargarPerfiles();
                    Utilitarios_234TL.MensajeExito("PermisoAgregadoCorrectamente");
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlAgregarPermisoAFamilia", ex);
            }
        }

        private void EliminarDeFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode == null ||
                 FamiliasTreeView.SelectedNode.Parent == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalidaEliminar");
                    return;
                }

                var familia = FamiliasTreeView.SelectedNode.Parent.Tag as Familia_234TL;
                var componente = FamiliasTreeView.SelectedNode.Tag as IComponente_234TL;

                if (familia == null || componente == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
                    return;
                }

                familia.EliminarHijo(componente);
                familiaBLL.Update(familia);
                CargarFamilias();
                CargarPerfiles();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlEliminarDeFamilia", ex);
            }
        }

        private void EliminarFamiliaDefinitivoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode?.Parent != null)
                {
                    Utilitarios_234TL.MensajeError("SoloSePuedeEliminarFamiliasRaiz");
                    return;
                }

                var familia = FamiliasTreeView.SelectedNode.Tag as Familia_234TL;
                if (familia == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
                    return;
                }
                if (familia.ObtenerHijos().Count > 0)
                {
                    Utilitarios_234TL.MensajeError("FamiliaNoVaciaNoEliminable");
                    return;
                }

                familiaBLL.Eliminar(familia);
                CargarFamilias();
                CargarPerfiles();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlEliminarFamilia", ex);
            }
        }

        private void CrearFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = NombreFamiliaTextbox.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Utilitarios_234TL.MensajeError("NombreFamiliaRequerido");
                    return;
                }

                if (familiaBLL.GetAll().Any(f => f.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                {
                    Utilitarios_234TL.MensajeError("MismoNombreError");
                    return;
                }
                var nueva = new Familia_234TL(nombre);
                familiaBLL.Guardar(nueva);
                CargarFamilias();
                CargarPerfiles();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlCrearFamilia: {ex.Message}");
            }
        }
        private void AgregarAFamiliaFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode?.Tag is not Familia_234TL familiaPadre)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarFamiliaPadre");
                    return;
                }

                if (temporal == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarFamiliaHija");
                    return;
                }

                if (familiaPadre.IdFamilia == temporal.IdFamilia)
                {
                    Utilitarios_234TL.MensajeError("NoSePuedeAgregarAFamiliaASiMisma");
                    return;
                }

                if (EsCircular(familiaPadre, temporal))
                {
                    Utilitarios_234TL.MensajeError("JerarquiaCircularError");
                    return;
                }
                bool familiaYaExiste = familiaPadre.ObtenerHijos().Any(h => h is Familia_234TL f && f.IdFamilia == temporal.IdFamilia);
                if (familiaYaExiste)
                {
                    Utilitarios_234TL.MensajeAdvertencia("FamiliaYaContieneEstaFamilia");
                    return;
                }

                familiaPadre.AgregarHijo(temporal);
                familiaBLL.Update(familiaPadre);
                CargarFamilias();
                CargarPerfiles();

                temporal = null;
                FamiliaSeleccionadaLabel.Text = "Familia seleccionada: (ninguna)";
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlAgregarFamiliaAFamilia", ex);
            }
        }
        private void SeleccionarFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode?.Tag is Familia_234TL familia)
                {
                    temporal = familia;
                    FamiliaSeleccionadaLabel.Text = $"Familia seleccionada: {familia.Nombre}";
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlSeleccionarFamilia", ex);
            }
        }

        private void DeseleccionarFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                temporal = null;
                FamiliaSeleccionadaLabel.Text = "Familia seleccionada: (ninguna)";
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlDeseleccionarFamilia", ex);
            }
        }

        #endregion

        #region Perfiles
        private void AgregarAPerfilPermisoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Tag is not Perfil_234TL perfil)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarPerfil");
                    return;
                }

                if (PermisosListbox.SelectedItems.Count == 0)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarPermiso");
                    return;
                }
                bool algunAgregado = false;
                foreach (var selectedItem in PermisosListbox.SelectedItems)
                {
                    if (selectedItem is Permiso_234TL permiso)
                    {
                        bool permisoDirectoExistente = perfil.ObtenerComponentes().Any(c => c is Permiso_234TL p && p.IdPermiso == permiso.IdPermiso);

                        bool permisoEnFamilias = perfil.ObtenerComponentes().OfType<Familia_234TL>().Any(familia => PermisoExisteEnJerarquia(familia, permiso.IdPermiso));

                        if (permisoDirectoExistente || permisoEnFamilias)
                        {
                            Utilitarios_234TL.MensajeAdvertencia($"El permiso '{permiso.Nombre}' ya existe en el perfil");
                            continue;
                        }

                        perfil.AgregarComponente(permiso);
                        algunAgregado = true;
                    }
                }

                if (algunAgregado)
                {
                    perfilBLL.Update(perfil);
                    CargarPerfiles();
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlAgregarPermisoAPerfil", ex);
            }
        }

        private void AgregarAPerfilFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Tag is not Perfil_234TL perfil)
                {
                    return;
                }
                if (FamiliasTreeView.SelectedNode?.Tag is not Familia_234TL familia)
                {
                    return;
                }
                bool familiaDirectaExistente = perfil.ObtenerComponentes().Any(c => c is Familia_234TL f && f.IdFamilia == familia.IdFamilia);

                bool familiaEnJerarquia = perfil.ObtenerComponentes().OfType<Familia_234TL>().Any(f => FamiliaExisteEnJerarquia(f, familia.IdFamilia));

                if (familiaDirectaExistente || familiaEnJerarquia)
                {
                    Utilitarios_234TL.MensajeAdvertencia("ErrorLaFamiliayaEstaEnElPerfil");
                    return;
                }
                perfil.AgregarComponente(familia);
                perfilBLL.Update(perfil);
                CargarPerfiles();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlAgregarFamiliaAPerfil", ex);
            }
        }
        private void EliminarDePerfilButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Tag is not IComponente_234TL componente || PerfilesTreeView.SelectedNode?.Parent?.Tag is not Perfil_234TL perfil)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
                    return;
                }

                if (perfil.EliminarComponente(componente))
                {
                    perfilBLL.Update(perfil);
                    CargarPerfiles();
                    CargarFamilias();
                }
                else
                {
                    Utilitarios_234TL.MensajeError("ErrorAlEliminarComponente");
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlEliminarDePerfil", ex);
            }
        }

        private void EliminarPerfilDefinitivoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Tag is not Perfil_234TL perfil)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
                    return;
                }


                if (perfil.ObtenerComponentes().Count > 0)
                {
                    Utilitarios_234TL.MensajeError("PerfilNoVacioNoEliminable");
                    return;
                }

                perfilBLL.Eliminar(perfil);
                CargarPerfiles();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlEliminarPerfil: {ex.Message}");
            }
        }

        private void CrearPerfilButton_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = NombrePerfilTextbox.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Utilitarios_234TL.MensajeError("NombrePerfilRequerido");
                    return;
                }

                if (perfilBLL.GetAll().Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                {
                    Utilitarios_234TL.MensajeError("PerfilDuplicadoError");
                    return;
                }

                Perfil_234TL nuevo = new(nombre);
                perfilBLL.Guardar(nuevo);
                TreeNode nodo = new TreeNode(nombre);
                nodo.Tag = new Perfil_234TL(nombre);
                PerfilesTreeView.Nodes.Add(nodo);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlCrearPerfil", ex);
            }
        }

        #endregion

        #region Nodo
        private TreeNode CrearNodo(IComponente_234TL comp)
        {
            try
            {
                TreeNode nodo = new TreeNode();
                nodo.Tag = comp;

                if (comp is Permiso_234TL permiso)
                {
                    nodo.Text = permiso.Nombre;
                }
                else if (comp is Familia_234TL familia)
                {
                    nodo.Text = familia.Nombre;
                    foreach (var hijo in familia.ObtenerHijos())
                    {
                        nodo.Nodes.Add(CrearNodo(hijo));
                    }
                }

                return nodo;
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlCrearNodo", ex);
                return new TreeNode("Error");
            }
        }
        private void ActualizarNodoFamilia(TreeNode nodo, Familia_234TL familia)
        {
            nodo.Nodes.Clear();
            foreach (var hijo in familia.ObtenerHijos())
            {
                nodo.Nodes.Add(CrearNodo(hijo));
            }
            nodo.Expand();
        }
        private void ActualizarNodoPerfil(TreeNode nodo, Perfil_234TL perfil)
        {
            nodo.Nodes.Clear();
            foreach (var componente in perfil.ObtenerComponentes())
            {
                nodo.Nodes.Add(CrearNodo(componente));
            }
            nodo.Expand();
        }
        #endregion

        #region Cargar
        private void CargarPermisos()
        {
            try
            {
                var Permisos = permisoBLL.GetAll();
                PermisosListbox.Items.Clear();
                foreach (var permiso in Permisos)
                {
                    PermisosListbox.Items.Add(permiso);
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlCargarPermisos", ex);
            }
        }
        private void CargarFamilias()
        {
            try
            {
                var familias = familiaBLL.GetAll();
                FamiliasTreeView.Nodes.Clear();
                foreach (var familia in familias)
                {
                    TreeNode nodoFamilia = CrearNodo(familia);
                    FamiliasTreeView.Nodes.Add(nodoFamilia);
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlCargarFamilias", ex);
            }
        }

        private void CargarPerfiles()
        {
            try
            {
                var perfiles = perfilBLL.GetAll();
                PerfilesTreeView.Nodes.Clear();

                foreach (var perfil in perfiles)
                {
                    TreeNode nodoPerfil = new TreeNode(perfil.Nombre) { Tag = perfil };
                    foreach (var componente in perfil.ObtenerComponentes())
                    {
                        nodoPerfil.Nodes.Add(CrearNodo(componente));
                    }
                    PerfilesTreeView.Nodes.Add(nodoPerfil);
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlCargarPerfiles", ex);
            }
        }

        #endregion
        private bool EsCircular(Familia_234TL padre, Familia_234TL posibleHijo)
        {
            if (padre.IdFamilia == posibleHijo.IdFamilia)
            {
                return true;
            }

            return VerificarJerarquia(posibleHijo, padre.IdFamilia);
        }

        private bool VerificarJerarquia(Familia_234TL familia, int idPadre)
        {
            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Familia_234TL f)
                {
                    if (f.IdFamilia == idPadre)
                    {
                        return true;
                    }

                    if (VerificarJerarquia(f, idPadre))
                    {
                        return true;
                    }
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
        private bool FamiliaExisteEnJerarquia(Familia_234TL familia, int idFamiliaBuscada)
        {
            if (familia.IdFamilia == idFamiliaBuscada)
            {
                return true;
            }

            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Familia_234TL subfamilia)
                {
                    if (FamiliaExisteEnJerarquia(subfamilia, idFamiliaBuscada))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PermisosListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PermisosListbox.SelectedItem is Permiso_234TL permiso)
                {
                    PermisoSeleccionadoLabel.Text = $"Permiso seleccionado: {permiso.Nombre}";
                }
                else
                {
                    PermisoSeleccionadoLabel.Text = "Permiso seleccionado: -";
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlSeleccionarPermiso", ex);
            }
        }

        private void PerfilesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node?.Tag is Perfil_234TL perfil)
                {
                    PerfilSeleccionadoLabel.Text = $"Perfil seleccionado: {perfil.Nombre}";
                }
                else
                {
                    PerfilSeleccionadoLabel.Text = "Perfil seleccionado: -";
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorAlSeleccionarPerfil", ex);
            }
        }
    }
}
