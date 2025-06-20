using Servicios_234TL.Observer_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCambiarIdioma_234TL : Form, IObserver_234TL<Dictionary<string, string>>
    {
        public FormCambiarIdioma_234TL()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
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

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormCambiarIdioma_234TL_Title"];
            EspañolButton.Text = Traduccion["FormCambiarIdioma_234TL_Español"];
            ItalianoButton.Text = Traduccion["FormCambiarIdioma_234TL_Italiano"];
            InglesButton.Text = Traduccion["FormCambiarIdioma_234TL_Ingles"];
            VolverButton.Text = Traduccion["FormCambiarIdioma_234TL_Volver"];
        }

        private void CambiarIdioma(string idioma)
        {
            IdiomasManager_234TL.Instancia.CambiarIdioma(idioma);
            this.Close();
        }

        private void FormCambiarIdioma_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }
    }
}