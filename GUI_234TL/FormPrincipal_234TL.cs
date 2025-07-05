using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormPrincipal_234TL : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private readonly UsuarioBLL_234TL usuario = new();
        private Usuario_234TL UsuarioLogueado;

        public FormPrincipal_234TL()
        {
            InitializeComponent();
            OcultarBotones();
            this.BackColor = Color.FromArgb(255, 192, 192, 192);
            UsuarioLogueado = usuario.GetUsuarioLogueado();
            ActualizarUsuarioLogueado(null);
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.CambiarIdioma("es");
        }


        private void AplicarPermisos()
        {
            var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();
            bool logueado = sesion.IsLoggedIn_234TL();

            IniciarSesionButton.Visible = !logueado;
            CerrarSesionButton.Visible = logueado;
            CambiarContraseñabutton.Visible = logueado;

            GestionUsuarioButton.Visible = sesion.TienePermiso("ACCESO_USUARIOS");
            PerfilesButton.Visible = sesion.TienePermiso("ACCESO_PERFILES");
            BackupButton.Visible = sesion.TienePermiso("ACCESO_BACKUP");
            RestoreButton.Visible = sesion.TienePermiso("ACCESO_RESTORE");
            BitacoraEButton.Visible = sesion.TienePermiso("ACCESO_BITACORA");

            GestionAdminbutton.Visible = PerfilesButton.Visible || BackupButton.Visible || RestoreButton.Visible || BitacoraEButton.Visible || GestionUsuarioButton.Visible;

            RecepcionButton.Visible = sesion.TienePermiso("ACCESO_RECEPCION");
            CrearOrdenButton.Visible = sesion.TienePermiso("ACCESO_CREAR_ORDEN");
            GeneralFacturaYComprobanteButton.Visible = sesion.TienePermiso("ACCESO_FACTURAR");
        }


        #region FormPrincipal

        private void FormPrincipal_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #endregion FormPrincipal

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
            FormInicioSesion_234TL formInicioSesion = new FormInicioSesion_234TL();
            formInicioSesion.Owner = this;
            formInicioSesion.StartPosition = FormStartPosition.CenterParent;
            DialogResult resultado = formInicioSesion.ShowDialog(this);
            if (resultado == DialogResult.OK)
            {
                ActualizarUsuarioLogueado(usuario.GetUsuarioLogueado());
            }
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
                    Utilitarios_234TL.MensajeInformacion("Mensaje_SesionCerrada");
                    ActualizarUsuarioLogueado(null);
                    IdiomasManager_234TL.Instancia.NotificarActuales();
                    break;

                case Resultados_234TL.NoHayLogueado:
                    Utilitarios_234TL.MensajeAdvertencia("Mensaje_NoHayLogueado");
                    break;

                default:
                    Utilitarios_234TL.MensajeError("Mensaje_ErrorCerrarSesion");
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

        #region Gestion Usuario

        private void GestionUsuarioButton_Click(object sender, EventArgs e)
        {
            var formPerfiles = new FormUsuarios_234TL();
            MostrarFormularioInterno(formPerfiles, this);
        }

        private void GestionUsuario_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(GestionUsuarioButton);
        }

        private void GestionUsuario_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(GestionUsuarioButton);
        }

        #endregion Gestion Usuario

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

        #region Gestion de Admin

        private void GestionAdminbutton_MouseHover(object sender, EventArgs e)
        {
            Utilitarios_234TL.Agrandar(GestionAdminbutton);
        }

        private void GestionAdminbutton_MouseLeave(object sender, EventArgs e)
        {
            Utilitarios_234TL.TamañoOriginal(GestionAdminbutton);
        }

        private void GestionAdminbutton_Click(object sender, EventArgs e)
        {
            bool activado = GestionUsuarioButton.Visible;
            GestionUsuarioButton.Visible = !activado;
        }

        #endregion Gestion de Admin

        #region Perfiles
        private void PerfilesButton_Click(object sender, EventArgs e)
        {
            FormPerfiles_234TL formPerfiles = new FormPerfiles_234TL();
            formPerfiles.Owner = this;
            formPerfiles.StartPosition = FormStartPosition.CenterParent;
            formPerfiles.ShowDialog(this);
        }
        #endregion

        #region Funciones

        private void OcultarBotones()
        {
            IniciarSesionButton.Visible = false;
            CerrarSesionButton.Visible = false;
            CambiarContraseñabutton.Visible = false;
            GestionUsuarioButton.Visible = false;
            CrearOrdenButton.Visible = false;
            GeneralFacturaYComprobanteButton.Visible = false;
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

        public void ActualizarUsuarioLogueado(Usuario_234TL usuarioLogueado)
        {
            this.UsuarioLogueado = usuarioLogueado;
            AplicarPermisos();
        }

        #endregion Funciones

        #region IObserver_234TL

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormPrincipal_234TL_Title"];
            SesionButton.Text = Traduccion["FormPrincipal_234TL_SesionButton"];
            IniciarSesionButton.Text = Traduccion["FormPrincipal_234TL_IniciarSesionButton"];
            CerrarSesionButton.Text = Traduccion["FormPrincipal_234TL_CerrarSesionButton"];
            CambiarContraseñabutton.Text = Traduccion["FormPrincipal_234TL_CambiarContraseñaButton"];
            GestionUsuarioButton.Text = Traduccion["FormPrincipal_234TL_GestionUsuariosButton"];
            GestionAdminbutton.Text = Traduccion["FormPrincipal_234TL_GestionAdminButton"];
            toolStripStatusLabel1.Text = Traduccion["FormPrincipal_234TL_ToolStripStatusLabel"];
            PerfilesButton.Text = Traduccion["FormPrincipal_234TL_PerfilesButton"];
            BackupButton.Text = Traduccion["FormPrincipal_234TL_BackupButton"];
            RestoreButton.Text = Traduccion["FormPrincipal_234TL_RestoreButton"];
            BitacoraEButton.Text = Traduccion["FormPrincipal_234TL_BitacoraEButton"];
            DigVerButton.Text = Traduccion["FormPrincipal_234TL_DigVerButton"];
            CambiarIdiomaButton.Text = Traduccion["FormPrincipal_234TL_CambiarIdiomaButton"];
            CrearOrdenButton.Text = Traduccion["FormPrincipal_234TL_CrearOrdenButton"];
            GeneralFacturaYComprobanteButton.Text = Traduccion["FormPrincipal_234TL_GeneralFacturaYComprobanteButton"];
            RecepcionButton.Text = Traduccion["FormPrincipal_234TL_RecepcionButton"];
            if (UsuarioLogueado != null)
            {
                toolStripStatusLabel1.Text = string.Format(Traduccion.GetValueOrDefault("FormPrincipal_234TL_ToolStripBienvenida", "¡Bienvenido/a, {0}! ¡Que tengas una excelente jornada!"), UsuarioLogueado.Nombre);
            }
            else
            {
                toolStripStatusLabel1.Text = Traduccion.GetValueOrDefault("FormPrincipal_234TL_ToolStripSinUsuario", "Usuario: No hay usuario logueado");
            }
        }

        #endregion IObserver_234TL



        private void CambiarIdiomaButton_Click(object sender, EventArgs e)
        {
            FormCambiarIdioma_234TL formCambiarIdioma = new();
            formCambiarIdioma.Owner = this;
            formCambiarIdioma.StartPosition = FormStartPosition.CenterParent;
            formCambiarIdioma.ShowDialog(this);
        }

        private void CrearOrdenButton_Click(object sender, EventArgs e)
        {
            FormOrdenIngreso formOrdenIngreso = new();
            formOrdenIngreso.Owner = this;
            formOrdenIngreso.StartPosition = FormStartPosition.CenterParent;
            formOrdenIngreso.ShowDialog(this);
        }

        private void GeneralFacturaYComprobanteButton_Click(object sender, EventArgs e)
        {
            FormFacturaYComprobante FormFacturaYComprobante = new();
            FormFacturaYComprobante.Owner = this;
            FormFacturaYComprobante.StartPosition = FormStartPosition.CenterParent;
            FormFacturaYComprobante.ShowDialog(this);
        }

        private void RecepcionButton_Click(object sender, EventArgs e)
        {
            bool activado = CrearOrdenButton.Visible;
            CrearOrdenButton.Visible = !activado;
            GeneralFacturaYComprobanteButton.Visible = !activado;
        }
    }
}