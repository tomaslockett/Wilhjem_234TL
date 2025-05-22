using BLL_234TL;
using Servicios_234TL.Singleton_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormPrincipal_234TL : Form
    {
        public FormPrincipal_234TL()
        {
            InitializeComponent();
            var Sesion = new Sesion_234TL();
            OcultarBotones();
            this.BackColor = Color.FromArgb(255, 192, 192, 192);
            var Usuariologueado = new UsuarioBLL_234TL().GetUsuarioLogueado();
            if (Usuariologueado != null)
            {
                toolStripStatusLabel1.Text = "Usuario: " + Usuariologueado.Nombre;
            }
            else
            {
                toolStripStatusLabel1.Text = "Usuario: No hay usuario logueado";
            }
        }

        //Sesion
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

        //Iniciar Sesion
        private void IniciarSesionButton_Click(object sender, EventArgs e)
        {
            FormInicioSesion_234TL formInicioSesion = FormInicioSesion_234TL.ObtenerInstancia();
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

        //Cerrar Sesion
        private void CerrarSesionButton_Click(object sender, EventArgs e)
        {
            var usuario = new UsuarioBLL_234TL();
            var resultado = usuario.Logout();

            switch (resultado)
            {
                case Resultados_234TL.SesionCerrada:
                    MessageBox.Show("Usuario deslogueado");
                    break;

                case Resultados_234TL.NoHayLogueado:
                    MessageBox.Show("No hay usuario logueado");
                    break;

                default:
                    MessageBox.Show("Error al cerrar sesión");
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

        //Perfiles
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

        // Cambiar Contraseña
        private void CambiarContraseñabutton_Click(object sender, EventArgs e)
        {
            FormCambiarContraseña_234TL formCambiarContraseña = new FormCambiarContraseña_234TL();
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

        //Gestion Usuario

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

        //Funciones
        private void IniciarBotones()
        {
            IniciarSesionButton.Visible = false;
            CerrarSesionButton.Visible = false;
        }

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
            //formHijo.FormBorderStyle = FormBorderStyle.None;

            // Márgenes personalizados
            int margenIzquierdo = 220;
            int margenSuperior = 10;
            int margenDerecho = 20;
            int margenInferior = 30;

            // Cálculo del tamaño y posición
            int ancho = formPadre.ClientSize.Width - margenIzquierdo - margenDerecho;
            int alto = formPadre.ClientSize.Height - margenSuperior - margenInferior;

            formHijo.Location = new Point(margenIzquierdo, margenSuperior);
            formHijo.Size = new Size(ancho, alto);
            formHijo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Mostrar dentro del formulario padre
            formPadre.Controls.Add(formHijo);
            formHijo.BringToFront();
            formHijo.Show();
        }
    }
}