using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Enum_234TL;
using Servicios_234TL.Observer_234TL;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormOrdenIngreso : Form, IObserver_234TL<Dictionary<string, string>>
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
            CargarDatos();
            EstadoComboBox.DataSource = Enum.GetValues(typeof(EstadoReparacion_234TL));


        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            this.Text = Traduccion["FormOrdenIngreso_Titulo"];
            ClientesLabel.Text = Traduccion["FormOrdenIngreso_ClientesLabel"];
            EquiposLabel.Text = Traduccion["FormOrdenIngreso_EquiposLabel"];
            TecnicosLabel.Text = Traduccion["FormOrdenIngreso_TecnicosLabel"];
            ClienteSeleccionadoLabel.Text = Traduccion["FormOrdenIngreso_ClienteSeleccionadoLabel"];
            EquipoSeleccionadoLabel.Text = Traduccion["FormOrdenIngreso_EquipoSeleccionadoLabel"];
            TecnicoSeleccionadoLabel.Text = Traduccion["FormOrdenIngreso_TecnicoSeleccionadoLabel"];
            OrdenesLabel.Text = Traduccion["FormOrdenIngreso_OrdenesLabel"];
            OrdenSeleccionadaLabel.Text = Traduccion["FormOrdenIngreso_OrdenSeleccionadaLabel"];
            Equipobutton.Text = Traduccion["FormOrdenIngreso_BotonEquipo"];
            Clientebutton.Text = Traduccion["FormOrdenIngreso_BotonCliente"];
            Ordenbutton.Text = Traduccion["FormOrdenIngreso_BotonCrearOrden"];
            EliminarOrdenButton.Text = Traduccion["FormOrdenIngreso_BotonEliminarOrden"];
            textoBaseClienteSeleccionado = Traduccion["FormOrdenIngreso_ClienteSeleccionadoLabel"];
            textoBaseEquipoSeleccionado = Traduccion["FormOrdenIngreso_EquipoSeleccionadoLabel"];
            textoBaseTecnicoSeleccionado = Traduccion["FormOrdenIngreso_TecnicoSeleccionadoLabel"];

            EstadoComboBox.DataSource = Enum.GetValues(typeof(EstadoReparacion_234TL)).Cast<EstadoReparacion_234TL>().Select(e => Traduccion[$"EstadoReparacion_{e.ToString()}"].ToList());
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
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionClienteIncompleta");
                    return;
                }
                if (dataGridViewEquipos.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionEquipoIncompleta");
                    return;
                }
                if (dataGridViewTecnicos.SelectedRows.Count != 1)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionTecnicoIncompleta");
                    return;
                }

                var cliente = (Cliente_234TL)dataGridViewClientes.SelectedRows[0].DataBoundItem;
                var equipo = (Equipo_234TL)dataGridViewEquipos.SelectedRows[0].DataBoundItem;
                var tecnico = (Tecnico_234TL)dataGridViewTecnicos.SelectedRows[0].DataBoundItem;
                var estadoEnum = (EstadoReparacion_234TL)EstadoComboBox.SelectedValue;
                var estado = estadoEnum.ToString();

                Reparacionbll.CrearReparacion(cliente, equipo, tecnico, estado);
                Utilitarios_234TL.MensajeExito("Exito_ReparacionCreada");

                CargarDatos();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Error_CreacionReparacion", ex);
            }

        }

        private void EliminarOrdenButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionIncompleta");
                return;
            }
            var reparacionDTO = (ReparacionDTO_234TL)dataGridView1.SelectedRows[0].DataBoundItem;

            var reparacion = Reparacionbll.GetAll().FirstOrDefault(e => string.Equals(e.NumeroReparacion.ToString(), reparacionDTO.NumeroReparacion, StringComparison.OrdinalIgnoreCase));

            if (reparacion != null)
            {
                Reparacionbll.Eliminar(reparacion);
                CargarDatos();
            }
            else
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_NoSeEncontroReparacion");
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
                ConfigurarColumnasOrdenes();
                ConfigurarColumnasClientes();
                ConfigurarColumnasEquipos();
                ConfigurarColumnasTecnicos();
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
                Utilitarios_234TL.MensajeError("Error_CargaGeneral", ex);
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
        private void ConfigurarColumnasOrdenes()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroReparacion", HeaderText = "Número", DataPropertyName = "NumeroReparacion" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", HeaderText = "Estado", DataPropertyName = "Estado" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", HeaderText = "N° Serie", DataPropertyName = "NumeroSerie" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreCliente", HeaderText = "Cliente", DataPropertyName = "NombreCliente" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNICliente", HeaderText = "DNI", DataPropertyName = "DNICliente" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NombreTecnico", HeaderText = "Técnico", DataPropertyName = "NombreTecnico" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_EspecialidadTecnico", HeaderText = "Especialidad", DataPropertyName = "EspecialidadTecnico" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Cobrado", HeaderText = "Cobrado", DataPropertyName = "Cobrado" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_FacturaGenerada", HeaderText = "Factura Generada", DataPropertyName = "FacturaGenerada" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_ComprobanteGenerado", HeaderText = "Comprobante Generado", DataPropertyName = "ComprobanteGenerado" });
        }
        private void ConfigurarColumnasClientes()
        {
            dataGridViewClientes.AutoGenerateColumns = false;
            dataGridViewClientes.Columns.Clear();
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNI", HeaderText = "DNI", DataPropertyName = "Dni" });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Nombre", HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Apellido", HeaderText = "Apellido", DataPropertyName = "Apellido" });
            dataGridViewClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Telefono", HeaderText = "Telefono", DataPropertyName = "Telefono" });

        }

        private void ConfigurarColumnasEquipos()
        {
            dataGridViewEquipos.AutoGenerateColumns = false;
            dataGridViewEquipos.Columns.Clear();
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_NumeroSerie", HeaderText = "N° Serie", DataPropertyName = "NumeroSerie" });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Marca", HeaderText = "Marca", DataPropertyName = "Marca" });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Modelo", HeaderText = "Modelo", DataPropertyName = "Modelo" });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Estado", HeaderText = "Estado", DataPropertyName = "Estado" });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_FallaReportada", HeaderText = "Falla Reportada", DataPropertyName = "FallaReportada" });
            dataGridViewEquipos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_HoraIngreso", HeaderText = "Hora Ingreso", DataPropertyName = "HoraIngreso" });
            dataGridViewEquipos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Desarmado", HeaderText = "Desarmado", DataPropertyName = "Desarmado" });
            dataGridViewEquipos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_DañoVisible", HeaderText = "Daño Visible", DataPropertyName = "DañoVisible" });
        }

        private void ConfigurarColumnasTecnicos()
        {
            dataGridViewTecnicos.AutoGenerateColumns = false;
            dataGridViewTecnicos.Columns.Clear();
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_DNI", HeaderText = "DNI", DataPropertyName = "Dni" });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Nombre", HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Apellido", HeaderText = "Apellido", DataPropertyName = "Apellido" });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Telefono", HeaderText = "Telefono", DataPropertyName = "Telefono" });
            dataGridViewTecnicos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Columna_Especialidad", HeaderText = "Especialidad", DataPropertyName = "Especialidad" });
            dataGridViewTecnicos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Columna_Disponible", HeaderText = "Disponible", DataPropertyName = "Disponible" });
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