using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormRegistrarCliente : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private readonly ClienteBLL_234TL bll = new ClienteBLL_234TL();
        public event EventHandler ClienteAgregadoOEliminado;
        public FormRegistrarCliente()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            CargarClientes();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.RegistroCliente;
                if (textos == null)
                {
                    MessageBox.Show("Exploto");
                    return;
                }

                this.Text = textos.Title;
                ClientesLabel.Text = textos.Label_Titulo;

                DNILabel.Text = textos.Label_DNI;
                NombreLabel.Text = textos.Label_Nombre;
                ApellidoLabel.Text = textos.Label_Apellido;
                NumeroTelefonicoLabel.Text = textos.Label_Telefono;

                RegistrarClientebutton.Text = textos.RegistrarBotton;
                EliminarClienteButton.Text = textos.EliminarBotton;
                ConfigurarColumnas(textos);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
        }

        private void FormRegistrarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void RegistrarClientebutton_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = new Cliente_234TL(
                    dni: DNITextBox.Text.Trim(),
                    nombre: NombreTextBox.Text.Trim(),
                    apellido: ApellidoTextBox.Text.Trim(),
                    telefono: TelefonoTextBox.Text.Trim()
                );

                bll.RegistrarNuevoCliente(cliente);

                Utilitarios_234TL.MensajeExito("ClienteRegistradoExito");
                LimpiarCampos();
                CargarClientes();
                ClienteAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);

                switch (ex.Nombre)
                {
                    case "DNI":
                        DNITextBox.Focus();
                        break;
                    case "Nombre":
                        NombreTextBox.Focus();
                        break;
                    case "Apellido":
                        ApellidoTextBox.Focus();
                        break;
                    case "Telefono":
                        TelefonoTextBox.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoRegistroCliente", ex);
            }
        }

        private void LimpiarCampos()
        {
            DNITextBox.Clear();
            NombreTextBox.Clear();
            ApellidoTextBox.Clear();
            TelefonoTextBox.Clear();
        }

        private void CargarClientes()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = bll.GetAll();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorCargarClientes", ex);
            }
        }
        private void ConfigurarColumnas(FormRegistroCliente_234TL textos)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI",
                DataPropertyName = "Dni",
                HeaderText = textos.Columna_DNI
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = textos.Columna_Nombre
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                DataPropertyName = "Apellido",
                HeaderText = textos.Columna_Apellido
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                DataPropertyName = "Telefono",
                HeaderText = textos.Columna_Telefono
            });
        }


        private void EliminarClienteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("AdvertenciaSeleccionarCliente");
                return;
            }

            var cliente = (Cliente_234TL)dataGridView1.SelectedRows[0].DataBoundItem;

            if (!bll.EliminarCliente(cliente.Dni))
            {
                Utilitarios_234TL.MensajeAdvertencia("AdvertenciaClienteNoExiste");
                return;
            }

            Utilitarios_234TL.MensajeExito("ExitoClienteEliminado");
            CargarClientes();
            LimpiarCampos();
            ClienteAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void FormRegistrarCliente_Load(object sender, EventArgs e)
        {
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }
    }
}