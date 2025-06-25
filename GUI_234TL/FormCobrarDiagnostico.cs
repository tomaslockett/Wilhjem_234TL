using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Observer_234TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCobrarDiagnostico : Form, IObserver_234TL<Dictionary<string, string>>
    {
        ReparacionDTOBLL_234TL ReparacionDTObll = new();
        public event EventHandler Cobrado;
        public FormCobrarDiagnostico()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            ConfigurarColumnasOrdenes();
            ConfigurarDataGrids();            
            dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormCobrarDiagnostico_Titulo"];
            NumeroTarjetaLabel.Text = Traduccion["FormCobrarDiagnostico_NumeroTarjetaLabel"];
            CodigoSeguridadLabel.Text = Traduccion["FormCobrarDiagnostico_CodigoSeguridadLabel"];
            VencimientoLabel.Text = Traduccion["FormCobrarDiagnostico_VencimientoLabel"];
            CobrarButton.Text = Traduccion["FormCobrarDiagnostico_CobrarButton"];

            dataGridViewReparaciones.Columns["Columna_NumeroReparacion"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_NumeroReparacion"];
            dataGridViewReparaciones.Columns["Columna_Estado"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_Estado"];
            dataGridViewReparaciones.Columns["Columna_NumeroSerie"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_NumeroSerie"];
            dataGridViewReparaciones.Columns["Columna_NombreCliente"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_NombreCliente"];
            dataGridViewReparaciones.Columns["Columna_DNICliente"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_DNICliente"];
            dataGridViewReparaciones.Columns["Columna_NombreTecnico"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_NombreTecnico"];
            dataGridViewReparaciones.Columns["Columna_EspecialidadTecnico"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_EspecialidadTecnico"];
            dataGridViewReparaciones.Columns["Columna_Cobrado"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_Cobrado"];
            dataGridViewReparaciones.Columns["Columna_FacturaGenerada"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_FacturaGenerada"];
            dataGridViewReparaciones.Columns["Columna_ComprobanteGenerado"].HeaderText =Traduccion["FormCobrarDiagnostico_Columna_ComprobanteGenerado"];
        }

        private void FormCobrarDiagnostico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void CobrarButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewReparaciones.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionarReparacion");
                return;
            }

            var fila = dataGridViewReparaciones.SelectedRows[0];
            if (fila.DataBoundItem is not ReparacionDTO_234TL reparacionDTO)
            {
                Utilitarios_234TL.MensajeError("Error_ReparacionInvalida");
                return;
            }

            string tarjeta = NumeroTarjetaTextBox.Text.Trim();
            string codigo = CodigoSeguridadTextBox.Text.Trim();
            string vencimiento = VencimientoTextBox.Text.Trim();

            if (!Regex.IsMatch(tarjeta, @"^\d{16}$"))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_TarjetaInvalida");
                NumeroTarjetaTextBox.Focus();
                return;
            }

            if (!Regex.IsMatch(codigo, @"^\d{3,4}$"))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_CodigoInvalido");
                CodigoSeguridadTextBox.Focus();
                return;
            }

            if (!Regex.IsMatch(vencimiento, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_VencimientoInvalido");
                VencimientoTextBox.Focus();
                return;
            }

            if (reparacionDTO.Cobrado)
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_YaCobrado");
                return;
            }

            try
            {
                ReparacionBLL_234TL reparacionBLL = new();
                reparacionBLL.CobrarDiagnostico(Convert.ToInt32(reparacionDTO.NumeroReparacion));

                Utilitarios_234TL.MensajeExito("Exito_DiagnosticoCobrado");

                Cobrado?.Invoke(this, EventArgs.Empty);
                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
                this.Close();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Error_CobrarDiagnostico", ex);
            }
        }
        private void ConfigurarColumnasOrdenes()
        {
            dataGridViewReparaciones.AutoGenerateColumns = false;
            dataGridViewReparaciones.Columns.Clear();

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroReparacion", HeaderText = "Número", DataPropertyName = "NumeroReparacion" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", HeaderText = "Estado", DataPropertyName = "Estado" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", HeaderText = "N° Serie", DataPropertyName = "NumeroSerie" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreCliente", HeaderText = "Cliente", DataPropertyName = "NombreCliente" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNICliente", HeaderText = "DNI", DataPropertyName = "DNICliente" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreTecnico", HeaderText = "Técnico", DataPropertyName = "NombreTecnico" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_EspecialidadTecnico", HeaderText = "Especialidad", DataPropertyName = "EspecialidadTecnico" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Cobrado", HeaderText = "Cobrado", DataPropertyName = "Cobrado" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_FacturaGenerada", HeaderText = "Factura Generada", DataPropertyName = "FacturaGenerada" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_ComprobanteGenerado", HeaderText = "Comprobante Generado", DataPropertyName = "ComprobanteGenerado" });
        }

        private void ConfigurarDataGrids()
        {
            dataGridViewReparaciones.ReadOnly = true;
            dataGridViewReparaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewReparaciones.MultiSelect = false;
            dataGridViewReparaciones.AllowUserToAddRows = false;
            dataGridViewReparaciones.AllowUserToDeleteRows = false;
            dataGridViewReparaciones.AllowUserToOrderColumns = false;
            dataGridViewReparaciones.AllowUserToResizeRows = false;
            dataGridViewReparaciones.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
