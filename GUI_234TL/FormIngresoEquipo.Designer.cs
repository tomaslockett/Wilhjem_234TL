namespace GUI_234TL
{
    partial class FormIngresoEquipo
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
            dataGridView1 = new DataGridView();
            IngresarEquipoButton = new Button();
            MarcaLabel = new Label();
            ModeloTextBox = new TextBox();
            ModeloLabel = new Label();
            NumeroSerieTipoTextBox = new TextBox();
            NumeroSerieLabel = new Label();
            EstadoVisualLabel = new Label();
            FallaTextBox = new TextBox();
            FallaLabel = new Label();
            DañoVisualLabel = new Label();
            DesarmadoLabel = new Label();
            EliminarEquipo = new Button();
            MarcaComboBox = new ComboBox();
            EstadoComboBox = new ComboBox();
            DañoVisibleComboBox = new ComboBox();
            DesarmadoComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(9, 18);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(807, 258);
            dataGridView1.TabIndex = 0;
            // 
            // IngresarEquipoButton
            // 
            IngresarEquipoButton.Location = new Point(352, 294);
            IngresarEquipoButton.Name = "IngresarEquipoButton";
            IngresarEquipoButton.Size = new Size(184, 101);
            IngresarEquipoButton.TabIndex = 1;
            IngresarEquipoButton.Text = "IngresarEquipo";
            IngresarEquipoButton.UseVisualStyleBackColor = true;
            IngresarEquipoButton.Click += IngresarEquipoButton_Click;
            // 
            // MarcaLabel
            // 
            MarcaLabel.AutoSize = true;
            MarcaLabel.Location = new Point(69, 338);
            MarcaLabel.Name = "MarcaLabel";
            MarcaLabel.Size = new Size(40, 15);
            MarcaLabel.TabIndex = 4;
            MarcaLabel.Text = "Marca";
            // 
            // ModeloTextBox
            // 
            ModeloTextBox.Location = new Point(115, 372);
            ModeloTextBox.Name = "ModeloTextBox";
            ModeloTextBox.Size = new Size(142, 23);
            ModeloTextBox.TabIndex = 7;
            // 
            // ModeloLabel
            // 
            ModeloLabel.AutoSize = true;
            ModeloLabel.Location = new Point(61, 375);
            ModeloLabel.Name = "ModeloLabel";
            ModeloLabel.Size = new Size(48, 15);
            ModeloLabel.TabIndex = 6;
            ModeloLabel.Text = "Modelo";
            // 
            // NumeroSerieTipoTextBox
            // 
            NumeroSerieTipoTextBox.Location = new Point(115, 305);
            NumeroSerieTipoTextBox.Name = "NumeroSerieTipoTextBox";
            NumeroSerieTipoTextBox.Size = new Size(142, 23);
            NumeroSerieTipoTextBox.TabIndex = 9;
            // 
            // NumeroSerieLabel
            // 
            NumeroSerieLabel.AutoSize = true;
            NumeroSerieLabel.Location = new Point(18, 308);
            NumeroSerieLabel.Name = "NumeroSerieLabel";
            NumeroSerieLabel.Size = new Size(94, 15);
            NumeroSerieLabel.TabIndex = 8;
            NumeroSerieLabel.Text = "Numero de serie";
            // 
            // EstadoVisualLabel
            // 
            EstadoVisualLabel.AutoSize = true;
            EstadoVisualLabel.Location = new Point(33, 404);
            EstadoVisualLabel.Name = "EstadoVisualLabel";
            EstadoVisualLabel.Size = new Size(76, 15);
            EstadoVisualLabel.TabIndex = 10;
            EstadoVisualLabel.Text = "Estado Visual";
            // 
            // FallaTextBox
            // 
            FallaTextBox.Location = new Point(115, 430);
            FallaTextBox.Name = "FallaTextBox";
            FallaTextBox.Size = new Size(301, 23);
            FallaTextBox.TabIndex = 13;
            // 
            // FallaLabel
            // 
            FallaLabel.AutoSize = true;
            FallaLabel.Location = new Point(78, 433);
            FallaLabel.Name = "FallaLabel";
            FallaLabel.Size = new Size(31, 15);
            FallaLabel.TabIndex = 12;
            FallaLabel.Text = "Falla";
            // 
            // DañoVisualLabel
            // 
            DañoVisualLabel.AutoSize = true;
            DañoVisualLabel.Location = new Point(32, 466);
            DañoVisualLabel.Name = "DañoVisualLabel";
            DañoVisualLabel.Size = new Size(77, 15);
            DañoVisualLabel.TabIndex = 14;
            DañoVisualLabel.Text = "Daño Visibles";
            // 
            // DesarmadoLabel
            // 
            DesarmadoLabel.AutoSize = true;
            DesarmadoLabel.Location = new Point(41, 495);
            DesarmadoLabel.Name = "DesarmadoLabel";
            DesarmadoLabel.Size = new Size(67, 15);
            DesarmadoLabel.TabIndex = 16;
            DesarmadoLabel.Text = "Desarmado";
            // 
            // EliminarEquipo
            // 
            EliminarEquipo.Location = new Point(542, 295);
            EliminarEquipo.Name = "EliminarEquipo";
            EliminarEquipo.Size = new Size(184, 101);
            EliminarEquipo.TabIndex = 18;
            EliminarEquipo.Text = "EliminarEquipo";
            EliminarEquipo.UseVisualStyleBackColor = true;
            EliminarEquipo.Click += EliminarEquipo_Click;
            // 
            // MarcaComboBox
            // 
            MarcaComboBox.FormattingEnabled = true;
            MarcaComboBox.Location = new Point(115, 334);
            MarcaComboBox.Name = "MarcaComboBox";
            MarcaComboBox.Size = new Size(121, 23);
            MarcaComboBox.TabIndex = 19;
            // 
            // EstadoComboBox
            // 
            EstadoComboBox.FormattingEnabled = true;
            EstadoComboBox.Location = new Point(115, 401);
            EstadoComboBox.Name = "EstadoComboBox";
            EstadoComboBox.Size = new Size(121, 23);
            EstadoComboBox.TabIndex = 20;
            // 
            // DañoVisibleComboBox
            // 
            DañoVisibleComboBox.FormattingEnabled = true;
            DañoVisibleComboBox.Location = new Point(115, 463);
            DañoVisibleComboBox.Name = "DañoVisibleComboBox";
            DañoVisibleComboBox.Size = new Size(121, 23);
            DañoVisibleComboBox.TabIndex = 21;
            // 
            // DesarmadoComboBox
            // 
            DesarmadoComboBox.FormattingEnabled = true;
            DesarmadoComboBox.Location = new Point(115, 492);
            DesarmadoComboBox.Name = "DesarmadoComboBox";
            DesarmadoComboBox.Size = new Size(121, 23);
            DesarmadoComboBox.TabIndex = 22;
            // 
            // FormIngresoEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(835, 547);
            Controls.Add(DesarmadoComboBox);
            Controls.Add(DañoVisibleComboBox);
            Controls.Add(EstadoComboBox);
            Controls.Add(MarcaComboBox);
            Controls.Add(EliminarEquipo);
            Controls.Add(DesarmadoLabel);
            Controls.Add(DañoVisualLabel);
            Controls.Add(FallaTextBox);
            Controls.Add(FallaLabel);
            Controls.Add(EstadoVisualLabel);
            Controls.Add(NumeroSerieTipoTextBox);
            Controls.Add(NumeroSerieLabel);
            Controls.Add(ModeloTextBox);
            Controls.Add(ModeloLabel);
            Controls.Add(MarcaLabel);
            Controls.Add(IngresarEquipoButton);
            Controls.Add(dataGridView1);
            Name = "FormIngresoEquipo";
            Text = "FormIngresoEquipo";
            FormClosed += FormIngresoEquipo_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button IngresarEquipoButton;
        private Label MarcaLabel;
        private TextBox ModeloTextBox;
        private Label ModeloLabel;
        private TextBox NumeroSerieTipoTextBox;
        private Label NumeroSerieLabel;
        private Label EstadoVisualLabel;
        private TextBox FallaTextBox;
        private Label FallaLabel;
        private Label DañoVisualLabel;
        private Label DesarmadoLabel;
        private Button EliminarEquipo;
        private ComboBox MarcaComboBox;
        private ComboBox EstadoComboBox;
        private ComboBox DañoVisibleComboBox;
        private ComboBox DesarmadoComboBox;
    }
}