using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Enum_234TL;
using Servicios_234TL.Observer_234TL;
using System.Text.RegularExpressions;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormIngresoEquipo : Form, IObserver_234TL<Dictionary<string, string>>
    {
        private readonly EquipoBLL_234TL bll = new EquipoBLL_234TL();
        public event EventHandler EquipoAgregadoOEliminado;

        public FormIngresoEquipo()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            ConfigurarColumnasDataGridView();
            CargarEquipos();
            ConfigurarCombobox();
        }

        public void Update(Dictionary<string, string> Traduccion)
        {
            try
            {
                this.Text = Traduccion["FormIngresoEquipo_Titulo"];
                NumeroSerieLabel.Text = Traduccion["FormIngresoEquipo_NumeroSerieLabel"];
                MarcaLabel.Text = Traduccion["FormIngresoEquipo_MarcaLabel"];
                ModeloLabel.Text = Traduccion["FormIngresoEquipo_ModeloLabel"];
                EstadoVisualLabel.Text = Traduccion["FormIngresoEquipo_EstadoLabel"];
                FallaLabel.Text = Traduccion["FormIngresoEquipo_FallaLabel"];
                DesarmadoLabel.Text = Traduccion["FormIngresoEquipo_DesarmadoLabel"];
                DañoVisualLabel.Text = Traduccion["FormIngresoEquipo_DañoVisibleLabel"];
                IngresarEquipoButton.Text = Traduccion["FormIngresoEquipo_IngresarButton"];
                EliminarEquipo.Text = Traduccion["FormIngresoEquipo_EliminarButton"];

                if (dataGridView1.Columns.Count >= 5)
                {
                    dataGridView1.Columns[0].HeaderText = Traduccion["ColumnaNumeroSerie"];
                    dataGridView1.Columns[1].HeaderText = Traduccion["ColumnaMarca"];
                    dataGridView1.Columns[2].HeaderText = Traduccion["ColumnaModelo"];
                    dataGridView1.Columns[3].HeaderText = Traduccion["ColumnaEstado"];
                    dataGridView1.Columns[4].HeaderText = Traduccion["ColumnaFalla"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en traducción: {ex.Message}");
            }
        }

        private void ConfigurarCombobox()
        {
            MarcaComboBox.DataSource = Enum.GetValues(typeof(MarcaEquipos_234TL));
            EstadoComboBox.DataSource = Enum.GetValues(typeof(EstadoEquipo_234TL));
            DañoVisibleComboBox.DataSource = Enum.GetValues(typeof(OpcionBool_234TL));
            DesarmadoComboBox.DataSource = Enum.GetValues(typeof(OpcionBool_234TL));
            DañoVisibleComboBox.SelectedIndex = 0; 
            DesarmadoComboBox.SelectedIndex = 0;
        }

        private void FormIngresoEquipo_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilitarios_234TL.DesuscribirDeIdiomas(this);
        }

        private void IngresarEquipoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NumeroSerieTipoTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_NumeroSerieObligatorio");
                    NumeroSerieTipoTextBox.Focus();
                    return;
                }
                if (!Regex.IsMatch(NumeroSerieTipoTextBox.Text, @"^\d{5,9}$"))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_NumeroSerieFormatoInvalido"); 
                    NumeroSerieTipoTextBox.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(ModeloTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_ModeloObligatorio");
                    ModeloTextBox.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(FallaTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_FallaObligatoria");
                    FallaTextBox.Focus();
                    return;
                }
                if (MarcaComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_MarcaNoSeleccionada");
                    MarcaComboBox.Focus();
                    return;
                }
                if (EstadoComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_EstadoNoSeleccionado");
                    EstadoComboBox.Focus();
                    return;
                }
                if (DañoVisibleComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_DañoVisibleNoSeleccionado");
                    DañoVisibleComboBox.Focus();
                    return;
                }
                if (DesarmadoComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_DesarmadoNoSeleccionado");
                    DesarmadoComboBox.Focus();
                    return;
                }

                if (bll.ExisteEquipo(NumeroSerieTipoTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("Advertencia_NumeroSerieDuplicado");
                    return;
                }
                bool desarmado = (OpcionBool_234TL)DesarmadoComboBox.SelectedItem == OpcionBool_234TL.Si;
                bool dañoVisible = (OpcionBool_234TL)DañoVisibleComboBox.SelectedItem == OpcionBool_234TL.Si;

                bll.IngresarEquipo(numeroSerie: NumeroSerieTipoTextBox.Text,modelo: ModeloTextBox.Text,marca: MarcaComboBox.SelectedItem.ToString(),estado: EstadoComboBox.SelectedItem.ToString(),falla: FallaTextBox.Text,desarmado: (OpcionBool_234TL)DesarmadoComboBox.SelectedItem == OpcionBool_234TL.Si,dañoVisible: (OpcionBool_234TL)DañoVisibleComboBox.SelectedItem == OpcionBool_234TL.Si);

                CargarEquipos();
                LimpiarFormulario();


                Utilitarios_234TL.MensajeExito("Exito_EquipoIngresado");
                EquipoAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Error_IngresarEquipo", ex);
            }
        }

        private void EliminarEquipo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("Advertencia_SeleccionarEquipo");
                return;
            }

            try
            {
                var equipo = (Equipo_234TL)dataGridView1.SelectedRows[0].DataBoundItem;
                bll.Eliminar(equipo);
                CargarEquipos();
                Utilitarios_234TL.MensajeExito("Exito_EquipoEliminado");
                EquipoAgregadoOEliminado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("Error_EliminarEquipo", ex);
            }
        }

        private void LimpiarFormulario()
        {
            NumeroSerieTipoTextBox.Clear();
            MarcaComboBox.SelectedIndex = 0;
            ModeloTextBox.Clear();
            EstadoComboBox.SelectedIndex = 0;
            FallaTextBox.Clear();
            DañoVisibleComboBox.SelectedIndex = 0;
            DesarmadoComboBox.SelectedIndex = 0;
        }


        private void ConfigurarColumnasDataGridView()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroSerie", HeaderText = "Número de Serie" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Marca", HeaderText = "Marca" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Modelo", HeaderText = "Modelo" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FallaReportada", HeaderText = "Falla Reportada" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoraIngreso", HeaderText = "Hora de Ingreso" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Desarmado", HeaderText = "Desarmado" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DañoVisible", HeaderText = "Daño Visible" });
        }

        private void CargarEquipos()
        {
            try
            {
                dataGridView1.DataSource = bll.GetAll();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorCargarEquipos", ex);
            }
        }

    }
}
