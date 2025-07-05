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
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormFacturaYComprobante : Form, IObserver_234TL<Dictionary<string, string>>
    {
        ReparacionDTOBLL_234TL ReparacionDTObll = new();

        public FormFacturaYComprobante()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            ConfigurarColumnasOrdenes();
            ConfigurarDataGrids();
            dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormFacturaYComprobante_Titulo"];
            CobrarDiagnosticoButton.Text = Traduccion["FormFacturaYComprobante_BotonCobrarDiagnostico"];
            CrearFacturaButton.Text = Traduccion["FormFacturaYComprobante_BotonCrearFactura"];
            CrearComprobanteButton.Text = Traduccion["FormFacturaYComprobante_BotonCrearComprobante"];


            dataGridViewReparaciones.Columns["Columna_NumeroReparacion"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_NumeroReparacion"];
            dataGridViewReparaciones.Columns["Columna_Estado"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_Estado"];
            dataGridViewReparaciones.Columns["Columna_NumeroSerie"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_NumeroSerie"];
            dataGridViewReparaciones.Columns["Columna_NombreCliente"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_NombreCliente"];
            dataGridViewReparaciones.Columns["Columna_DNICliente"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_DNICliente"];
            dataGridViewReparaciones.Columns["Columna_NombreTecnico"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_NombreTecnico"];
            dataGridViewReparaciones.Columns["Columna_EspecialidadTecnico"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_EspecialidadTecnico"];
            dataGridViewReparaciones.Columns["Columna_Cobrado"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_Cobrado"];
            dataGridViewReparaciones.Columns["Columna_FacturaGenerada"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_FacturaGenerada"];
            dataGridViewReparaciones.Columns["Columna_ComprobanteGenerado"].HeaderText =Traduccion["FormFacturaYComprobante_Columna_ComprobanteGenerado"];
        }

        private void FormFacturaYComprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void CobrarDiagnosticoButton_Click(object sender, EventArgs e)
        {
            FormCobrarDiagnostico formCobrarDiagnostico = new();
            formCobrarDiagnostico.Owner = this;
            formCobrarDiagnostico.StartPosition = FormStartPosition.CenterParent;
            formCobrarDiagnostico.Cobrado += (s, args) =>
            {
                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            };
            formCobrarDiagnostico.ShowDialog(this);
        }

        private void CrearFacturaButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewReparaciones.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("Mensaje_SeleccionReparacion");
                return;
            }

            var filaSeleccionada = dataGridViewReparaciones.SelectedRows[0];
            var reparacionDTO = (ReparacionDTO_234TL)filaSeleccionada.DataBoundItem;

            try
            {
                int numeroReparacion = int.Parse(reparacionDTO.NumeroReparacion);
                decimal total = 10000m; 

                var facturaBLL = new FacturaBLL_234TL();
                var factura = facturaBLL.CrearFacturaParaReparacion(numeroReparacion, total);

                Utilitarios_234TL.MensajeExito("Exito_FacturaGenerada", factura.NumeroFactura, numeroReparacion);

                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            }
            catch (InvalidOperationException ex)
            {
                Utilitarios_234TL.MensajeAdvertencia(ex.Message);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Mensaje_ErrorGenerarFactura", ex);
            }

        }

        private void CrearComprobanteButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewReparaciones.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("Mensaje_SeleccionReparacion");
                return;
            }

            var filaSeleccionada = dataGridViewReparaciones.SelectedRows[0];
            var reparacionDTO = (ReparacionDTO_234TL)filaSeleccionada.DataBoundItem;

            try
            {
                int numeroReparacion = int.Parse(reparacionDTO.NumeroReparacion);
                ComprobanteIngresoBLL_234TL comprobanteBLL = new();

                var comprobante = comprobanteBLL.CrearComprobanteParaReparacion(numeroReparacion);

                Utilitarios_234TL.MensajeExito("Exito_ComprobanteGenerado", comprobante.NumeroIngreso, numeroReparacion);

                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            }
            catch (InvalidOperationException ex)
            {
                Utilitarios_234TL.MensajeAdvertencia(ex.Message);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Mensaje_ErrorGenerarComprobante", ex);
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
