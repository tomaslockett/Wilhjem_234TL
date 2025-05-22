using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Singleton_234TL;

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
                        MessageBox.Show("Ya hay un usuario logueado");
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioValido:
                        MessageBox.Show("Login exitoso");
                        this.Close();
                        break;

                    case Resultados_234TL.UsuarioBloqueado:
                        MessageBox.Show("El usuario está bloqueado");
                        this.Close();
                        break;

                    case Resultados_234TL.ContrasenaInvalida:
                        MessageBox.Show("Contraseña incorrecta");
                        break;

                    case Resultados_234TL.UsuarioNoExiste:
                        MessageBox.Show("El usuario no existe");
                        break;

                    case Resultados_234TL.UsuarioInactivo:
                        MessageBox.Show("El usuario está inactivo");
                        this.Close();
                        break;
                }
            }
            catch (Exepcion_234TL ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}