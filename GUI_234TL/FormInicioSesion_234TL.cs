using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormInicioSesion_234TL : Form
    {
        private UsuarioBLL_234TL usuarioBLL;

        private static FormInicioSesion_234TL instancia;

        public static FormInicioSesion_234TL ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FormInicioSesion_234TL();
            }
            return instancia;
        }

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
        }

        private void IngresarButton_Click(object sender, EventArgs e)
        {
            string Login = textBox1.Text;
            string Password = Encryptador_234TL.SHA256Encrpytar_234TL(textBox2.Text);

            IngresarButton.Enabled = false;
            try
            {
                if (string.IsNullOrEmpty(Login))
                    throw new Exception("Inserte un login");

                if (string.IsNullOrEmpty(Password))
                    throw new Exception("Inserte un password");

                var resultado = usuarioBLL.Login(Login, Password);

                switch (resultado)
                {
                    case Resultados_234TL.UsuarioLogueado:
                        Utilitarios_234TL.MensajeError("Ya hay un usuario logueado");
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioValido:
                        Utilitarios_234TL.MensajeExito("Usuario logueado");
                        textBox1.Clear();
                        textBox2.Clear();

                        if (this.Owner is FormPrincipal_234TL formPrincipal)
                        {
                            Utilitarios_234TL.CambiarUsuarioToolStrip(formPrincipal.toolStripStatusLabel1, usuarioBLL.GetUsuarioLogueado());
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar el usuario logueado");
                        }
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioBloqueado:
                        Utilitarios_234TL.MensajeError("El usuario está bloqueado");
                        textBox1.Clear();
                        textBox2.Clear();
                        this.Close();
                        break;

                    case Resultados_234TL.ContrasenaInvalida:
                        Utilitarios_234TL.MensajeAdvertencia("Contraseña incorrecta");
                        break;

                    case Resultados_234TL.UsuarioNoExiste:
                        Utilitarios_234TL.MensajeError("El usuario no existe");
                        break;

                    case Resultados_234TL.UsuarioInactivo:
                        Utilitarios_234TL.MensajeError("El usuario está inactivo");
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
    }
}