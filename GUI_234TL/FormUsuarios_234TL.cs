using BLL_234TL;
using Servicios_234TL;
using System.Text.RegularExpressions;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormUsuarios_234TL : Form
    {
        private static FormUsuarios_234TL instancia;

        private const string RolAdmin = "SuperAdmin";

        public static FormUsuarios_234TL ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FormUsuarios_234TL();
            }
            return instancia;
        }

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
            dataGridViewUsuarios.DataBindingComplete += DataGridCompletado;

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
            if (dataGridViewUsuarios.CurrentRow == null)
            {
                Utilitarios_234TL.MensajeError("Seleccione un usuario para activar/desactivar");
                return;
            }
            var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

            if (usuario.Rol == RolAdmin)
            {
                Utilitarios_234TL.MensajeError("No se puede activar/desactivar un usuario Super administrador");
                return;
            }

            if (usuario.Activo)
            {
                var Confirmar = MessageBox.Show($"¿Seguro que desea desactivar al usuario “{usuario.Login}”?", "Confirmar desactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmar == DialogResult.Yes)
                {
                    bll.DesactivarUsuario(usuario);
                    Utilitarios_234TL.MensajeExito("Usuario desactivado correctamente");
                }
            }
            else
            {
                var Confirmar2 = MessageBox.Show($"¿Seguro que desea activar al usuario “{usuario.Login}”?", "Confirmar activación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmar2 == DialogResult.Yes)
                {
                    bll.ActivarUsuario(usuario);
                    Utilitarios_234TL.MensajeExito("Usuario activado correctamente");
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
            Modo.Text = $"{modo}";
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
            switch (ModoActual)
            {
                case ModoFormulario.ModoConsulta:
                    CargarUsuarios();
                    break;

                case ModoFormulario.ModoCrear:
                    var nuevousuario = new Usuario_234TL();

                    if (!ERRORUsuario(nuevousuario, out string errormsg))
                    {
                        MessageBox.Show(errormsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //Bloqueado
                    nuevousuario.Bloqueado = false;
                    //Activo
                    nuevousuario.Activo = true;
                    //Intentos fallidos
                    nuevousuario.IntentosFallidos = 0;
                    bll.GenerarCredenciales(nuevousuario);
                    bll.Guardar(nuevousuario);
                    Utilitarios_234TL.MensajeExito("Usuario creado correctamente");
                    CargarUsuarios();
                    LimpiarCampos();
                    break;

                case ModoFormulario.ModoModificar:

                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Seleccione un usuario para modificar");
                        return;
                    }

                    var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

                    if (usuario.Rol == RolAdmin)
                    {
                        Utilitarios_234TL.MensajeError("No se puede modificar un usuario Super administrador");
                        return;
                    }

                    if (!ERRORUsuario(usuario, out string errormsgmodificar))
                    {
                        Utilitarios_234TL.MensajeError(errormsgmodificar);
                        return;
                    }
                    bll.GenerarLogin(usuario);
                    bll.Update(usuario);

                    CargarUsuarios();
                    if (dataGridViewUsuarios.CurrentRow != null)
                        CargarDatosUsuario();
                    break;

                case ModoFormulario.ModoEliminar:

                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Seleccione un usuario para eliminar");
                        return;
                    }
                    var usuarioeliminar = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (usuarioeliminar.Rol == RolAdmin)
                    {
                        Utilitarios_234TL.MensajeError("No se puede eliminar un usuario administrador");
                        return;
                    }
                    bll.Eliminar(usuarioeliminar);
                    CargarUsuarios();

                    break;

                case ModoFormulario.ModoDesbloquear:
                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Seleccione un usuario para desbloquear");
                        return;
                    }
                    var usuarioseleccionado = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (!usuarioseleccionado.Bloqueado)
                    {
                        Utilitarios_234TL.MensajeInformacion("El usuario no está bloqueado.");
                        return;
                    }
                    bll.DesbloquearUsuario(usuarioseleccionado);
                    CargarUsuarios();
                    ColorearFilasBloqueadas();
                    Utilitarios_234TL.MensajeExito("Usuario desbloqueado correctamente");
                    break;

                default:
                    break;
            }
        }

        // Devuelve Dos cosas el bool y el out que es un string para decir que mierda esta mal
        // lo demas es puro regex y cambiar colorcitos
        private bool ERRORUsuario(Usuario_234TL usuario, out string errormsg)
        {
            errormsg = string.Empty;
            bool Crear = ModoActual == ModoFormulario.ModoCrear;
            bool Modificar = ModoActual == ModoFormulario.ModoModificar;

            if (Modificar && DNItextBox.ReadOnly == false)
            {
                errormsg = "No está permitido modificar el DNI.";
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            //DNI
            string dni = DNItextBox.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                errormsg = "El campo DNI es obligatorio.";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }
            if (Crear)
            {
                usuario.DNI = dni;
                if (bll.ExisteDni(usuario.DNI))
                {
                    errormsg = "El DNI ya existe.";
                    DNItextBox.Focus();
                    DNItextBox.BackColor = Color.LightPink;
                    return false;
                }
            }

            usuario.DNI = dni;
            if (Crear && bll.ExisteDni(usuario.DNI))
            {
                errormsg = "El DNI ya existe.";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }
            else if (!Crear && bll.ExisteDni(usuario.DNI, usuario.Login))
            {
                errormsg = "El DNI ya pertenece a otro usuario.";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            if (!Regex.IsMatch(dni, @"^\d{8}$"))
            {
                errormsg = "DNI inválido. Debe contener exactamente 8 dígitos.";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            DNItextBox.BackColor = SystemColors.Window;

            //Nombre
            string nombre = NombretextBox.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                errormsg = "El campo Nombre es obligatorio.";
                NombretextBox.Focus();
                NombretextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                errormsg = "Nombre inválido. Solo letras (2–50 caracteres).";
                NombretextBox.Focus();
                NombretextBox.BackColor = Color.LightPink;
                return false;
            }
            usuario.Nombre = nombre;
            NombretextBox.BackColor = SystemColors.Window;

            //Apellido
            string apellido = ApellidotextBox.Text.Trim();
            if (string.IsNullOrEmpty(apellido))
            {
                errormsg = "El campo Apellido es obligatorio.";
                ApellidotextBox.Focus();
                ApellidotextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                errormsg = "Apellido inválido. Solo letras (2–50 caracteres).";
                ApellidotextBox.Focus();
                ApellidotextBox.BackColor = Color.LightPink;
                return false;
            }
            usuario.Apellido = apellido;
            ApellidotextBox.BackColor = SystemColors.Window;

            //Email
            string mail = EmailtextBox.Text.Trim();
            if (string.IsNullOrEmpty(mail))
            {
                errormsg = "El campo Email es obligatorio.";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            if (Crear && bll.ExisteEmail(mail))
            {
                errormsg = "El Email ya está registrado.";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            else if (!Crear && bll.ExisteEmail(mail, usuario.Login))
            {
                errormsg = "El Email ya pertenece a otro usuario.";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(mail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errormsg = "Formato de Email inválido. Ej: usuario@dominio.com";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            usuario.Email = mail;
            EmailtextBox.BackColor = SystemColors.Window;

            //Rol
            if (string.IsNullOrEmpty(RolcomboBox.Text))
            {
                errormsg = "El campo Rol es obligatorio.";
                RolcomboBox.Focus();
                return false;
            }
            usuario.Rol = RolcomboBox.Text;

            return true;
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
                RolcomboBox.Text = usuario.Rol;
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
                usuarios = usuarios.Where(u => u.Activo == true).ToList();
            dataGridViewUsuarios.DataSource = usuarios;
            if (AtributoscheckBox.Checked)
                MostrarTodasColumnas();
            else
                OcultarColumnas();
        }

        private void DataGridCompletado(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorearFilasBloqueadas();
        }

        #endregion Funciones
    }
}