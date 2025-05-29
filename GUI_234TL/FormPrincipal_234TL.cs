using BLL_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormPrincipal_234TL : Form
    {
        private readonly UsuarioBLL_234TL usuario = new();

        public FormPrincipal_234TL()
        {
            InitializeComponent();
            OcultarBotones();
            this.BackColor = Color.FromArgb(255, 192, 192, 192);
            var Usuariologueado = usuario.GetUsuarioLogueado();
            Utilitarios_234TL.CambiarUsuarioToolStrip(toolStripStatusLabel1, Usuariologueado);
        }

        #region Sesion

        private void SesionButton_Click(object sender, EventArgs e)
        {
            bool activado = IniciarSesionButton.Visible;
            IniciarSesionButton.Visible = !activado;
            CerrarSesionButton.Visible = !activado;
            CambiarContraseñabutton.Visible = !activado;
        }

        private void SesionButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(SesionButton);
        }

        private void SesionButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(SesionButton);
        }

        #endregion Sesion

        #region Iniciar Sesion

        private void IniciarSesionButton_Click(object sender, EventArgs e)
        {
            FormInicioSesion_234TL formInicioSesion = FormInicioSesion_234TL.ObtenerInstancia();
            formInicioSesion.Owner = this;
            formInicioSesion.StartPosition = FormStartPosition.CenterParent;
            formInicioSesion.ShowDialog(this);
        }

        private void IniciarSesionButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(IniciarSesionButton);
        }

        private void IniciarSesionButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(IniciarSesionButton);
        }

        #endregion Iniciar Sesion

        #region Cerrar Sesion

        private void CerrarSesionButton_Click(object sender, EventArgs e)
        {
            var resultado = usuario.Logout();

            switch (resultado)
            {
                case Resultados_234TL.SesionCerrada:
                    Utilitarios_234TL.MensajeInformacion("Sesión cerrada");
                    Utilitarios_234TL.CambiarUsuarioToolStrip(toolStripStatusLabel1, null);
                    break;

                case Resultados_234TL.NoHayLogueado:
                    Utilitarios_234TL.MensajeAdvertencia("No hay usuario logueado");
                    break;

                default:
                    Utilitarios_234TL.MensajeError("Error al cerrar sesión");
                    break;
            }
        }

        private void CerrarSesionButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(CerrarSesionButton);
        }

        private void CerrarSesionButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(CerrarSesionButton);
        }

        #endregion Cerrar Sesion

        #region Perfiles

        private void PerfilesButton_Click(object sender, EventArgs e)
        {
            var formPerfiles = FormUsuarios_234TL.ObtenerInstancia();
            MostrarFormularioInterno(formPerfiles, this);
        }

        private void PerfilesButton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(PerfilesButton);
        }

        private void PerfilesButton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(PerfilesButton);
        }

        #endregion Perfiles

        #region Cambiar Contraseña

        private void CambiarContraseñabutton_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña_234TL formCambiarContraseña = new FormCambiarContraseña_234TL();
            formCambiarContraseña.Owner = this;
            formCambiarContraseña.StartPosition = FormStartPosition.CenterParent;
            formCambiarContraseña.ShowDialog(this);
        }

        private void CambiarContraseñabutton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(CambiarContraseñabutton);
        }

        private void CambiarContraseñabutton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(CambiarContraseñabutton);
        }

        #endregion Cambiar Contraseña

        #region Gestion de Usuarios

        private void GestionUsuariobutton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(GestionUsuariobutton);
        }

        private void GestionUsuariobutton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(GestionUsuariobutton);
        }

        private void GestionUsuariobutton_Click(object sender, EventArgs e)
        {
            bool activado = PerfilesButton.Visible;
            PerfilesButton.Visible = !activado;
        }

        #endregion Gestion de Usuarios

        #region Funciones

        private void OcultarBotones()
        {
            IniciarSesionButton.Visible = false;
            CerrarSesionButton.Visible = false;
            CambiarContraseñabutton.Visible = false;
            PerfilesButton.Visible = false;
        }

        private void MostrarFormularioInterno(Form formHijo, Form formPadre)
        {
            formHijo.TopLevel = false;

            int margenIzquierdo = 220;
            int margenSuperior = 10;
            int margenDerecho = 20;
            int margenInferior = 30;

            int ancho = formPadre.ClientSize.Width - margenIzquierdo - margenDerecho;
            int alto = formPadre.ClientSize.Height - margenSuperior - margenInferior;

            formHijo.Location = new Point(margenIzquierdo, margenSuperior);
            formHijo.Size = new Size(ancho, alto);
            formHijo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            formPadre.Controls.Add(formHijo);
            formHijo.BringToFront();
            formHijo.Show();
        }

        #endregion Funciones
    }
}