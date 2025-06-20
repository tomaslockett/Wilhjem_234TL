using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormInicioSesion_234TL : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private UsuarioBLL_234TL usuarioBLL;

        public FormInicioSesion_234TL()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = true;
            usuarioBLL = new UsuarioBLL_234TL();
            textBox2.UseSystemPasswordChar = true;
            this.BackColor = Color.FromArgb(255, 192, 192, 192);
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }



        private void IngresarButton_Click(object sender, EventArgs e)
        {
            string Login = textBox1.Text;
            string Password = Encryptador_234TL.SHA256Encrpytar_234TL(textBox2.Text);

            IngresarButton.Enabled = false;
            try
            {
                if (string.IsNullOrEmpty(Login))
                    throw new Exception("Mensaje_LoginRequerido");

                if (string.IsNullOrEmpty(Password))
                    throw new Exception("Mensaje_PasswordRequerido");

                var resultado = usuarioBLL.Login(Login, Password);

                switch (resultado)
                {
                    case Resultados_234TL.UsuarioLogueado:
                        Utilitarios_234TL.MensajeError("Mensaje_UsuarioYaLogueado");
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioValido:
                        Utilitarios_234TL.MensajeExito("Mensaje_UsuarioValido");
                        textBox1.Clear();
                        textBox2.Clear();

                        if (this.Owner is FormPrincipal_234TL formPrincipal)
                        {
                            formPrincipal.ActualizarUsuarioLogueado(usuarioBLL.GetUsuarioLogueado());
                            IdiomasManager_234TL.Instancia.NotificarActuales();
                        }
                        else
                        {
                            MessageBox.Show("Mensaje_ErrorActualizarUsuario");
                        }
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioBloqueado:
                        Utilitarios_234TL.MensajeError("Mensaje_UsuarioBloqueado");
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                        break;

                    case Resultados_234TL.ContrasenaInvalida:
                        Utilitarios_234TL.MensajeAdvertencia("Mensaje_ContrasenaInvalida");
                        break;

                    case Resultados_234TL.UsuarioNoExiste:
                        Utilitarios_234TL.MensajeError("Mensaje_UsuarioNoExiste");
                        break;

                    case Resultados_234TL.UsuarioInactivo:
                        Utilitarios_234TL.MensajeError("Mensaje_UsuarioInactivo");
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                        break;
                }
            }
            catch (Exepcion_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);
            }
            finally
            {
                IngresarButton.Enabled = true;
            }
        }

        private void FormInicioSesion_234TL_Load(object sender, EventArgs e)
        {
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

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void VolverButton_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
            this.Close();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormInicioSesion_234TL_Title"];
            label1.Text = Traduccion["FormInicioSesion_234TL_Label_Titulo"];
            label2.Text = Traduccion["FormInicioSesion_234TL_Label_Usuario"];
            label3.Text = Traduccion["FormInicioSesion_234TL_Label_Contraseña"];
            IngresarButton.Text = Traduccion["FormInicioSesion_234TL_Boton_Ingresar"];
            VolverButton.Text = Traduccion["FormInicioSesion_234TL_Boton_Volver"];
            label4.Text = Traduccion["FormInicioSesion_234TL_Label_EjemploUsuario"];
            label5.Text = Traduccion["FormInicioSesion_234TL_Label_EjemploContraseña"];
            MostrarButton.Text = Traduccion["FormInicioSesion_234TL_CheckBox_MostrarContraseña"];
        }

        private void FormInicioSesion_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}