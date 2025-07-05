using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCambiarContraseña_234TL : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private readonly UsuarioBLL_234TL usuarioBLL = new();
        public FormCambiarContraseña_234TL()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = true;
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
            this.BackColor = Color.FromArgb(255, 192, 192, 192);
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        private void Ingresarbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string actual = textBox1.Text.Trim();
                string nueva = textBox2.Text.Trim();
                string confirmacion = textBox3.Text.Trim();

                if (!SingletonT_234TL<Sesion_234TL>.GetInstance().IsLoggedIn_234TL())
                {
                    Utilitarios_234TL.MensajeError("Mensaje_NoUsuarioLogueado");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    this.Close();
                    return;
                }
                var usuario = usuarioBLL.GetUsuarioLogueado();
                if (usuario == null)
                {
                    Utilitarios_234TL.MensajeError("Mensaje_NoUsuarioLogueado");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Mensaje_CompletarCampos");
                    return;
                }

                if (nueva != confirmacion)
                {
                    Utilitarios_234TL.MensajeError("Mensaje_ContraseñaIncorrecta");
                    return;
                }

                string Actualpassword = Encryptador_234TL.SHA256Encrpytar_234TL(actual);

                if (usuario.Password != Actualpassword)
                {
                    Utilitarios_234TL.MensajeError("Mensaje_ContraseñaIgualAnterior");
                    return;
                }
                if (Encryptador_234TL.SHA256Encrpytar_234TL(nueva) == usuario.Password)
                {
                    Utilitarios_234TL.MensajeError("Mensaje_ContraseñaIgualAnterior");
                    return;
                }
                string nuevaHash = Encryptador_234TL.SHA256Encrpytar_234TL(nueva);
                usuario.Password = nuevaHash;
                usuarioBLL.Update(usuario);
                usuarioBLL.Logout();
                if (this.Owner is FormPrincipal_234TL formPrincipal)
                {
                    formPrincipal.ActualizarUsuarioLogueado(null);
                    IdiomasManager_234TL.Instancia.NotificarActuales();
                }
                Utilitarios_234TL.MensajeExito("Mensaje_ContraseñaCambiada");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                var msg = string.Format(IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales()["Mensaje_ErrorInesperado"], ex.Message);
                MessageBox.Show(msg, IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales()["MensajeTitulo_Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MostrarButton.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void MostarButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (MostarButton2.Checked)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void FormCambiarContraseña_234TL_Load(object sender, EventArgs e)
        {
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Title"];
            label1.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Titulo"];
            label2.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Label_Actual"];
            label3.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Label_Nueva"];
            label4.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Label_Confirmar"];
            Ingresarbutton.Text = Traduccion["FormCambiarContraseñaLabel_234TL_Button_Ingresar"];
            MostrarButton.Text = Traduccion["FormCambiarContraseñaLabel_234TL_CheckBox_Mostrar"];
            MostarButton2.Text = Traduccion["FormCambiarContraseñaLabel_234TL_CheckBox_Mostrar2"];
        }

        private void FormCambiarContraseña_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}