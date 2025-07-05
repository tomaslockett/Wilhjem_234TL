using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Observer_234TL;
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
    public partial class FormRegistrarCliente : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private readonly ClienteBLL_234TL bll = new ClienteBLL_234TL();
        public event EventHandler ClienteAgregadoOEliminado;
        public FormRegistrarCliente()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            CargarClientes();
            ConfigurarColumnas();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            try
            {
                this.Text = Traduccion["FormRegistrarCliente_Titulo"];
                ClientesLabel.Text = Traduccion["FormRegistrarCliente_ClientesLabel"];
                DNILabel.Text = Traduccion["FormRegistrarCliente_DNILabel"];
                NombreLabel.Text = Traduccion["FormRegistrarCliente_NombreLabel"];
                ApellidoLabel.Text = Traduccion["FormRegistrarCliente_ApellidoLabel"];
                NumeroTelefonicoLabel.Text = Traduccion["FormRegistrarCliente_TelefonoLabel"];
                RegistrarClientebutton.Text = Traduccion["FormRegistrarCliente_RegistrarButton"];
                EliminarClienteButton.Text = Traduccion["FormRegistrarCliente_EliminarButton"];

                if (dataGridView1.Columns.Count >= 4)
                {
                    dataGridView1.Columns[0].HeaderText = Traduccion["ColumnaDNI"];
                    dataGridView1.Columns[1].HeaderText = Traduccion["ColumnaNombre"];
                    dataGridView1.Columns[2].HeaderText = Traduccion["ColumnaApellido"];
                    dataGridView1.Columns[3].HeaderText = Traduccion["ColumnaTelefono"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en traducción: {ex.Message}");
            }
        }

        private void FormRegistrarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void RegistrarClientebutton_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var cliente = new Cliente_234TL(
                dni: DNITextBox.Text.Trim(),
                nombre: NombreTextBox.Text.Trim(),
                apellido: ApellidoTextBox.Text.Trim(),
                telefono: TelefonoTextBox.Text.Trim()
            );

            if (!bll.RegistrarCliente(cliente))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_ClienteExistente");
                return;
            }

            Utilitarios_234TL.MensajeExito("Exito_ClienteRegistrado");
            LimpiarCampos();
            CargarClientes();
            ClienteAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
        }

        private void LimpiarCampos()
        {
            DNITextBox.Clear();
            NombreTextBox.Clear();
            ApellidoTextBox.Clear();
            TelefonoTextBox.Clear();
        }
        private bool ValidarCampos()
        {
            var regexDni = new System.Text.RegularExpressions.Regex(@"^\d{7,8}$");
            var regexNombreApellido = new System.Text.RegularExpressions.Regex(@"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$");
            var regexTelefono = new System.Text.RegularExpressions.Regex(@"^\d{6,15}$");

            string dni = DNITextBox.Text.Trim();

            if (!regexDni.IsMatch(DNITextBox.Text))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_DNI_Invalido");
                DNITextBox.Focus();
                return false;
            }

            if (!regexNombreApellido.IsMatch(NombreTextBox.Text))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_Nombre_Invalido");
                NombreTextBox.Focus();
                return false;
            }

            if (dni == "00000000" || dni == "0000000")
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_DNI_TodoCeros");
                DNITextBox.Focus();
                return false;
            }

            if (!regexNombreApellido.IsMatch(ApellidoTextBox.Text))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_Apellido_Invalido");
                ApellidoTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text) || !regexTelefono.IsMatch(TelefonoTextBox.Text))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_Telefono_Invalido");
                TelefonoTextBox.Focus();
                return false;
            }

            return true;
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
                Utilitarios_234TL.MensajeError("Error_CargarClientes", ex);
            }
        }
        private void ConfigurarColumnas()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "DNI", DataPropertyName = "Dni" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Apellido", DataPropertyName = "Apellido" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Teléfono", DataPropertyName = "Telefono" });
        }


        private void EliminarClienteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionarCliente");
                return;
            }

            var cliente = (Cliente_234TL)dataGridView1.SelectedRows[0].DataBoundItem;

            if (!bll.EliminarCliente(cliente.Dni))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_ClienteNoExiste");
                return;
            }

            Utilitarios_234TL.MensajeExito("Exito_ClienteEliminado");
            CargarClientes();
            LimpiarCampos();
            ClienteAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}