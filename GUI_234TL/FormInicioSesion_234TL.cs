using BLL_234TL;
using Microsoft.VisualBasic.Logging;
using Servicios_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormInicioSesion_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
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
            string login = textBox1.Text.Trim();
            string password = textBox2.Text;

            IngresarButton.Enabled = false;
            try
            {
                usuarioBLL.Login(login, password);

                Utilitarios_234TL.MensajeExito("LoginExitoso", login);
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);

                textBox2.Clear();
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoLogin", ex);
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

        public void Update(TraduccionesClase_234TL traduccion)
        {
            try
            {
                var textos = traduccion.Forms.IniciaSesion;

                this.Text = textos.Title;
                label3.Text = textos.Label_Titulo;
                label1.Text = textos.Label_Usuario; 
                label2.Text = textos.Label_Contraseña; 

                label4.Text = textos.Label_EjemploUsuario;
                label5.Text = textos.Label_EjemploContraseña;

                IngresarButton.Text = textos.IngresarBotton;
                VolverButton.Text = traduccion.Comunes.VolverBotton; 

                MostrarButton.Text = textos.CheckBox_MostrarContraseña;
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoLogin", ex);
            }
        }

        private void FormInicioSesion_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}