using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormMessageBox_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private string _claveMensaje;
        private MessageBoxButtons _botones;
        private MessageBoxIcon _icono;
        private object[] _args;
        private FormMessageBox_234TL()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
        }
        public FormMessageBox_234TL(string claveMensaje, params object[] args)
        {
            InitializeComponent();
            _claveMensaje = claveMensaje;
            _args = args;
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public static DialogResult Mostrar(Form owner,string claveMensaje, MessageBoxButtons botones, MessageBoxIcon icono, params object[] args)
        {
            using (var form = new FormMessageBox_234TL())
            {
                form.Configurar(claveMensaje, botones, icono, args);
                form.StartPosition = FormStartPosition.CenterParent;
                return form.ShowDialog(owner);
                return form.ShowDialog();
            }
        }
        private void Configurar(string claveMensaje, MessageBoxButtons botones, MessageBoxIcon icono, params object[] args)
        {
            _claveMensaje = claveMensaje;
            _botones = botones;
            _icono = icono;
            _args = args;

            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(TraduccionesClase_234TL traduccion)
        {
            try
            {
                switch (_icono)
                {
                    case MessageBoxIcon.Error: this.Text = traduccion.Comunes.Title_Error; break;
                    case MessageBoxIcon.Warning: this.Text = traduccion.Comunes.Title_Peligro; break;
                    case MessageBoxIcon.Information: this.Text = traduccion.Comunes.Title_Informacion; break;
                    case MessageBoxIcon.Question: this.Text = traduccion.Comunes.Title_Confirmacion; break;
                    default: this.Text = "Mensaje"; break;
                }

                string textoMensaje = ObtenerTextoDeMensaje(traduccion.Messages, _claveMensaje) ?? "Clave no encontrada: " + _claveMensaje;
                Mensaje.Text = string.Format(textoMensaje, _args);

                ConfigurarBotones(traduccion.Comunes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de traducción en MessageBox: " + ex.Message);
            }
        }
        private string ObtenerTextoDeMensaje(TraduccionesMensajes_234TL mensajes, string clave)
        {
            if (mensajes.Exito.ContainsKey(clave)) return mensajes.Exito[clave];
            if (mensajes.Error.ContainsKey(clave)) return mensajes.Error[clave];
            if (mensajes.Confirmacion.ContainsKey(clave)) return mensajes.Confirmacion[clave];
            if (mensajes.Informacion.ContainsKey(clave)) return mensajes.Informacion[clave];
            if (mensajes.Peligro.ContainsKey(clave)) return mensajes.Peligro[clave];
            return null;
        }

        private void ConfigurarBotones(TraduccionesCOMUNES_234TL common)
        {
            Boton1.Visible = Boton2.Visible = Boton3.Visible = false;

            switch (_botones)
            {
                case MessageBoxButtons.OK:
                    Boton2.Text = common.Aceptar;
                    Boton2.DialogResult = DialogResult.OK;
                    Boton2.Visible = true;
                    this.AcceptButton = Boton2;
                    break;
                case MessageBoxButtons.YesNo:
                    Boton1.Text = common.Si;
                    Boton1.DialogResult = DialogResult.Yes;
                    Boton1.Visible = true;
                    Boton3.Text = common.No;
                    Boton3.DialogResult = DialogResult.No;
                    Boton3.Visible = true;
                    this.AcceptButton = Boton1;
                    this.CancelButton = Boton3;
                    break;
            }
        }

        private void Boton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Boton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Boton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMessageBox_234TL_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomasManager_234TL.Instancia.Unsubscribe(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Mensaje_Click(object sender, EventArgs e)
        {

        }
    }
}
