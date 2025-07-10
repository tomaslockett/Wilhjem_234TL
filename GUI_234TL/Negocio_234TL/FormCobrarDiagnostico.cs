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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormCobrarDiagnostico : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        ReparacionDTOBLL_234TL ReparacionDTObll = new();
        ReparacionBLL_234TL Reparacionbll = new();
        public event EventHandler Cobrado;
        public FormCobrarDiagnostico()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            ConfigurarDataGrids();            
            dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.CobrarDiagnostico;
                this.Text = textos.Title;

                NumeroTarjetaLabel.Text = textos.Label_NumeroTarjeta;
                CodigoSeguridadLabel.Text = textos.Label_CodigoSeguridad;
                VencimientoLabel.Text = textos.Label_Vencimiento;
                CobrarButton.Text = textos.CobrarBotton;
                ConfigurarColumnasOrdenes(textos);
            }
            catch (Exception ex) 
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
        }

        private void FormCobrarDiagnostico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void CobrarButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewReparaciones.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("SeleccionarReparacion");
                return;
            }
            var reparacionDTO = (ReparacionDTO_234TL)dataGridViewReparaciones.SelectedRows[0].DataBoundItem;

            try
            {
                Reparacionbll.CobrarDiagnostico(numeroReparacion: Convert.ToInt32(reparacionDTO.NumeroReparacion),numeroTarjeta: NumeroTarjetaTextBox.Text,codigoSeguridad: CodigoSeguridadTextBox.Text,vencimiento: VencimientoTextBox.Text);
                Utilitarios_234TL.MensajeExito("DiagnosticoCobradoExito");
                Cobrado?.Invoke(this, EventArgs.Empty); 
                this.Close();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);

                switch (ex.Nombre)
                {
                    case "NumeroTarjeta":
                        NumeroTarjetaTextBox.Focus();
                        break;
                    case "CodigoSeguridad":
                        CodigoSeguridadTextBox.Focus();
                        break;
                    case "Vencimiento":
                        VencimientoTextBox.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoCobrarDiagnostico", ex);
            }
        }
        private void ConfigurarColumnasOrdenes(FormCobrarDiagnostico_234TL textos)
        {
            dataGridViewReparaciones.AutoGenerateColumns = false;
            dataGridViewReparaciones.Columns.Clear();

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroReparacion", HeaderText = textos.Columna_NumeroReparacion, DataPropertyName = "NumeroReparacion" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", HeaderText = textos.Columna_Estado, DataPropertyName = "Estado" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", HeaderText = textos.Columna_NumeroSerie, DataPropertyName = "NumeroSerie" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreCliente", HeaderText = textos.Columna_NombreCliente, DataPropertyName = "NombreCliente" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNICliente", HeaderText = textos.Columna_DNICliente, DataPropertyName = "DNICliente" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreTecnico", HeaderText = textos.Columna_NombreTecnico, DataPropertyName = "NombreTecnico" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_EspecialidadTecnico", HeaderText = textos.Columna_EspecialidadTecnico, DataPropertyName = "EspecialidadTecnico" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Cobrado", HeaderText = textos.Columna_Cobrado, DataPropertyName = "Cobrado" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_FacturaGenerada", HeaderText = textos.Columna_FacturaGenerada, DataPropertyName = "FacturaGenerada" });
            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_ComprobanteGenerado", HeaderText = textos.Columna_ComprobanteGenerado, DataPropertyName = "ComprobanteGenerado" });
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
