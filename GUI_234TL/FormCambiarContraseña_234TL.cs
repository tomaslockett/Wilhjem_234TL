using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCambiarContraseña_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
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
                string actual = textBox1.Text;
                string nueva = textBox2.Text;
                string confirmacion = textBox3.Text;

                usuarioBLL.CambiarContraseña(actual, nueva, confirmacion);

                Utilitarios_234TL.MensajeExito("ContraseñaCambiadaConExito");

                if (this.Owner is FormPrincipal_234TL formPrincipal)
                {
                    formPrincipal.ActualizarUsuarioLogueado(null);
                }
                this.Close();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);

                switch (ex.Nombre)
                {
                    case "ContraseñaActual":
                        textBox1.Focus();
                        break;
                    case "ContraseñaNueva":
                        textBox2.Focus();
                        break;
                    case "Confirmacion":
                        textBox3.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoCambiarContraseña", ex);
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

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.CambiarContraseña;

                this.Text = textos.Title;
                label1.Text = textos.Label_Titulo;
                label2.Text = textos.Label_Actual;
                label3.Text = textos.Label_Nueva;
                label4.Text = textos.Label_Confirmar;

                Ingresarbutton.Text = textos.CambiarBotton; 

                MostrarButton.Text = textos.CheckBox_Mostrar;
                MostarButton2.Text = textos.CheckBox_Mostrar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar las traducciones para el formulario de Cambio de Contraseña.");
            }
        }

        private void FormCambiarContraseña_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}