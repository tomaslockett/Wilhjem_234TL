using Servicios_234TL.Observer_234TL;
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
    public partial class FormMessageBox_234TL : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private string _claveTitulo;
        private string _claveMensaje;
        private MessageBoxButtons _botones;
        private readonly object[] _args;
        public FormMessageBox_234TL()
        {
            InitializeComponent();
            IdiomasManager_234TL.Instancia.Subscribe(this);
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

        public static DialogResult Mostrar(string claveMensaje, string claveTitulo, MessageBoxButtons botones)
        {
            using (var form = new FormMessageBox_234TL())
            {
                form.Configurar(claveMensaje, claveTitulo, botones);
                return form.ShowDialog();
            }
        }
        private void Configurar(string claveMensaje, string claveTitulo, MessageBoxButtons botones)
        {
            _claveMensaje = claveMensaje;
            _claveTitulo = claveTitulo;
            _botones = botones;

            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            try
            {
                this.Text = Traduccion.ContainsKey(_claveTitulo) ? Traduccion[_claveTitulo] : "Confirmación";
                Mensaje.Text = Traduccion.ContainsKey(_claveMensaje) ? Traduccion[_claveMensaje] : "Mensaje no encontrado.";

                switch (_botones)
                {
                    case MessageBoxButtons.OK:
                        Boton1.Text = Traduccion["Boton_Aceptar"];
                        Boton1.DialogResult = DialogResult.OK;
                        Boton1.Visible = true;
                        Boton2.Visible = false; 
                        Boton3.Visible = false;
                        this.AcceptButton = Boton1; 
                        break;

                    case MessageBoxButtons.OKCancel:
                        Boton1.Text = Traduccion["Boton_Aceptar"];
                        Boton1.DialogResult = DialogResult.OK;
                        Boton1.Visible = true;
                        Boton2.Visible = false;
                        Boton3.Text = Traduccion["Boton_Cancelar"];
                        Boton3.DialogResult = DialogResult.Cancel;
                        Boton3.Visible = true;
                        this.AcceptButton = Boton1;
                        this.CancelButton = Boton3; 
                        break;

                    case MessageBoxButtons.YesNo:
                        Boton1.Text = Traduccion["Boton_Si"];
                        Boton1.DialogResult = DialogResult.Yes;
                        Boton1.Visible = true;
                        Boton2.Visible = false;
                        Boton3.Text = Traduccion["Boton_No"];
                        Boton3.DialogResult = DialogResult.No;
                        Boton3.Visible = true;
                        this.AcceptButton = Boton1;
                        this.CancelButton = Boton3;
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        Boton1.Text = Traduccion["Boton_Si"];
                        Boton1.DialogResult = DialogResult.Yes;
                        Boton1.Visible = true;
                        Boton2.Text = Traduccion["Boton_No"]; 
                        Boton2.DialogResult = DialogResult.No;
                        Boton2.Visible = true;
                        Boton3.Text = Traduccion["Boton_Cancelar"];
                        Boton3.DialogResult = DialogResult.Cancel;
                        Boton3.Visible = true;
                        this.AcceptButton = Boton1;
                        this.CancelButton = Boton3;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de traducción en MessageBox: " + ex.Message);
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
    }
}
