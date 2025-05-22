using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Singleton_234TL;

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

        private readonly UsuarioBLL_234TL usuarioBLL = new UsuarioBLL_234TL();

        private void Ingresarbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string actual = textBox1.Text.Trim();
                string nueva = textBox2.Text.Trim();
                string confirmacion = textBox3.Text.Trim();

                var sesion = SingletonSesion.GetInstance();
                if (!sesion.IsLoggedIn_234TL())
                {
                    MessageBox.Show("No hay un usuario logueado. No se puede cambiar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                var usuario = usuarioBLL.GetUsuarioLogueado();
                if (usuario == null)
                {
                    MessageBox.Show("No hay un usuario logueado. No se puede cambiar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmacion))
                {
                    MessageBox.Show("Por favor, completá todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nueva != confirmacion)
                {
                    MessageBox.Show("La nueva contraseña no coincide con la confirmación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string Actualpassword = Encryptador_234TL.SHA256Encrpytar_234TL(actual);

                if (usuario.Password != Actualpassword)
                {
                    MessageBox.Show("La contraseña actual es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string nuevaHash = Encryptador_234TL.SHA256Encrpytar_234TL(nueva);
                usuario.Password = nuevaHash;
                usuarioBLL.Update(usuario);
                MessageBox.Show("Contraseña cambiada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}