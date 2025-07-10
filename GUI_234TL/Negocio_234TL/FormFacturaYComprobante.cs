using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System.Diagnostics;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormFacturaYComprobante : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        ReparacionDTOBLL_234TL ReparacionDTObll = new();

        public FormFacturaYComprobante()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            ConfigurarDataGrids();
            IdiomasManager_234TL.Instancia.NotificarActuales();
            dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.FacturaYComprobante;

                this.Text = textos.Title;

                CobrarDiagnosticoButton.Text = textos.CobrarDiagnosticoBotton;
                CrearFacturaButton.Text = textos.CrearFacturaBotton;
                CrearComprobanteButton.Text = textos.CrearComprobanteBotton;

                ConfigurarColumnasOrdenes(textos);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
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
                Utilitarios_234TL.MensajeAdvertencia("SeleccionarReparacion");
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
                facturaBLL.GenerarPdf(factura);
                Utilitarios_234TL.MensajeExito("FacturaGeneradaExito", factura.NumeroFactura, numeroReparacion);
                string rutaPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Facturas", $"{factura.NumeroFactura}.pdf");
                if (File.Exists(rutaPdf))
                {
                    Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
                }
                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorGenerarFactura", ex);
            }

        }

        private void CrearComprobanteButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewReparaciones.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("SeleccionarReparacion");
                return;
            }

            var filaSeleccionada = dataGridViewReparaciones.SelectedRows[0];
            var reparacionDTO = (ReparacionDTO_234TL)filaSeleccionada.DataBoundItem;

            try
            {
                int numeroReparacion = int.Parse(reparacionDTO.NumeroReparacion);
                ComprobanteIngresoBLL_234TL comprobanteBLL = new();

                var comprobante = comprobanteBLL.CrearComprobanteParaReparacion(numeroReparacion);

                comprobanteBLL.GenerarPdf(comprobante);

                Utilitarios_234TL.MensajeExito("ComprobanteGeneradoExito", comprobante.NumeroIngreso, numeroReparacion);
                string rutaPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprobantes", $"{comprobante.NumeroIngreso}.pdf");
                if (File.Exists(rutaPdf))
                {
                    Process.Start(new ProcessStartInfo(rutaPdf) { UseShellExecute = true });
                }
                dataGridViewReparaciones.DataSource = null;
                dataGridViewReparaciones.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorGenerarComprobante", ex);
            }
        }
        private void ConfigurarColumnasOrdenes(FormFacturaYComprobatne_234TL textos)
        {
            dataGridViewReparaciones.AutoGenerateColumns = false;
            dataGridViewReparaciones.Columns.Clear();
            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_NumeroReparacion",
                DataPropertyName = "NumeroReparacion",
                HeaderText = textos.Columna_NumeroReparacion
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_Estado",
                DataPropertyName = "Estado",
                HeaderText = textos.Columna_Estado
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_NumeroSerie",
                DataPropertyName = "NumeroSerie",
                HeaderText = textos.Columna_NumeroSerie
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_NombreCliente",
                DataPropertyName = "NombreCliente",
                HeaderText = textos.Columna_NombreCliente
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_DNICliente",
                DataPropertyName = "DNICliente",
                HeaderText = textos.Columna_DNICliente
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_NombreTecnico",
                DataPropertyName = "NombreTecnico",
                HeaderText = textos.Columna_NombreTecnico
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Columna_EspecialidadTecnico",
                DataPropertyName = "EspecialidadTecnico",
                HeaderText = textos.Columna_EspecialidadTecnico
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Columna_Cobrado",
                DataPropertyName = "Cobrado",
                HeaderText = textos.Columna_Cobrado
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Columna_FacturaGenerada",
                DataPropertyName = "FacturaGenerada",
                HeaderText = textos.Columna_FacturaGenerada
            });

            dataGridViewReparaciones.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Columna_ComprobanteGenerado",
                DataPropertyName = "ComprobanteGenerado",
                HeaderText = textos.Columna_ComprobanteGenerado
            });
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
