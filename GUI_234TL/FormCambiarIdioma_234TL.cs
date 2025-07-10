using BLL_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCambiarIdioma_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private readonly UsuarioBLL_234TL usuarioBLL;
        public FormCambiarIdioma_234TL()
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL_234TL();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
            ItalianoButton.Visible = false; 
        }

        #region Click

        private void EspañolButton_Click(object sender, EventArgs e)
        {
            CambiarIdioma("es");
        }

        private void ItalianoButton_Click(object sender, EventArgs e)
        {
            CambiarIdioma("it");
        }

        private void InglesButton_Click(object sender, EventArgs e)
        {
            CambiarIdioma("en");
        }

        private void VolverButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Click

        #region MouseHover

        private void EspañolButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(EspañolButton);
        }

        private void EspañolButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(EspañolButton);
        }

        private void ItalianoButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(ItalianoButton);
        }

        #endregion MouseHover

        #region MouseLeave

        private void ItalianoButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(ItalianoButton);
        }

        private void InglesButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(InglesButton);
        }

        private void InglesButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(InglesButton);
        }

        #endregion MouseLeave

        public void Update(TraduccionesClase_234TL traduccion)
        {
            try
            {
                var textos = traduccion.Forms.CambiarIdioma;

                this.Text = textos.Title;
                EspañolButton.Text = textos.Boton_Español;
                ItalianoButton.Text = textos.Boton_Italiano;
                InglesButton.Text = textos.Boton_Ingles;

                VolverButton.Text = traduccion.Comunes.VolverBotton;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar las traducciones para el formulario de Cambio de Idioma.");
            }
        }

        private void CambiarIdioma(string idioma)
        {
            try
            {
                var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();

                if (sesion.IsLoggedIn_234TL())
                {
                    var usuarioActual = sesion.Usuario;
                    usuarioBLL.CambiarIdiomaUsuario(usuarioActual, idioma);
                }
                else
                {
                    IdiomasManager_234TL.Instancia.CambiarIdioma(idioma);
                }
                Utilitarios_234TL.MensajeExito("IdiomaCambiadoExito");
                this.Close();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoCambiarIdioma", ex);
            }
        }

        private void FormCambiarIdioma_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}