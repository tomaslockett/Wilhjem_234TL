using BE_234TL;
using BLL_234TL;
using Servicios_234TL.Enum_234TL;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using Wilhjem;

namespace GUI_234TL.Negocio_234TL
{
    public partial class FormTecnicos_234TL : Form, IObserver_234TL<TraduccionesClase_234TL>
    {
        private readonly TecnicoBLL_234TL bll = new TecnicoBLL_234TL();
        public FormTecnicos_234TL()
        {
            InitializeComponent();
            ConfigurarComboBoxes();
            ConfigurarColumnas(); 
            CargarTecnicos();
            ModificarButton.Visible = false; 

            Utilitarios_234TL.SuscribirAIdiomas(this);
            IdiomasManager_234TL.Instancia.NotificarActuales();
        }

        public void Update(TraduccionesClase_234TL Traduccion)
        {
            try
            {
                var textos = Traduccion.Forms.FormTecnicos;
                this.Text = textos.Title;
                TecnicosLabel.Text = textos.Label_Titulo;
                DNILabel.Text = textos.Label_DNI;
                NombreLabel.Text = textos.Label_Nombre;
                ApellidoLabel.Text = textos.Label_Apellido;
                TelefonoLabel.Text = textos.Label_Telefono;
                EspecialidadLabel.Text = textos.Label_Especialidad;
                DisponibleLabel.Text = textos.Label_Disponible;
                AgregarButton.Text = textos.Boton_Agregar;
                ModificarButton.Text = textos.Boton_Modificar;
                EliminarButton.Text = textos.Boton_Eliminar;


                dataGridView1.Columns["DNI"].HeaderText = textos.Columna_DNI;
                dataGridView1.Columns["Nombre"].HeaderText = textos.Columna_Nombre;
                dataGridView1.Columns["Apellido"].HeaderText = textos.Columna_Apellido;
                dataGridView1.Columns["Especialidad"].HeaderText = textos.Columna_Especialidad;
                dataGridView1.Columns["Disponible"].HeaderText = textos.Columna_Disponible;
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperado", ex);
            }
        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {
            LimpiarColoresError();
            if (string.IsNullOrWhiteSpace(DNItextBox.Text))
            {
                Utilitarios_234TL.MensajeError("DNIRequerido");
                DNItextBox.BackColor = Color.LightPink; DNItextBox.Focus();
                return; 
            }
            if (string.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                Utilitarios_234TL.MensajeError("NombreRequerido");
                NombretextBox.BackColor = Color.LightPink; NombretextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(ApellidotextBox.Text))
            {
                Utilitarios_234TL.MensajeError("ApellidoRequerido");
                ApellidotextBox.BackColor = Color.LightPink; ApellidotextBox.Focus();
                return;
            }
            if (EspecialidadcomboBox.SelectedItem == null)
            {
                Utilitarios_234TL.MensajeError("EspecialidadRequerida");
                EspecialidadcomboBox.BackColor = Color.LightPink; EspecialidadcomboBox.Focus();
                return;
            }
            if (DisponiblecomboBox.SelectedItem == null)
            {
                Utilitarios_234TL.MensajeAdvertencia("SeleccionarOpcion", DisponibleLabel.Text); 
                DisponiblecomboBox.BackColor = Color.LightPink; DisponiblecomboBox.Focus();
                return;
            }
            try
            {
                var tecnico = new Tecnico_234TL(DNItextBox.Text.Trim(), NombretextBox.Text.Trim(), ApellidotextBox.Text.Trim(), TelefonotextBox.Text.Trim(), EspecialidadcomboBox.Text); tecnico.Disponible = ((OpcionBool_234TL)DisponiblecomboBox.SelectedItem == OpcionBool_234TL.Si);

                bll.RegistrarNuevoTecnico(tecnico);
                Utilitarios_234TL.MensajeExito("TecnicoRegistrado");
                CargarTecnicos();
                LimpiarCampos();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);
                switch (ex.Nombre)
                {
                    case "DNI": DNItextBox.BackColor = Color.LightPink; DNItextBox.Focus(); break;
                    case "Nombre": NombretextBox.BackColor = Color.LightPink; NombretextBox.Focus(); break;
                    case "Apellido": ApellidotextBox.BackColor = Color.LightPink; ApellidotextBox.Focus(); break;
                    case "Especialidad": EspecialidadcomboBox.BackColor = Color.LightPink; EspecialidadcomboBox.Focus(); break;
                }
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperado", ex);
            }
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeInformacion("SeleccionarItem"); return;
            }

