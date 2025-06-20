using BLL_234TL;
using Servicios_234TL;
using System.Text.RegularExpressions;
using Wilhjem;
using Servicios_234TL.Observer_234TL;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace GUI_234TL
{
    public partial class FormUsuarios_234TL : Form, IObserver_234TL<Dictionary<string, string>>
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
                Utilitarios_234TL.MensajeError("Mensaje_SeleccioneUsuario");
                return;
            }
            var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

            if (usuario.Rol == RolAdmin)
            {
                Utilitarios_234TL.MensajeError("Mensaje_NoModificarSuperAdmin");
                return;
            }

            if (usuario.Activo)
            {
                string mensaje = string.Format(traducciones["Mensaje_ConfirmarDesactivacion"], usuario.Login);
                var Confirmar = MessageBox.Show(mensaje, traducciones["MensajeTitulo_Confirmacion"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmar == DialogResult.Yes)
                {
                    bll.DesactivarUsuario(usuario);
                    Utilitarios_234TL.MensajeExito("Mensaje_UsuarioDesactivado");
                }
            }
            else
            {
                string mensaje = string.Format(traducciones["Mensaje_ConfirmarActivacion"], usuario.Login);
                var Confirmar2 = MessageBox.Show(mensaje, traducciones["MensajeTitulo_Confirmacion"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            switch (ModoActual)
            {
                case ModoFormulario.ModoConsulta:
                    CargarUsuarios();
                    break;

                case ModoFormulario.ModoCrear:
                    var nuevousuario = new Usuario_234TL();

                    if (!ERRORUsuario(nuevousuario, out string errormsg))
                    {
                        Utilitarios_234TL.MensajeError(errormsg);
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
                    Utilitarios_234TL.MensajeExito("Mensaje_UsuarioCreado");
                    CargarUsuarios();
                    LimpiarCampos();
                    break;

                case ModoFormulario.ModoModificar:

                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Mensaje_SeleccioneUsuario");
                        return;
                    }

                    var usuario = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;

                    if (usuario.Rol == RolAdmin)
                    {
                        Utilitarios_234TL.MensajeError("Mensaje_NoModificarSuperAdmin");
                        return;
                    }

                    if (!ERRORUsuario(usuario, out string errormsgmodificar))
                    {
                        Utilitarios_234TL.MensajeError(errormsgmodificar);
                        return;
                    }
                    bll.GenerarLogin(usuario);
                    bll.Update(usuario);
                    Utilitarios_234TL.MensajeExito("Mensaje_UsuarioModificado");
                    CargarUsuarios();
                    if (dataGridViewUsuarios.CurrentRow != null)
                        CargarDatosUsuario();
                    break;

                case ModoFormulario.ModoEliminar:

                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Mensaje_SeleccioneUsuario");
                        return;
                    }
                    var usuarioeliminar = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (usuarioeliminar.Rol == RolAdmin)
                    {
                        Utilitarios_234TL.MensajeError("Mensaje_NoEliminarAdmin");
                        return;
                    }
                    bll.Eliminar(usuarioeliminar);
                    Utilitarios_234TL.MensajeExito("Mensaje_UsuarioEliminado");
                    CargarUsuarios();

                    break;

                case ModoFormulario.ModoDesbloquear:
                    if (dataGridViewUsuarios.CurrentRow == null)
                    {
                        Utilitarios_234TL.MensajeError("Mensaje_SeleccioneUsuario");
                        return;
                    }
                    var usuarioseleccionado = (Usuario_234TL)dataGridViewUsuarios.CurrentRow.DataBoundItem;
                    if (!usuarioseleccionado.Bloqueado)
                    {
                        Utilitarios_234TL.MensajeInformacion("Mensaje_UsuarioNoBloqueado");
                        return;
                    }
                    bll.DesbloquearUsuario(usuarioseleccionado);
                    CargarUsuarios();
                    ColorearFilasBloqueadas();
                    Utilitarios_234TL.MensajeExito("Mensaje_UsuarioDesbloqueado");
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
                errormsg = "Error_ModificarDNI";
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            //DNI
            string dni = DNItextBox.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                errormsg = "Error_DNI_Requerido";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }
            if (Crear)
            {
                usuario.DNI = dni;
                if (bll.ExisteDni(usuario.DNI))
                {
                    errormsg = "Error_DNI_Existente";
                    DNItextBox.Focus();
                    DNItextBox.BackColor = Color.LightPink;
                    return false;
                }
            }

            usuario.DNI = dni;
            if (Crear && bll.ExisteDni(usuario.DNI))
            {
                errormsg = "Error_DNI_Existente";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }
            else if (!Crear && bll.ExisteDni(usuario.DNI, usuario.Login))
            {
                errormsg = "Error_DNI_PerteneceOtro";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            if (!Regex.IsMatch(dni, @"^\d{8}$"))
            {
                errormsg = "Error_DNI_Formato";
                DNItextBox.Focus();
                DNItextBox.BackColor = Color.LightPink;
                return false;
            }

            DNItextBox.BackColor = SystemColors.Window;

            //Nombre
            string nombre = NombretextBox.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                errormsg = "Error_Nombre_Requerido";
                NombretextBox.Focus();
                NombretextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                errormsg = "Error_Nombre_Formato";
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
                errormsg = "Error_Apellido_Requerido";
                ApellidotextBox.Focus();
                ApellidotextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                errormsg = "Error_Apellido_Formato";
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
                errormsg = "Error_Email_Requerido";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            if (Crear && bll.ExisteEmail(mail))
            {
                errormsg = "Error_Email_Existente";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            else if (!Crear && bll.ExisteEmail(mail, usuario.Login))
            {
                errormsg = "Error_Email_PerteneceOtro";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            if (!Regex.IsMatch(mail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errormsg = "Error_Email_Formato";
                EmailtextBox.Focus();
                EmailtextBox.BackColor = Color.LightPink;
                return false;
            }
            usuario.Email = mail;
            EmailtextBox.BackColor = SystemColors.Window;

            //Rol
            if (string.IsNullOrEmpty(RolcomboBox.Text))
            {
                errormsg = "Error_Rol_Requerido";
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

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormUsuarios_234TL_Title"];
            CrearButton.Text = Traduccion["FormUsuarios_234TL_CrearButton"];
            ModificarButton.Text = Traduccion["FormUsuarios_234TL_ModificarButton"];
            DesbloquearButton.Text = Traduccion["FormUsuarios_234TL_DesbloquearButton"];
            ActDesactButton.Text = Traduccion["FormUsuarios_234TL_ActivarDesactivarButton"];
            AplicarButton.Text = Traduccion["FormUsuarios_234TL_AplicarButton"];
            Eliminarbutton.Text = Traduccion["FormUsuarios_234TL_EliminarButton"];
            ConsultaButton.Text = Traduccion["FormUsuarios_234TL_ConsultaButton"];

            label1.Text = Traduccion["FormUsuarios_234TL_Label_DNI"];
            label2.Text = Traduccion["FormUsuarios_234TL_Label_Nombre"];
            label3.Text = Traduccion["FormUsuarios_234TL_Label_Apellido"];
            label4.Text = Traduccion["FormUsuarios_234TL_Label_Email"];
            label5.Text = Traduccion["FormUsuarios_234TL_Label_Rol"];
            label9.Text = Traduccion["FormUsuarios_234TL_Title"];

            radioButtonActivo.Text = Traduccion["FormUsuarios_234TL_Label_Activos"];
            TodosradioButton.Text = Traduccion["FormUsuarios_234TL_Label_Todos"];
            AtributoscheckBox.Text = Traduccion["FormUsuarios_234TL_Label_Atributos"];

            if (dataGridViewUsuarios.Columns.Count > 0)
            {
                dataGridViewUsuarios.Columns[0].HeaderText = Traduccion["FormUsuarios_234TL_Label_DNI"];
                dataGridViewUsuarios.Columns[1].HeaderText = Traduccion["FormUsuarios_234TL_Label_Nombre"];
                dataGridViewUsuarios.Columns[2].HeaderText = Traduccion["FormUsuarios_234TL_Label_Apellido"];
                dataGridViewUsuarios.Columns[3].HeaderText = Traduccion["FormUsuarios_234TL_Label_Email"];
                dataGridViewUsuarios.Columns[4].HeaderText = Traduccion["FormUsuarios_234TL_Label_Rol"];
                dataGridViewUsuarios.Columns[5].HeaderText = Traduccion["FormUsuarios_234TL_Label_Bloqueado"];
                dataGridViewUsuarios.Columns[6].HeaderText = Traduccion["FormUsuarios_234TL_Label_Activo"];
                dataGridViewUsuarios.Columns[7].HeaderText = Traduccion["FormUsuarios_234TL_Label_Login"];
                dataGridViewUsuarios.Columns[8].HeaderText = Traduccion["FormUsuarios_234TL_Label_Password"];
                dataGridViewUsuarios.Columns[9].HeaderText = Traduccion["FormUsuarios_234TL_Label_IntentosFallidos"];
                dataGridViewUsuarios.Columns[10].HeaderText = Traduccion["FormUsuarios_234TL_Label_UltimoIntentoFallido"];

            }

        }


        private string TraducirModo(ModoFormulario modo)
        {
            try
            {
                var traducciones = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
                string clave = modo.ToString();
                return traducciones[clave];
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