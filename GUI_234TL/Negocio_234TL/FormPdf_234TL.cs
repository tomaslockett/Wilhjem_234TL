using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL.Negocio_234TL
{
    public partial class FormPdf_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private readonly FacturaBLL_234TL facturaBLL = new FacturaBLL_234TL();
        private readonly ComprobanteIngresoBLL_234TL comprobanteBLL = new ComprobanteIngresoBLL_234TL();
        public FormPdf_234TL()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                FacturadataGridView.DataSource = facturaBLL.GetAll();
                ComprobantedataGridView.DataSource = comprobanteBLL.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los documentos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AbrirPdf(string ruta)
        {
            if (File.Exists(ruta))
            {
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show($"El archivo PDF no se encontró en la ruta:\n{ruta}", "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Volverbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FacturadataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var factura = (Factura_234TL)FacturadataGridView.Rows[e.RowIndex].DataBoundItem;

            string rutaPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Facturas", $"{factura.NumeroFactura}.pdf");

            AbrirPdf(rutaPdf);
        }

        private void ComprobantedataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var comprobante = (ComprobanteIngreso_234TL)ComprobantedataGridView.Rows[e.RowIndex].DataBoundItem;
            string rutaPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprobantes", $"{comprobante.NumeroIngreso}.pdf");

            AbrirPdf(rutaPdf);
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.FormPdf;
                if (textos == null)
                {
                    return;
                }
                this.Text = textos.Title;
                Facturaslabel.Text = textos.Label_Facturas;
                Comprobantelabel.Text = textos.Label_Comprobantes;
                Volverbutton.Text = textos.Boton_Volver;

                ConfigurarGrids(textos);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
        }

        private void ConfigurarGrids(Servicios_234TL.Observer_234TL.Traducciones_234TL.FormPdf_234TL textos)
        {
            ConfiguraGridBase(FacturadataGridView);
            FacturadataGridView.Columns.Clear();
            FacturadataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroFactura", DataPropertyName = "NumeroFactura", HeaderText = textos.Columna_NumeroFactura, Width = 120 });
            FacturadataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cliente", DataPropertyName = "Cliente", HeaderText = textos.Columna_Cliente, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            FacturadataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", DataPropertyName = "Total", HeaderText = textos.Columna_Total, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            FacturadataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Reparacion", DataPropertyName = "Reparacion", HeaderText = textos.Columna_NumeroReparacion });

            ConfiguraGridBase(ComprobantedataGridView);
            ComprobantedataGridView.Columns.Clear();
            ComprobantedataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroIngreso", DataPropertyName = "NumeroIngreso", HeaderText = textos.Columna_NumeroIngreso, Width = 150 });
            ComprobantedataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Equipo", DataPropertyName = "Equipo", HeaderText = textos.Columna_Equipo, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            ComprobantedataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoraIngreso", DataPropertyName = "HoraIngreso", HeaderText = textos.Columna_HoraIngreso, Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } });
        }

        private void ConfiguraGridBase(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
        }
    }
}
