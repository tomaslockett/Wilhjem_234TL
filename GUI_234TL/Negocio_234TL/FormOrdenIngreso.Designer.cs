namespace GUI_234TL
{
    partial class FormOrdenIngreso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewEquipos = new DataGridView();
            dataGridViewClientes = new DataGridView();
            ClientesLabel = new Label();
            EquiposLabel = new Label();
            Equipobutton = new Button();
            Clientebutton = new Button();
            Ordenbutton = new Button();
            dataGridViewTecnicos = new DataGridView();
            TecnicosLabel = new Label();
            ClienteSeleccionadoLabel = new Label();
            EquipoSeleccionadoLabel = new Label();
            TecnicoSeleccionadoLabel = new Label();
            EliminarOrdenButton = new Button();
            dataGridView1 = new DataGridView();
            OrdenesLabel = new Label();
            OrdenSeleccionadaLabel = new Label();
            EstadoLabel = new Label();
            EstadoComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEquipos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTecnicos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewEquipos
            // 
            dataGridViewEquipos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEquipos.Location = new Point(37, 28);
            dataGridViewEquipos.Name = "dataGridViewEquipos";
            dataGridViewEquipos.Size = new Size(592, 231);
            dataGridViewEquipos.TabIndex = 0;
            dataGridViewEquipos.SelectionChanged += dataGridViewEquipos_SelectionChanged;
            // 
            // dataGridViewClientes
            // 
            dataGridViewClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientes.Location = new Point(37, 322);
            dataGridViewClientes.Name = "dataGridViewClientes";
            dataGridViewClientes.Size = new Size(592, 231);
            dataGridViewClientes.TabIndex = 1;
            dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged;
            // 
            // ClientesLabel
            // 
            ClientesLabel.AutoSize = true;
            ClientesLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClientesLabel.Location = new Point(37, 298);
            ClientesLabel.Name = "ClientesLabel";
            ClientesLabel.Size = new Size(71, 21);
            ClientesLabel.TabIndex = 2;
            ClientesLabel.Text = "Clientes";
            // 
            // EquiposLabel
            // 
            EquiposLabel.AutoSize = true;
            EquiposLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EquiposLabel.Location = new Point(37, 4);
            EquiposLabel.Name = "EquiposLabel";
            EquiposLabel.Size = new Size(71, 21);
            EquiposLabel.TabIndex = 3;
            EquiposLabel.Text = "Equipos";
            // 
            // Equipobutton
            // 
            Equipobutton.Location = new Point(464, 265);
            Equipobutton.Name = "Equipobutton";
            Equipobutton.Size = new Size(165, 31);
            Equipobutton.TabIndex = 4;
            Equipobutton.Text = "Ingreso Equipo";
            Equipobutton.UseVisualStyleBackColor = true;
            Equipobutton.Click += Equipobutton_Click;
            // 
            // Clientebutton
            // 
            Clientebutton.Location = new Point(464, 559);
            Clientebutton.Name = "Clientebutton";
            Clientebutton.Size = new Size(165, 31);
            Clientebutton.TabIndex = 5;
            Clientebutton.Text = "Registrar Cliente";
            Clientebutton.UseVisualStyleBackColor = true;
            Clientebutton.Click += Clientebutton_Click;
            // 
            // Ordenbutton
            // 
            Ordenbutton.Location = new Point(635, 322);
            Ordenbutton.Name = "Ordenbutton";
            Ordenbutton.Size = new Size(131, 48);
            Ordenbutton.TabIndex = 6;
            Ordenbutton.Text = "Crear Orden";
            Ordenbutton.UseVisualStyleBackColor = true;
            Ordenbutton.Click += Ordenbutton_Click;
            // 
            // dataGridViewTecnicos
            // 
            dataGridViewTecnicos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTecnicos.Location = new Point(849, 28);
            dataGridViewTecnicos.Name = "dataGridViewTecnicos";
            dataGridViewTecnicos.Size = new Size(592, 231);
            dataGridViewTecnicos.TabIndex = 7;
            dataGridViewTecnicos.SelectionChanged += dataGridViewTecnicos_SelectionChanged;
            // 
            // TecnicosLabel
            // 
            TecnicosLabel.AutoSize = true;
            TecnicosLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TecnicosLabel.Location = new Point(849, 4);
            TecnicosLabel.Name = "TecnicosLabel";
            TecnicosLabel.Size = new Size(75, 21);
            TecnicosLabel.TabIndex = 8;
            TecnicosLabel.Text = "Tecnicos";
            // 
            // ClienteSeleccionadoLabel
            // 
            ClienteSeleccionadoLabel.AutoSize = true;
            ClienteSeleccionadoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClienteSeleccionadoLabel.Location = new Point(37, 556);
            ClienteSeleccionadoLabel.Name = "ClienteSeleccionadoLabel";
            ClienteSeleccionadoLabel.Size = new Size(166, 21);
            ClienteSeleccionadoLabel.TabIndex = 9;
            ClienteSeleccionadoLabel.Text = "ClienteSeleccionado";
            // 
            // EquipoSeleccionadoLabel
            // 
            EquipoSeleccionadoLabel.AutoSize = true;
            EquipoSeleccionadoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EquipoSeleccionadoLabel.Location = new Point(37, 262);
            EquipoSeleccionadoLabel.Name = "EquipoSeleccionadoLabel";
            EquipoSeleccionadoLabel.Size = new Size(166, 21);
            EquipoSeleccionadoLabel.TabIndex = 10;
            EquipoSeleccionadoLabel.Text = "EquipoSeleccionado";
            // 
            // TecnicoSeleccionadoLabel
            // 
            TecnicoSeleccionadoLabel.AutoSize = true;
            TecnicoSeleccionadoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TecnicoSeleccionadoLabel.Location = new Point(849, 262);
            TecnicoSeleccionadoLabel.Name = "TecnicoSeleccionadoLabel";
            TecnicoSeleccionadoLabel.Size = new Size(170, 21);
            TecnicoSeleccionadoLabel.TabIndex = 11;
            TecnicoSeleccionadoLabel.Text = "TecnicoSeleccionado";
            // 
            // EliminarOrdenButton
            // 
            EliminarOrdenButton.Location = new Point(635, 445);
            EliminarOrdenButton.Name = "EliminarOrdenButton";
            EliminarOrdenButton.Size = new Size(131, 48);
            EliminarOrdenButton.TabIndex = 12;
            EliminarOrdenButton.Text = "Eliminar Orden";
            EliminarOrdenButton.UseVisualStyleBackColor = true;
            EliminarOrdenButton.Click += EliminarOrdenButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(849, 322);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(592, 231);
            dataGridView1.TabIndex = 13;
            // OrdenesLabel
            // 
            OrdenesLabel.AutoSize = true;
            OrdenesLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OrdenesLabel.Location = new Point(849, 298);
            OrdenesLabel.Name = "OrdenesLabel";
            OrdenesLabel.Size = new Size(73, 21);
            OrdenesLabel.TabIndex = 14;
            OrdenesLabel.Text = "Ordenes";
            // 
            // OrdenSeleccionadaLabel
            // 
            OrdenSeleccionadaLabel.AutoSize = true;
            OrdenSeleccionadaLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OrdenSeleccionadaLabel.Location = new Point(849, 556);
            OrdenSeleccionadaLabel.Name = "OrdenSeleccionadaLabel";
            OrdenSeleccionadaLabel.Size = new Size(159, 21);
            OrdenSeleccionadaLabel.TabIndex = 15;
            OrdenSeleccionadaLabel.Text = "OrdenSeleccionado";
            // 
            // EstadoLabel
            // 
            EstadoLabel.AutoSize = true;
            EstadoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EstadoLabel.Location = new Point(635, 373);
            EstadoLabel.Name = "EstadoLabel";
            EstadoLabel.Size = new Size(61, 21);
            EstadoLabel.TabIndex = 16;
            EstadoLabel.Text = "Estado";
            // 
            // EstadoComboBox
            // 
            EstadoComboBox.FormattingEnabled = true;
            EstadoComboBox.Location = new Point(635, 397);
            EstadoComboBox.Name = "EstadoComboBox";
            EstadoComboBox.Size = new Size(121, 23);
            EstadoComboBox.TabIndex = 17;
            // 
            // FormOrdenIngreso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1453, 599);
            Controls.Add(EstadoComboBox);
            Controls.Add(EstadoLabel);
            Controls.Add(OrdenSeleccionadaLabel);
            Controls.Add(OrdenesLabel);
            Controls.Add(dataGridView1);
            Controls.Add(EliminarOrdenButton);
            Controls.Add(TecnicoSeleccionadoLabel);
            Controls.Add(EquipoSeleccionadoLabel);
            Controls.Add(ClienteSeleccionadoLabel);
            Controls.Add(TecnicosLabel);
            Controls.Add(dataGridViewTecnicos);
            Controls.Add(Ordenbutton);
            Controls.Add(Clientebutton);
            Controls.Add(Equipobutton);
            Controls.Add(EquiposLabel);
            Controls.Add(ClientesLabel);
            Controls.Add(dataGridViewClientes);
            Controls.Add(dataGridViewEquipos);
            Name = "FormOrdenIngreso";
            Text = "FormOrdenIngreso";
            FormClosed += FormOrdenIngreso_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEquipos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTecnicos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewEquipos;
        private DataGridView dataGridViewClientes;
        private Label ClientesLabel;
        private Label EquiposLabel;
        private Button Equipobutton;
        private Button Clientebutton;
        private Button Ordenbutton;
        private DataGridView dataGridViewTecnicos;
        private Label TecnicosLabel;
        private Label ClienteSeleccionadoLabel;
        private Label EquipoSeleccionadoLabel;
        private Label TecnicoSeleccionadoLabel;
        private Button EliminarOrdenButton;
        private DataGridView dataGridView1;
        private Label OrdenesLabel;
        private Label OrdenSeleccionadaLabel;
        private Label EstadoLabel;
        private ComboBox EstadoComboBox;
    }
}