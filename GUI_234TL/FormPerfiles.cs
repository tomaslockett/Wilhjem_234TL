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
                Utilitarios_234TL.MensajeError($"ErrorEnUpdateIdioma: {ex.Message}");
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

                IComponente_234TL Componente = null;

                foreach (var selectedItem in PermisosListbox.SelectedItems)
                {
                    if (selectedItem is Permiso_234TL permiso)
                    {
                        Componente = permiso;
                        break;
                    }
                }
                if (FamiliasTreeView.SelectedNode.Tag is Familia_234TL familiaPadre && Componente != null)
                {
                    familiaPadre.AgregarHijo(Componente);
                    familiaBLL.Update(familiaPadre);
                    FamiliasTreeView.Nodes.Clear();
                    CargarFamilias();
                    Utilitarios_234TL.MensajeExito("PermisoAgregadoCorrectamente");
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlAgregarPermisoAFamilia: {ex.Message}");
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

                FamiliasTreeView.SelectedNode.Remove();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlEliminarDeFamilia: {ex.Message}");
            }
        }

        private void EliminarFamiliaDefinitivoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FamiliasTreeView.SelectedNode == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionInvalida");
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
                FamiliasTreeView.SelectedNode.Remove();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlEliminarFamilia: {ex.Message}");
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

                FamiliasTreeView.Nodes.Add(CrearNodo(nueva));
                NombreFamiliaTextbox.Clear();
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


                familiaPadre.AgregarHijo(temporal);
                familiaBLL.Update(familiaPadre);

                FamiliasTreeView.BeginUpdate();
                FamiliasTreeView.Nodes.Clear();
                CargarFamilias();
                FamiliasTreeView.EndUpdate();

                temporal = null;
                FamiliaSeleccionadaLabel.Text = "Familia seleccionada: (ninguna)";
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlAgregarFamiliaAFamilia: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlSeleccionarFamilia: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlDeseleccionarFamilia: {ex.Message}");
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

                foreach (var selectedItem in PermisosListbox.SelectedItems)
                {
                    if (selectedItem is Permiso_234TL permiso)
                    {
                        perfil.AgregarComponente(permiso);
                        PerfilesTreeView.SelectedNode.Nodes.Add(CrearNodo(permiso));
                    }
                }
                perfilBLL.Update(perfil);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlAgregarPermisoAPerfil: {ex.Message}");
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
                perfil.AgregarComponente(familia);
                PerfilesTreeView.SelectedNode.Nodes.Add(CrearNodo(familia));
                perfilBLL.Update(perfil);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlAgregarFamiliaAPerfil: {ex.Message}");
            }
        }
        private void EliminarDePerfilButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Parent?.Tag is Perfil_234TL perfil)
                {
                    perfil.ObtenerComponentes().Remove((IComponente_234TL)PerfilesTreeView.SelectedNode.Tag);
                    PerfilesTreeView.SelectedNode.Remove();
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlEliminarDePerfil: {ex.Message}");
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
                PerfilesTreeView.SelectedNode.Remove();
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
                Utilitarios_234TL.MensajeError($"ErrorAlCrearPerfil: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlCrearNodo: {ex.Message}");
                return new TreeNode("Error");
            }
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
                Utilitarios_234TL.MensajeError($"ErrorAlCargarPermisos: {ex.Message}");
            }
        }
        private void CargarFamilias()
        {
            try
            {
                var familias = familiaBLL.GetAll();
                foreach (var familia in familias)
                {
                    TreeNode nodoFamilia = CrearNodo(familia);
                    FamiliasTreeView.Nodes.Add(nodoFamilia);
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"ErrorAlCargarFamilias: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlCargarPerfiles: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlSeleccionarPermiso: {ex.Message}");
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
                Utilitarios_234TL.MensajeError($"ErrorAlSeleccionarPerfil: {ex.Message}");
            }
        }
    }
}
