﻿using BLL_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormPerfiles_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private PerfilBLL_234TL perfilBLL = new PerfilBLL_234TL();
        private FamiliaBLL_234TL familiaBLL = new FamiliaBLL_234TL();
        private PermisoBLL_234TL permisoBLL = new PermisoBLL_234TL();
        private Familia_234TL temporal;
        public FormPerfiles_234TL()
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

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.Perfiles;

                this.Text = textos.Title;

                PermisosLabel.Text = textos.GroupBoxPermisos;
                FamiliaLabel.Text = textos.GroupBoxFamilias;
                ContenidoPerfiles.Text = textos.GroupBoxPerfiles;

                NombreFamiliaLabel.Text = textos.LabelNombreFamilia;
                CrearFamiliaButton.Text = textos.CrearFamiliaBotton;
                SeleccionarFamiliaButton.Text = textos.SeleccionarFamiliaBotton;
                DeseleccionarFamiliaButton.Text = textos.DeseleccionarFamiliaBotton;
                AgregarAFamiliaPermisoButton.Text = textos.AgregarPermisoAFamiliaBotton;
                AgregarAFamiliaFamiliaButton.Text = textos.AgregarFamiliaAFamiliaBotton;
                EliminarDeFamiliaButton.Text = textos.EliminarDeFamiliaBotton;
                EliminarFamiliaDefinitivoButton.Text = textos.EliminarFamiliaDefinitivoBotton;

                NombrePerfilLabel.Text = textos.LabelNombrePerfil;
                CrearPerfilButton.Text = textos.CrearPerfilBotton;
                AgregarAPerfilPermisoButton.Text = textos.AgregarPermisoAPerfilBotton;
                AgregarAPerfilFamiliaButton.Text = textos.AgregarFamiliaAPerfilBotton;
                EliminarDePerfilButton.Text = textos.EliminarDePerfilBotton;
                EliminarPerfilDefinitivoButton.Text = textos.EliminarPerfilDefinitivoBotton;

                FamiliaSeleccionadaLabel.Text = textos.FamiliaSeleccionadaLabel;
                PermisoSeleccionadoLabel.Text = textos.PermisoSeleccionadoLabel;
                PerfilSeleccionadoLabel.Text = textos.PerfilSeleccionadoLabel;
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorEnUpdateIdioma", ex);
            }
        }
        #endregion

        /// <summary>
        /// Tendria que cambiar el metodo de abajo. :D
        /// </summary>


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
                    Utilitarios_234TL.MensajeError("DebeSeleccionarUnPermiso");
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
                        familiaBLL.ValidarPermisoNoRedundante(Padre, permiso);
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
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
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
                Utilitarios_234TL.MensajeExito("ComponenteEliminadoExito");
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
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
                var familiaAEliminar = FamiliasTreeView.SelectedNode?.Tag as Familia_234TL;

                if (familiaAEliminar == null || FamiliasTreeView.SelectedNode.Parent != null)
                {
                    Utilitarios_234TL.MensajeError("SoloEliminarFamiliasRaiz");
                    return;
                }

                if (Utilitarios_234TL.MensajeConfirmacion(this,"ConfirmacionEliminarFamilia") == DialogResult.Yes)
                {
                    familiaBLL.EliminarFamilia(familiaAEliminar);
                    CargarFamilias();
                    CargarPerfiles();
                    Utilitarios_234TL.MensajeExito("FamiliaEliminadaExito");
                }

            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoEliminarFamilia", ex);
            }
        }

        private void CrearFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = NombreFamiliaTextbox.Text.Trim();

                Familia_234TL nuevaFamilia = familiaBLL.CrearNuevaFamilia(nombre);


                TreeNode nuevoNodo = CrearNodo(nuevaFamilia);
                FamiliasTreeView.Nodes.Add(nuevoNodo);

                NombreFamiliaTextbox.Clear();
                Utilitarios_234TL.MensajeExito("FamiliaCreadaExito");
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
                if (ex.Nombre == "nombre") NombreFamiliaTextbox.Focus();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoCrearFamilia", ex);
            }
        }
        private void AgregarAFamiliaFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                var familiaPadre = FamiliasTreeView.SelectedNode?.Tag as Familia_234TL;
                var familiaHija = this.temporal;

                if (familiaPadre == null || familiaHija == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarFamiliaPadreHija");
                    return;
                }
                familiaBLL.AgregarHijo(familiaPadre, familiaHija);

                Utilitarios_234TL.MensajeExito("FamiliaAgregadaExito");
                CargarFamilias();
                CargarPerfiles();

                temporal = null;
                FamiliaSeleccionadaLabel.Text = "Familia seleccionada: (ninguna)";
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoAgregarFamilia", ex);
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



        #region Perfiles
        private void AgregarAPerfilPermisoButton_Click(object sender, EventArgs e)
        {
            try
            {
                var perfilSeleccionado = PerfilesTreeView.SelectedNode?.Tag as Perfil_234TL;
                var permisoSeleccionado = PermisosListbox.SelectedItem as Permiso_234TL;

                if (perfilSeleccionado == null || permisoSeleccionado == null)
                {
                    Utilitarios_234TL.MensajeError("SeleccionarPerfilYPermiso");
                    return;
                }

                perfilBLL.AgregarComponenteAPerfil(perfilSeleccionado, permisoSeleccionado);

                CargarPerfiles();
                Utilitarios_234TL.MensajeExito("PermisoAgregadoAPerfilExito");
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoAgregarPermiso", ex);
            }
        }

        private void AgregarAPerfilFamiliaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PerfilesTreeView.SelectedNode?.Tag is not Perfil_234TL perfil)
                {
                    Utilitarios_234TL.MensajeError("DebeSeleccionarUnPerfil");
                    return;
                }
                if (FamiliasTreeView.SelectedNode?.Tag is not Familia_234TL familia)
                {
                    Utilitarios_234TL.MensajeError("DebeSeleccionarUnaFamilia");
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
                CargarFamilias();
                CargarPerfiles();
                Utilitarios_234TL.MensajeExito("FamiliaAgregadaAPerfilExito");
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
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
                    Utilitarios_234TL.MensajeExito("ComponenteEliminadoDePerfilExito");
                }
                else
                {
                    Utilitarios_234TL.MensajeError("ErrorAlEliminarComponente");
                }
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
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
                    Utilitarios_234TL.MensajeError("DebeSeleccionarUnPerfil");
                    return;
                }

                if (Utilitarios_234TL.MensajeConfirmacion(this,"ConfirmacionEliminarPerfil") == DialogResult.Yes)
                {
                    perfilBLL.Eliminar(perfil);
                    Utilitarios_234TL.MensajeExito("PerfilEliminadoExito");
                    CargarPerfiles();
                }
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoEliminarPerfil", ex);
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
                Utilitarios_234TL.MensajeExito("PerfilCreadoExito");
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
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
