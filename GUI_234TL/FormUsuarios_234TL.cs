using System.Text.RegularExpressions;
using BLL_234TL;
using Servicios_234TL;

namespace GUI_234TL
{
    public partial class FormUsuarios_234TL : Form
    {
        private static FormUsuarios_234TL instancia;

        private const string RolAdmin = "admin";

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

        private bool Activo;

        public FormUsuarios_234TL()
        {
            InitializeComponent();

            configurarDatagridView();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = true;
            radioButtonActivo.Checked = true;
            bool Activo = radioButtonActivo.Checked;
            CambiarModo(ModoFormulario.ModoConsulta);
            CargarUsuarios();

            #region Color Fondo

            this.BackColor = Color.FromArgb(255, 192, 192, 192);

            #endregion Color Fondo

            #region ColorBotones

            ConsultaButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            ConsultaButton.ForeColor = Color.White;
            CrearButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            CrearButton.ForeColor = Color.White;
            ModificarButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            ModificarButton.ForeColor = Color.White;
            ActDesactButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            ActDesactButton.ForeColor = Color.White;
            DesbloquearButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            DesbloquearButton.ForeColor = Color.White;
            AplicarButton.BackColor = ColorTranslator.FromHtml("#043f5b");
            AplicarButton.ForeColor = Color.White;
            Eliminarbutton.BackColor = ColorTranslator.FromHtml("#043f5b");
            Eliminarbutton.ForeColor = Color.White;

            #endregion ColorBotones
        }

        private void CargarDatosUsuario()
        {
            var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            DNItextBox.Text = usuario.DNI.ToString();
            NombretextBox.Text = usuario.Nombre;
            ApellidotextBox.Text = usuario.Apellido;
            EmailtextBox.Text = usuario.Email;
            RolcomboBox.Text = usuario.Rol;
        }

        private void OcultarColumnas()
        {
            dataGridViewUsuarios.Columns["Login"].Visible = false;
            dataGridViewUsuarios.Columns["Activo"].Visible = false;
            dataGridViewUsuarios.Columns["Password"].Visible = false;
            dataGridViewUsuarios.Columns["IntentosFallidos"].Visible = false;
            dataGridViewUsuarios.Columns["Bloqueado"].Visible = false;
        }

        private void ColorearFilasBloqueadas()
        {
            foreach (DataGridViewRow row in dataGridViewUsuarios.Rows)
            {
                var usuario = (Usuario_234TL)row.DataBoundItem;
                if (usuario.Bloqueado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

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

        private void CargarUsuarios()
        {
            Activo = radioButtonActivo.Checked;
            var usuarios = bll.GetUsuariosActivos(Activo);
            dataGridViewUsuarios.DataSource = usuarios;
            OcultarColumnas();
            ColorearFilasBloqueadas();
        }

        private void CrearButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoCrear);
        }

        private void DesbloquearButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoDesbloquear);
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoModificar);
        }

        private void ActDesactButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para activar/desactivar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

            if (usuario.Rol == RolAdmin)
            {
                MessageBox.Show("No se puede activar/desactivar un usuario administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuario.Activo)
            {
                var Confirmar = MessageBox.Show($"¿Seguro que desea desactivar al usuario “{usuario.Login}”?", "Confirmar desactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmar == DialogResult.Yes)
                {
                    bll.DesactivarUsuario(usuario);
                    MessageBox.Show("Usuario desactivado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                var Confirmar2 = MessageBox.Show($"¿Seguro que desea activar al usuario “{usuario.Login}”?", "Confirmar activación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmar2 == DialogResult.Yes)
                {
                    bll.ActivarUsuario(usuario);
                    MessageBox.Show("Usuario activado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            CargarUsuarios();
            CargarDatosUsuario();
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
                    MessageBox.Show("Usuario creado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                    LimpiarCampos();
                    break;

                case ModoFormulario.ModoModificar:

                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        MessageBox.Show("Seleccione un usuario para modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

                    if (usuario.Rol == RolAdmin)
                    {
                        MessageBox.Show("No se puede modificar un usuario administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!ERRORUsuario(usuario, out string errormsgmodificar))
                    {
                        MessageBox.Show(errormsgmodificar, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Seleccione un usuario para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var usuarioeliminar = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (usuarioeliminar.Rol == RolAdmin)
                    {
                        MessageBox.Show("No se puede eliminar un usuario administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bll.Eliminar(usuarioeliminar);
                    CargarUsuarios();

                    break;

                case ModoFormulario.ModoDesbloquear:
                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        MessageBox.Show("Seleccione un usuario para desbloquear", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var usuarioseleccionado = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (!usuarioseleccionado.Bloqueado)
                    {
                        MessageBox.Show("Este usuario ya está desbloqueado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bll.DesbloquearUsuario(usuarioseleccionado);
                    CargarUsuarios();
                    MessageBox.Show("Usuario desbloqueado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                default:
                    break;
            }
        }

        private void ConsultaButton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoConsulta);
        }

        // Devuelve Dos cosas el bool y el out que es un string para decir que mierda esta mal
        // lo demas es puro regex y cambiar colorcitos
        private bool ERRORUsuario(Usuario_234TL usuario, out string errormsg)
        {
            errormsg = string.Empty;
            bool Crear = ModoActual == ModoFormulario.ModoCrear;
            bool Modificar = ModoActual == ModoFormulario.ModoModificar;

            //DNI
            string dni = DNItextBox.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                errormsg = "El campo DNI es obligatorio.";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }
            usuario.DNI = int.Parse(dni);
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

        private void radioButtonActivo_CheckedChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void TodosradioButton_CheckedChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void dataGridViewUsuarios_SelectionChanged(object sender, EventArgs e)
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

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.ModoEliminar);
        }

        private void VerradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (VerradioButton.Checked)
            {
                MostrarTodasColumnas();
            }
            else
            {
                OcultarColumnas();
            }
        }

        #region Funciones

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

        #endregion Funciones
    }
}