            try
            {
                var tecnico = (Tecnico_234TL)dataGridView1.SelectedRows[0].DataBoundItem;
                tecnico.Nombre = NombretextBox.Text.Trim();
                tecnico.Apellido = ApellidotextBox.Text.Trim();
                tecnico.Telefono = TelefonotextBox.Text.Trim();
                tecnico.Especialidad = EspecialidadcomboBox.Text;
                tecnico.Disponible = ((OpcionBool_234TL)DisponiblecomboBox.SelectedItem == OpcionBool_234TL.Si);

                bll.ModificarTecnico(tecnico);
                Utilitarios_234TL.MensajeExito("TecnicoModificado");
                CargarTecnicos();
                LimpiarCampos();
            }
            catch (ValidacionesException_234TL ex)
            {
                Utilitarios_234TL.MensajeError(ex.Message);
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorInesperado", ex);
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                Utilitarios_234TL.MensajeInformacion("SeleccionarItem"); return;
            }

            if (Utilitarios_234TL.MensajeConfirmacion(this,"Confirmacion_EliminarTecnico") == DialogResult.Yes)
            {
                try
                {
                    var tecnico = (Tecnico_234TL)dataGridView1.SelectedRows[0].DataBoundItem;
                    bll.EliminarTecnico(tecnico);
                    Utilitarios_234TL.MensajeExito("TecnicoEliminado");
                    CargarTecnicos();
                    LimpiarCampos();
                }
                catch (ValidacionesException_234TL ex)
                {
                    Utilitarios_234TL.MensajeError(ex.Message);
                }
                catch (Exception ex)
                {
                    Utilitarios_234TL.MensajeError("ErrorInesperado", ex);
                }
            }
        }
        private void CargarTecnicos()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = bll.GetAll();
            }
            catch (Exception ex)
            {
                Utilitarios_234TL.MensajeError("ErrorCargarDatos", ex);
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", DataPropertyName = "Dni" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Especialidad", DataPropertyName = "Especialidad" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Disponible", DataPropertyName = "Disponible" });
        }

        private void ConfigurarComboBoxes()
        {
            EspecialidadcomboBox.DataSource = Enum.GetValues(typeof(Especialidad_234TL));
            DisponiblecomboBox.DataSource = Enum.GetValues(typeof(OpcionBool_234TL));
        }

        private void LimpiarColoresError()
        {
            DNItextBox.BackColor = SystemColors.Window;
            NombretextBox.BackColor = SystemColors.Window;
            ApellidotextBox.BackColor = SystemColors.Window;
            EspecialidadcomboBox.BackColor = SystemColors.Window;
        }

        private void LimpiarCampos()
        {
            DNItextBox.Clear();
            NombretextBox.Clear();
            ApellidotextBox.Clear();
            TelefonotextBox.Clear();
            EspecialidadcomboBox.SelectedIndex = -1;
            DisponiblecomboBox.SelectedIndex = -1;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var tecnico = (Tecnico_234TL)dataGridView1.SelectedRows[0].DataBoundItem;
                DNItextBox.Text = tecnico.Dni;
                NombretextBox.Text = tecnico.Nombre;
                ApellidotextBox.Text = tecnico.Apellido;
                TelefonotextBox.Text = tecnico.Telefono;
                EspecialidadcomboBox.Text = tecnico.Especialidad;
                DisponiblecomboBox.SelectedItem = tecnico.Disponible ? OpcionBool_234TL.Si : OpcionBool_234TL.No;
            }
        }
    }
}
