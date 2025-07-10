using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Enum_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using System.Text.RegularExpressions;
using Wilhjem;

namespace GUI_234TL
{
    public partial class FormIngresoEquipo : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private readonly EquipoBLL_234TL bll = new EquipoBLL_234TL();
        public event EventHandler EquipoAgregadoOEliminado;

        public FormIngresoEquipo()
        {
            InitializeComponent();
            Utilitarios_234TL.SuscribirAIdiomas(this);
            CargarEquipos();
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.IngresoEquipo;
                var textosEnum = Traduccion.Enums;
                this.Text = textos.Title;

                NumeroSerieLabel.Text = textos.Label_NumeroSerie;
                MarcaLabel.Text = textos.Label_Marca;
                ModeloLabel.Text = textos.Label_Modelo;
                EstadoVisualLabel.Text = textos.Label_Estado;
                FallaLabel.Text = textos.Label_Falla;
                DesarmadoLabel.Text = textos.Label_Desarmado;
                DañoVisualLabel.Text = textos.Label_DañoVisible;

                IngresarEquipoButton.Text = textos.IngresarBotton;
                EliminarEquipo.Text = textos.EliminarBotton;
                Utilitarios_234TL.PoblarComboBox<MarcaEquipos_234TL>(MarcaComboBox, textosEnum.EstadoEquipo);
                Utilitarios_234TL.PoblarComboBox<EstadoEquipo_234TL>(EstadoComboBox, textosEnum.EstadoEquipo);
                Utilitarios_234TL.PoblarComboBox<OpcionBool_234TL>(DañoVisibleComboBox, textosEnum.OpcionBool);
                Utilitarios_234TL.PoblarComboBox<OpcionBool_234TL>(DesarmadoComboBox, textosEnum.OpcionBool);
                ConfigurarColumnasDataGridView(textos);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorTraduccion", ex);
            }
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
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaNumeroSerieObligatorio");
                    NumeroSerieTipoTextBox.Focus();
                    return;
                }
                if (!Regex.IsMatch(NumeroSerieTipoTextBox.Text, @"^\d{5,9}$"))
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaNumeroSerieFormatoInvalido");
                    NumeroSerieTipoTextBox.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(ModeloTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaModeloObligatorio");
                    ModeloTextBox.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(FallaTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaFallaObligatoria");
                    FallaTextBox.Focus();
                    return;
                }
                if (MarcaComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaMarcaNoSeleccionada");
                    MarcaComboBox.Focus();
                    return;
                }
                if (EstadoComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaEstadoNoSeleccionado");
                    EstadoComboBox.Focus();
                    return;
                }
                if (DañoVisibleComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaDañoVisibleNoSeleccionado");
                    DañoVisibleComboBox.Focus();
                    return;
                }
                if (DesarmadoComboBox.SelectedIndex < 0)
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaDesarmadoNoSeleccionado");
                    DesarmadoComboBox.Focus();
                    return;
                }

                if (bll.ExisteEquipo(NumeroSerieTipoTextBox.Text))
                {
                    Utilitarios_234TL.MensajeAdvertencia("AdvertenciaNumeroSerieDuplicado");
                    return;
                }
                var marca = ((KeyValuePair<MarcaEquipos_234TL, string>)MarcaComboBox.SelectedItem).Key;
                var estado = ((KeyValuePair<EstadoEquipo_234TL, string>)EstadoComboBox.SelectedItem).Key;
                bool desarmado = ((KeyValuePair<OpcionBool_234TL, string>)DesarmadoComboBox.SelectedItem).Key == OpcionBool_234TL.Si;
                bool dañoVisible = ((KeyValuePair<OpcionBool_234TL, string>)DañoVisibleComboBox.SelectedItem).Key == OpcionBool_234TL.Si;

                bll.IngresarEquipo(numeroSerie: NumeroSerieTipoTextBox.Text, modelo: ModeloTextBox.Text, marca: marca.ToString(), estado: estado.ToString(), falla: FallaTextBox.Text, desarmado: desarmado, dañoVisible: dañoVisible);

                CargarEquipos();
                LimpiarFormulario();

                Utilitarios_234TL.MensajeExito("ExitoEquipoIngresado");
                EquipoAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorIngresarEquipo", ex);
            }
        }

        private void EliminarEquipo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeAdvertencia("AdvertenciaSeleccionarEquipo");
                return;
            }

            try
            {
                var equipo = (Equipo_234TL)dataGridView1.SelectedRows[0].DataBoundItem;

                if (Utilitarios_234TL.MensajeConfirmacion(this,"ConfirmacionEliminarEquipo", equipo.NumeroSerie) == DialogResult.Yes)
                {
                    bll.Eliminar(equipo); 
                    CargarEquipos();
                    Utilitarios_234TL.MensajeExito("ExitoEquipoEliminado");
                    EquipoAgregadoOEliminado?.Invoke(this, EventArgs.Empty);
                }

            }
            catch (ValidacionesException_234TL ex) 
            {
                Utilitarios_234TL.MensajeError(ex.Message, null, ex.Args);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorEliminarEquipo", ex);
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


        private void ConfigurarColumnasDataGridView(FormIngresoEquipo_234TL textos)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroSerie", 
                DataPropertyName = "NumeroSerie",
                HeaderText = textos.Columna_NumeroSerie 
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                DataPropertyName = "Marca",
                HeaderText = textos.Columna_Marca
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Modelo",
                DataPropertyName = "Modelo",
                HeaderText = textos.Columna_Modelo
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                DataPropertyName = "Estado",
                HeaderText = textos.Columna_Estado
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FallaReportada",
                DataPropertyName = "FallaReportada",
                HeaderText = textos.Columna_Falla
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoraIngreso",
                DataPropertyName = "HoraIngreso",
                HeaderText = textos.Columna_FechaIngreso
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Desarmado",
                DataPropertyName = "Desarmado",
                HeaderText = textos.Columna_Desarmado
            });

            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "DañoVisible",
                DataPropertyName = "DañoVisible",
                HeaderText = textos.Columna_DañoVisible
            });
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
