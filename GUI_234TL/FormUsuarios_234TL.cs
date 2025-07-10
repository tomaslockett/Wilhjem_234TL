using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System.Text.RegularExpressions;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormUsuarios_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private const string RolAdmin = "SuperAdmin";
        private UsuarioBLL_234TL bll = new BLL_234TL.UsuarioBLL_234TL();
        private ModoFormulario ModoActual = ModoFormulario.ModoConsulta;

        public FormUsuarios_234TL()
        {
            InitializeComponent();

            configurarDatagridView();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = true;
            radioButtonActivo.Checked = true;
            CambiarModo(ModoFormulario.ModoConsulta);
            CargarUsuarios();
            CargarPerfilesEnComboBox();
            dataGridViewUsuarios.DataBindingComplete += DataGridCompletado;
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();



            #region Color Fondo

            this.BackColor = Color.FromArgb(255, 192, 192, 192);

            #endregion Color Fondo

            #region ColorBotones

            AplicarEstiloBoton(CrearButton);
            AplicarEstiloBoton(ModificarButton);
            AplicarEstiloBoton(ActDesactButton);
            AplicarEstiloBoton(DesbloquearButton);
            AplicarEstiloBoton(AplicarButton);
            AplicarEstiloBoton(Eliminarbutton);
            AplicarEstiloBoton(ConsultaButton);

            #endregion ColorBotones
        }


        #region Botones

        #region Consulta

        private void ConsultaButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoConsulta);
        }

        #endregion Consulta

        #region Crear

        private void CrearButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoCrear);
        }

        #endregion Crear

        #region Modificar

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoModificar);
        }

        #endregion Modificar

        #region Desbloquear

        private void DesbloquearButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoDesbloquear);
        }

        #endregion Desbloquear

        #region Eliminar

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoEliminar);
        }

        #endregion Eliminar

        #region Activar/Desactivar

        private void ActDesactButton_Click(object sender, EventArgs e)
        {
            var traducciones = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();

            if (dataGridViewUsuarios.CurrentRow == null)
            {
                Utilitarios_234TL.MensajeError("SeleccioneUsuario");
                return;
            }
            var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

            if (usuario.Perfil?.Nombre == RolAdmin)
            {
                Utilitarios_234TL.MensajeError("NoModificarSuperAdmin");
                return;
            }

            if (usuario.Activo)
            {
                if (Utilitarios_234TL.MensajeConfirmacion(this,"DesactivarUsuario", usuario.Login) == DialogResult.Yes)
                {
                    bll.DesactivarUsuario(usuario);
                    Utilitarios_234TL.MensajeExito("UsuarioDesactivado");
                }
            }
            else
            {
                if (Utilitarios_234TL.MensajeConfirmacion(this,"ActivarUsuario", usuario.Login) == DialogResult.Yes)
                {
                    bll.ActivarUsuario(usuario);
                    Utilitarios_234TL.MensajeExito("UsuarioActivado");
                }
            }

            CargarUsuarios();
            CargarDatosUsuario();
        }

        #endregion Activar/Desactivar

        #endregion Botones

        #region RadioButtons

        private void radioButtonActivo_CheckedChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void TodosradioButton_CheckedChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void AtributoscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AtributoscheckBox.Checked)
            {
                MostrarTodasColumnas();
            }
            else
            {
                OcultarColumnas();
            }
        }

        #endregion RadioButtons

        #region Datagrid

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            CargarDatosUsuario();
        }

        #endregion Datagrid

        private void CambiarModo(ModoFormulario modo)
        {
            ModoActual = modo;
            Modo.Text = TraducirModo(modo);
            switch (modo)
            {
                case ModoFormulario.ModoConsulta:
                    CrearButton.Enabled = true;
                    ModificarButton.Enabled = true;
                    DesbloquearButton.Enabled = true;
                    ActDesactButton.Enabled = false;
                    AplicarButton.Enabled = false;
                    ConsultaButton.Enabled = false;
                    Eliminarbutton.Enabled = true;
                    SetSoloLeer(true);
                    Blanco();
                    LimpiarCampos();
                    break;

                case ModoFormulario.ModoCrear:
                    CrearButton.Enabled = false;
                    ModificarButton.Enabled = false;
                    DesbloquearButton.Enabled = false;
                    ActDesactButton.Enabled = false;
                    AplicarButton.Enabled = true;
                    ConsultaButton.Enabled = true;
                    Eliminarbutton.Enabled = false;
                    SetSoloLeer(false);
                    Blanco();
                    break;

                case ModoFormulario.ModoModificar:
                    AplicarButton.Enabled = true;
                    CrearButton.Enabled = false;
                    ModificarButton.Enabled = false;
                    ActDesactButton.Enabled = true;
                    DesbloquearButton.Enabled = false;
                    ConsultaButton.Enabled = true;
                    Eliminarbutton.Enabled = false;
                    SetSoloLeer(false);
                    DNItextBox.ReadOnly = true;
                    Blanco();
                    break;

                case ModoFormulario.ModoEliminar:
                    AplicarButton.Enabled = true;
                    CrearButton.Enabled = false;
                    ModificarButton.Enabled = false;
                    DesbloquearButton.Enabled = false;
                    ConsultaButton.Enabled = true;
                    Eliminarbutton.Enabled = false;
                    SetSoloLeer(true);
                    Blanco();
                    break;

                case ModoFormulario.ModoDesbloquear:
                    AplicarButton.Enabled = true;
                    ActDesactButton.Enabled = false;
                    CrearButton.Enabled = false;
                    ModificarButton.Enabled = false;
                    DesbloquearButton.Enabled = false;
                    ConsultaButton.Enabled = true;
                    Eliminarbutton.Enabled = false;
                    SetSoloLeer(true);
                    Blanco();
                    break;

                default:
                    break;
            }
        }

        private void AplicarButton_Click(object sender, EventArgs e)
        {
            LimpiarColoresError();
            try
            {
                switch (ModoActual)
                {
                    case ModoFormulario.ModoConsulta:
                        CargarUsuarios();
                        break;

                    case ModoFormulario.ModoCrear:
                        var nuevoUsuario = new Usuario_234TL
                        {
                            DNI = DNItextBox.Text.Trim(),
                            Nombre = NombretextBox.Text.Trim(),
                            Apellido = ApellidotextBox.Text.Trim(),
                            Email = EmailtextBox.Text.Trim(),
                            Perfil = RolcomboBox.SelectedItem as Perfil_234TL
                        };

                        bll.CrearNuevoUsuario(nuevoUsuario);
                        Utilitarios_234TL.MensajeExito("UsuarioCreado");
                        break;

                    case ModoFormulario.ModoModificar:

                        if (dataGridViewUsuarios.CurrentRow == null)
                        {
                            Utilitarios_234TL.MensajeError("SeleccioneUsuario");
                            return;
                        }
                        var usuarioModificar = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

                        usuarioModificar.Nombre = NombretextBox.Text.Trim();
                        usuarioModificar.Apellido = ApellidotextBox.Text.Trim();
                        usuarioModificar.Email = EmailtextBox.Text.Trim();
                        usuarioModificar.Perfil = RolcomboBox.SelectedItem as Perfil_234TL;

                        bll.ModificarUsuario(usuarioModificar);
                        Utilitarios_234TL.MensajeExito("UsuarioModificado");
                        break;

                    case ModoFormulario.ModoEliminar:

                        if (dataGridViewUsuarios.CurrentRow == null)
                        {
                            Utilitarios_234TL.MensajeError("SeleccioneUsuario");
                            return;
                        }
                        var usuarioeliminar = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                        if (usuarioeliminar.Perfil?.Nombre == RolAdmin)
                        {
                            Utilitarios_234TL.MensajeError("NoEliminarAdmin");
                            return;
                        }
                        if (Utilitarios_234TL.MensajeConfirmacion(this,"EliminarUsuario") != DialogResult.Yes)
                        {
                            return;
                        }
                        bll.Eliminar(usuarioeliminar);
                        Utilitarios_234TL.MensajeExito("UsuarioEliminado");
                        CargarUsuarios();

                        break;

                    case ModoFormulario.ModoDesbloquear:
                        if (dataGridViewUsuarios.CurrentRow == null)
                        {
                            Utilitarios_234TL.MensajeError("SeleccioneUsuario");
                            return;
                        }
                        var usuarioseleccionado = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                        if (!usuarioseleccionado.Bloqueado)
                        {
                            Utilitarios_234TL.MensajeInformacion("UsuarioNoBloqueado");
                            return;
                        }
                        bll.DesbloquearUsuario(usuarioseleccionado);
                        CargarUsuarios();
                        ColorearFilasBloqueadas();
                        Utilitarios_234TL.MensajeExito("UsuarioDesbloqueado");
                        break;

                    default:
                        break;
                }
                CargarUsuarios();
                LimpiarCampos();
            }
            catch(ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);

                switch (ex.Nombre)
                {
                    case "DNI":
                        DNItextBox.BackColor = Color.LightPink;
                        DNItextBox.Focus();
                        break;
                    case "Nombre":
                        NombretextBox.BackColor = Color.LightPink;
                        NombretextBox.Focus();
                        break;
                    case "Apellido":
                        ApellidotextBox.BackColor = Color.LightPink;
                        ApellidotextBox.Focus();
                        break;
                    case "Email":
                        EmailtextBox.BackColor = Color.LightPink;
                        EmailtextBox.Focus();
                        break;
                    case "Perfil":
                        RolcomboBox.BackColor = Color.LightPink;
                        RolcomboBox.Focus();
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);
            }
            catch (InvalidOperationException ex) 
            {
                Utilitarios_234TL.MensajeError(ex.Message);
            }
            catch (Exception ex) 
            {
                Utilitarios_234TL.MensajeError($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        #region Funciones

        private void AplicarEstiloBoton(Button button)
        {
            button.BackColor = ColorTranslator.FromHtml("#007ACC");
            button.ForeColor = Color.White;
        }

        private void MostrarTodasColumnas()
        {
            foreach (DataGridViewColumn column in dataGridViewUsuarios.Columns)
            {
                column.Visible = true;
            }
        }
        private void LimpiarCampos()
        {
            DNItextBox.Clear();
            NombretextBox.Clear();
            ApellidotextBox.Clear();
            EmailtextBox.Clear();
            RolcomboBox.SelectedIndex = -1;
        }

        private void configurarDatagridView()
        {
            dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsuarios.MultiSelect = false;
            dataGridViewUsuarios.ReadOnly = true;
            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.AllowUserToDeleteRows = false;
            dataGridViewUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewUsuarios.AllowUserToResizeRows = false;
            dataGridViewUsuarios.RowHeadersVisible = false;
        }

        private void SetSoloLeer(bool soloLeer)
        {
            DNItextBox.ReadOnly = soloLeer;
            NombretextBox.ReadOnly = soloLeer;
            ApellidotextBox.ReadOnly = soloLeer;
            EmailtextBox.ReadOnly = soloLeer;
            RolcomboBox.Enabled = !soloLeer;
        }
        private void LimpiarColoresError()
        {
            DNItextBox.BackColor = SystemColors.Window;
            NombretextBox.BackColor = SystemColors.Window;
            ApellidotextBox.BackColor = SystemColors.Window;
            EmailtextBox.BackColor = SystemColors.Window;
            RolcomboBox.BackColor = SystemColors.Window;
        }

        private void Blanco()
        {
            DNItextBox.BackColor = SystemColors.Window;
            NombretextBox.BackColor = SystemColors.Window;
            ApellidotextBox.BackColor = SystemColors.Window;
            EmailtextBox.BackColor = SystemColors.Window;
            RolcomboBox.BackColor = SystemColors.Window;
        }

        private void CargarDatosUsuario()
        {
            if (dataGridViewUsuarios.CurrentRow != null)
            {
                var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                DNItextBox.Text = usuario.DNI.ToString();
                NombretextBox.Text = usuario.Nombre;
                ApellidotextBox.Text = usuario.Apellido;
                EmailtextBox.Text = usuario.Email;
                if (usuario.Perfil != null)
                {
                    RolcomboBox.SelectedValue = usuario.Perfil.IdPerfil;
                }
                else
                {
                    RolcomboBox.SelectedIndex = -1;
                }
            }
        }

        private void OcultarColumnas()
        {
            dataGridViewUsuarios.Columns["Login"].Visible = false;
            dataGridViewUsuarios.Columns["Activo"].Visible = false;
            dataGridViewUsuarios.Columns["Password"].Visible = false;
            dataGridViewUsuarios.Columns["IntentosFallidos"].Visible = false;
            dataGridViewUsuarios.Columns["Bloqueado"].Visible = false;
            dataGridViewUsuarios.Columns["UltimoIntentoFallido"].Visible = false;
        }

        private void ColorearFilasBloqueadas()
        {
            foreach (DataGridViewRow row in dataGridViewUsuarios.Rows)
            {
                if (row.DataBoundItem is Usuario_234TL usuario)
                {
                    row.DefaultCellStyle.BackColor = dataGridViewUsuarios.DefaultCellStyle.BackColor;
                    if (usuario.Bloqueado)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
        }

        private void CargarUsuarios()
        {
            var usuarios = bll.GetAll();
            if (radioButtonActivo.Checked)
            {
                usuarios = usuarios.Where(u => u.Activo == true).ToList();
            }

            dataGridViewUsuarios.DataSource = usuarios;
            if (AtributoscheckBox.Checked)
            {
                MostrarTodasColumnas();
            }
            else
            {
                OcultarColumnas();
            }
        }
        private void DataGridCompletado(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorearFilasBloqueadas();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {

                var textos = Traduccion.Forms.Usuarios;

                this.Text = textos.Title;

                CrearButton.Text = textos.CrearButton;
                ModificarButton.Text = textos.ModificarButton;
                Eliminarbutton.Text = textos.EliminarButton;
                DesbloquearButton.Text = textos.DesbloquearButton;
                ActDesactButton.Text = textos.ActivarDesactivarButton;
                AplicarButton.Text = textos.AplicarButton;
                ConsultaButton.Text = textos.ConsultaButton;


                label1.Text = textos.Label_DNI;
                label3.Text = textos.Label_Nombre;
                label2.Text = textos.Label_Apellido;
                label4.Text = textos.Label_Email;
                label5.Text = textos.Label_Rol;
                label9.Text = textos.Label_Titulo;

                radioButtonActivo.Text = textos.Label_Activos;
                TodosradioButton.Text = textos.Label_Todos;
                AtributoscheckBox.Text = textos.Label_Atributos;

                if (dataGridViewUsuarios.Columns.Count > 0)
                {
                    dataGridViewUsuarios.Columns["DNI"].HeaderText = textos.Columna_DNI;
                    dataGridViewUsuarios.Columns["Nombre"].HeaderText = textos.Columna_Nombre;
                    dataGridViewUsuarios.Columns["Apellido"].HeaderText = textos.Columna_Apellido;
                    dataGridViewUsuarios.Columns["Email"].HeaderText = textos.Columna_Email;
                    dataGridViewUsuarios.Columns["Perfil"].HeaderText = textos.Columna_Rol;
                    dataGridViewUsuarios.Columns["Bloqueado"].HeaderText = textos.Columna_Bloqueado;
                    dataGridViewUsuarios.Columns["Activo"].HeaderText = textos.Columna_Activo;
                    dataGridViewUsuarios.Columns["Login"].HeaderText = textos.Columna_Login;
                    dataGridViewUsuarios.Columns["Password"].HeaderText = textos.Columna_Password;
                    dataGridViewUsuarios.Columns["IntentosFallidos"].HeaderText = textos.Columna_IntentosFallidos;
                    dataGridViewUsuarios.Columns["UltimoIntentoFallido"].HeaderText = textos.Columna_UltimoIntentoFallido;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar traducciones en FormUsuarios: {ex.Message}");
            }

        }

        private void CargarPerfilesEnComboBox()
        {
            try
            {
                var perfiles = bll.ObtenerPerfilesDisponibles();
                RolcomboBox.DataSource = perfiles;
                RolcomboBox.DisplayMember = "Nombre";
                RolcomboBox.ValueMember = "IdPerfil";
                RolcomboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError($"Error al cargar los perfiles: {ex.Message}");
            }
        }


        private string TraducirModo(ModoFormulario modo)
        {
            try
            {
                var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();

                switch (modo)
                {
                    case ModoFormulario.ModoConsulta: return traduccion.Modos.ModoConsulta;
                    case ModoFormulario.ModoCrear: return traduccion.Modos.ModoCrear;
                    case ModoFormulario.ModoModificar: return traduccion.Modos.ModoModificar;
                    case ModoFormulario.ModoEliminar: return traduccion.Modos.ModoEliminar;
                    case ModoFormulario.ModoDesbloquear: return traduccion.Modos.ModoDesbloquear;
                    default: return modo.ToString();
                }
            }
            catch
            {
                return modo.ToString();
            }
        }

        #endregion Funciones

        private void FormUsuarios_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}