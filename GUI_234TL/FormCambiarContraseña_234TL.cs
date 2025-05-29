using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCambiarContraseña_234TL : Form
    {
        private static FormCambiarContraseña_234TL instancia;

        public static FormCambiarContraseña_234TL ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FormCambiarContraseña_234TL();
            }
            return instancia;
        }

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
        }

        private readonly UsuarioBLL_234TL usuarioBLL = new();

        private void Ingresarbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string actual = textBox1.Text.Trim();
                string nueva = textBox2.Text.Trim();
                string confirmacion = textBox3.Text.Trim();

                if (!SingletonSesion.GetInstance().IsLoggedIn_234TL())
                {
                    Utilitarios_234TL.MensajeError("No hay un usuario logueado. No se puede cambiar la contraseña.");
                    this.Close();
                    return;
                }
                var usuario = usuarioBLL.GetUsuarioLogueado();
                if (usuario == null)
                {
                    Utilitarios_234TL.MensajeError("No hay un usuario logueado. No se puede cambiar la contraseña.");
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Por favor, completá todos los campos.");
                    return;
                }

                if (nueva != confirmacion)
                {
                    Utilitarios_234TL.MensajeError("La nueva contraseña no coincide con la confirmación.");
                    return;
                }

                string Actualpassword = Encryptador_234TL.SHA256Encrpytar_234TL(actual);

                if (usuario.Password != Actualpassword)
                {
                    Utilitarios_234TL.MensajeError("La contraseña actual es incorrecta");
                    return;
                }
                if (Encryptador_234TL.SHA256Encrpytar_234TL(nueva) == usuario.Password)
                {
                    Utilitarios_234TL.MensajeError("La nueva contraseña no puede ser igual a la actual");
                    return;
                }
                string nuevaHash = Encryptador_234TL.SHA256Encrpytar_234TL(nueva);
                usuario.Password = nuevaHash;
                usuarioBLL.Update(usuario);
                usuarioBLL.Logout();
                if (this.Owner is FormPrincipal_234TL formPrincipal)
                {
                    Utilitarios_234TL.CambiarUsuarioToolStrip(formPrincipal.toolStripStatusLabel1, usuarioBLL.GetUsuarioLogueado());
                }
                Utilitarios_234TL.MensajeExito("Contraseña cambiada correctamente, va a cerrarse la sesion");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Ocurrió un error inesperado:\n" + ex.Message);
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
    }
}