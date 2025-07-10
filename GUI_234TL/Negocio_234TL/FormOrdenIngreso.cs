using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Enum_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormOrdenIngreso : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        EquipoBLL_234TL Equipobll = new();
        ClienteBLL_234TL Clientebll = new();
        TecnicoBLL_234TL Tecnicobll = new();
        ReparacionBLL_234TL Reparacionbll = new();
        ReparacionDTOBLL_234TL ReparacionDTObll = new();

        private string textoBaseClienteSeleccionado = "Cliente seleccionado:";
        private string textoBaseEquipoSeleccionado = "Equipo seleccionado:";
        private string textoBaseTecnicoSeleccionado = "Técnico seleccionado:";
        public FormOrdenIngreso()
        {
            InitializeComponent();
            ConfigurarDataGrids();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.OrdenIngreso;
                var textosEnum = Traduccion.Enums;

                this.Text = textos.Title;

                ClientesLabel.Text = textos.Label_Clientes;
                EquiposLabel.Text = textos.Label_Equipos;
                TecnicosLabel.Text = textos.Label_Tecnicos;
                OrdenesLabel.Text = textos.Label_Ordenes;
                EstadoLabel.Text = textos.Label_Estado;

                textoBaseClienteSeleccionado = textos.Label_ClienteSeleccionado;
                textoBaseEquipoSeleccionado = textos.Label_EquipoSeleccionado;
                textoBaseTecnicoSeleccionado = textos.Label_TecnicoSeleccionado;
                OrdenSeleccionadaLabel.Text = textos.Label_OrdenSeleccionada;

                ConfigurarColumnasOrdenes(textos);
                ConfigurarColumnasClientes(textos);
                ConfigurarColumnasEquipos(textos);
                ConfigurarColumnasTecnicos(textos);

                Utilitarios_234TL.PoblarComboBox<EstadoReparacion_234TL>(EstadoComboBox, textosEnum.EstadoReparacion);

                CargarDatos();

                Equipobutton.Text = textos.NuevoEquipoBotton;
                Clientebutton.Text = textos.NuevoClienteBotton;
                Ordenbutton.Text = textos.CrearOrdenBotton;
                EliminarOrdenButton.Text = textos.EliminarOrdenBotton;

                dataGridViewClientes_SelectionChanged(null, null);
                dataGridViewEquipos_SelectionChanged(null, null);
                dataGridViewTecnicos_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
        }

        private void FormOrdenIngreso_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void Equipobutton_Click(object sender, EventArgs e)
        {
            FormIngresoEquipo formIngresoEquipo = new();
            formIngresoEquipo.Owner = this;
            formIngresoEquipo.StartPosition = FormStartPosition.CenterParent;
            formIngresoEquipo.EquipoAgregadoOEliminado += (s, ev) =>
            {
                CargarDatos();
            };
            formIngresoEquipo.ShowDialog(this);

        }

        private void Clientebutton_Click(object sender, EventArgs e)
        {
            FormRegistrarCliente formRegistrarCliente = new();
            formRegistrarCliente.Owner = this;
            formRegistrarCliente.StartPosition = FormStartPosition.CenterParent;
            formRegistrarCliente.ClienteAgregadoOEliminado += (s, ev) =>
            {
                CargarDatos();
            };
            formRegistrarCliente.ShowDialog(this);
        }

        private void Ordenbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewClientes.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("SeleccionClienteIncompleta");
                    return;
                }
                if (dataGridViewEquipos.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("SeleccionEquipoIncompleta");
                    return;
                }
                if (dataGridViewTecnicos.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("SeleccionTecnicoIncompleta");
                    return;
                }

                var cliente = (Cliente_234TL)dataGridViewClientes.SelectedRows[0].DataBoundItem;
                var equipo = (Equipo_234TL)dataGridViewEquipos.SelectedRows[0].DataBoundItem;
                var tecnico = (Tecnico_234TL)dataGridViewTecnicos.SelectedRows[0].DataBoundItem;
                var estadoEnum = (EstadoReparacion_234TL)EstadoComboBox.SelectedValue;
                var estado = estadoEnum.ToString();

                Reparacionbll.CrearReparacion(cliente, equipo, tecnico, estado);
                Utilitarios_234TL.MensajeExito("ReparacionCreadaExito");
                CargarDatos();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoCrearOrden", ex);
            }

        }

        private void EliminarOrdenButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("SeleccionOrdenIncompleta");
                    return;
                }
                var reparacionDTO = (ReparacionDTO_234TL)dataGridView1.SelectedRows[0].DataBoundItem;

                var reparacion = Reparacionbll.GetAll().FirstOrDefault(e => string.Equals(e.NumeroReparacion.ToString(), reparacionDTO.NumeroReparacion, StringComparison.OrdinalIgnoreCase));

                if (reparacion != null)
                {
                    if (Utilitarios_234TL.MensajeConfirmacion(this,"ConfirmacionEliminarOrden", reparacion.NumeroReparacion) == DialogResult.Yes)
                    {
                        Reparacionbll.Eliminar(reparacion);
                        CargarDatos();
                        Utilitarios_234TL.MensajeExito("OrdenEliminadaExito");
                    }
                }
                else
                {
                    Utilitarios_234TL.MensajeAdvertencia("ReparacionNoEncontrada", reparacionDTO.NumeroReparacion);
                }
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperadoEliminarOrden", ex);
            }
        }

        private void LimpiarSeleccionDataGrids()
        {
            dataGridViewClientes.ClearSelection();
            dataGridViewEquipos.ClearSelection();
            dataGridViewTecnicos.ClearSelection();
            dataGridView1.ClearSelection();
        }

        private void CargarDatos()
        {
            try
            {
                var equiposEnReparacion = Reparacionbll.GetAll().Select(r => r.Equipo.NumeroSerie).ToHashSet();
                var equiposDisponibles = Equipobll.GetAll().Where(e => !equiposEnReparacion.Contains(e.NumeroSerie)).ToList();
                dataGridViewEquipos.DataSource = equiposDisponibles;
                dataGridViewClientes.DataSource = Clientebll.GetAll();
                dataGridViewTecnicos.DataSource = Tecnicobll.GetAll();
                dataGridView1.DataSource = ReparacionDTObll.ObtenerReparacionesDTO();
                LimpiarSeleccionDataGrids();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorCargaGeneral", ex);
            }
        }
        private void ConfigurarDataGrids()
        {
            dataGridViewClientes.ReadOnly = true;
            dataGridViewClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClientes.MultiSelect = false;
            dataGridViewClientes.AllowUserToAddRows = false;
            dataGridViewClientes.AllowUserToDeleteRows = false;
            dataGridViewClientes.AllowUserToOrderColumns = false;
            dataGridViewClientes.AllowUserToResizeRows = false;
            dataGridViewClientes.EditMode = DataGridViewEditMode.EditProgrammatically;

            dataGridViewEquipos.ReadOnly = true;
            dataGridViewEquipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEquipos.MultiSelect = false;
            dataGridViewEquipos.AllowUserToAddRows = false;
            dataGridViewEquipos.AllowUserToDeleteRows = false;
            dataGridViewEquipos.AllowUserToOrderColumns = false;
            dataGridViewEquipos.AllowUserToResizeRows = false;
            dataGridViewEquipos.EditMode = DataGridViewEditMode.EditProgrammatically;

            dataGridViewTecnicos.ReadOnly = true;
            dataGridViewTecnicos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTecnicos.MultiSelect = false;
            dataGridViewTecnicos.AllowUserToAddRows = false;
            dataGridViewTecnicos.AllowUserToDeleteRows = false;
            dataGridViewTecnicos.AllowUserToOrderColumns = false;
            dataGridViewTecnicos.AllowUserToResizeRows = false;
            dataGridViewTecnicos.EditMode = DataGridViewEditMode.EditProgrammatically;

            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ConfigurarColumnasOrdenes(FormOrdenIngreso_234TL textos)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroReparacion", DataPropertyName = "NumeroReparacion", HeaderText = textos.Columna_NumeroReparacion });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", DataPropertyName = "Estado", HeaderText = textos.Columna_Estado });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", DataPropertyName = "NumeroSerie", HeaderText = textos.Columna_NumeroSerie });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreCliente", DataPropertyName = "NombreCliente", HeaderText = "Cliente" }); 
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNICliente", DataPropertyName = "DNICliente", HeaderText = "DNI Cliente" }); 
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreTecnico", DataPropertyName = "NombreTecnico", HeaderText = "Técnico" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_EspecialidadTecnico", DataPropertyName = "EspecialidadTecnico", HeaderText = textos.Columna_Especialidad });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Cobrado", DataPropertyName = "Cobrado", HeaderText = "Cobrado" }); 
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_FacturaGenerada", DataPropertyName = "FacturaGenerada", HeaderText = "Factura" }); 
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_ComprobanteGenerado", DataPropertyName = "ComprobanteGenerado", HeaderText = "Comprobante" }); 
        }

        private void ConfigurarColumnasClientes(FormOrdenIngreso_234TL textos)
        {
            dataGridViewClientes.AutoGenerateColumns = false;
            dataGridViewClientes.Columns.Clear();
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNI", DataPropertyName = "Dni", HeaderText = textos.Columna_DNI });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Nombre", DataPropertyName = "Nombre", HeaderText = textos.Columna_Nombre });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Apellido", DataPropertyName = "Apellido", HeaderText = textos.Columna_Apellido });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Telefono", DataPropertyName = "Telefono", HeaderText = textos.Columna_Telefono });
        }

        private void ConfigurarColumnasEquipos(FormOrdenIngreso_234TL textos)
        {
            dataGridViewEquipos.AutoGenerateColumns = false;
            dataGridViewEquipos.Columns.Clear();
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", DataPropertyName = "NumeroSerie", HeaderText = textos.Columna_NumeroSerie });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Marca", DataPropertyName = "Marca", HeaderText = textos.Columna_Marca });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Modelo", DataPropertyName = "Modelo", HeaderText = textos.Columna_Modelo });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", DataPropertyName = "Estado", HeaderText = textos.Columna_Estado });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_FallaReportada", DataPropertyName = "FallaReportada", HeaderText = textos.Columna_Falla });
            dataGridViewEquipos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Desarmado", DataPropertyName = "Desarmado", HeaderText = "Desarmado" });
            dataGridViewEquipos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_DañoVisible", DataPropertyName = "DañoVisible", HeaderText = "Daño Visible" });
        }
        private void ConfigurarColumnasTecnicos(FormOrdenIngreso_234TL textos)
        {
            dataGridViewTecnicos.AutoGenerateColumns = false;
            dataGridViewTecnicos.Columns.Clear();
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNI", DataPropertyName = "Dni", HeaderText = textos.Columna_DNI });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Nombre", DataPropertyName = "Nombre", HeaderText = textos.Columna_Nombre });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Apellido", DataPropertyName = "Apellido", HeaderText = textos.Columna_Apellido });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Especialidad", DataPropertyName = "Especialidad", HeaderText = textos.Columna_Especialidad });
            dataGridViewTecnicos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Disponible", DataPropertyName = "Disponible", HeaderText = textos.Columna_Disponible });
        }


        private void dataGridViewEquipos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEquipos.SelectedRows.Count == 1)
            {
                var equipo = (Equipo_234TL)dataGridViewEquipos.SelectedRows[0].DataBoundItem;
                EquipoSeleccionadoLabel.Text = $"{textoBaseEquipoSeleccionado} {equipo.NumeroSerie}";
            }
            else
            {
                EquipoSeleccionadoLabel.Text = $"{textoBaseEquipoSeleccionado} -";
            }
        }

        private void dataGridViewTecnicos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTecnicos.SelectedRows.Count == 1)
            {
                var tecnico = (Tecnico_234TL)dataGridViewTecnicos.SelectedRows[0].DataBoundItem;
                TecnicoSeleccionadoLabel.Text = $"{textoBaseTecnicoSeleccionado} {tecnico.Nombre} {tecnico.Apellido}";
            }
            else
            {
                TecnicoSeleccionadoLabel.Text = $"{textoBaseTecnicoSeleccionado} -";
            }
        }

        private void dataGridViewClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count == 1)
            {
                var cliente = (Cliente_234TL)dataGridViewClientes.SelectedRows[0].DataBoundItem;
                ClienteSeleccionadoLabel.Text = $"{textoBaseClienteSeleccionado} {cliente.Nombre} {cliente.Apellido}";
            }
            else
            {
                ClienteSeleccionadoLabel.Text = $"{textoBaseClienteSeleccionado} -";
            }
        }

    }
}