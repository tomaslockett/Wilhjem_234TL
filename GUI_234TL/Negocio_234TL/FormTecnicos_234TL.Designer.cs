namespace GUI_234TL.Negocio_234TL
{
    partial class FormTecnicos_234TL
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
            TecnicosLabel = new Label();
            AgregarButton = new Button();
            DisponiblecomboBox = new ComboBox();
            ModificarButton = new Button();
            EliminarButton = new Button();
            DNILabel = new Label();
            NombreLabel = new Label();
            ApellidoLabel = new Label();
            TelefonoLabel = new Label();
            EspecialidadcomboBox = new ComboBox();
            TelefonotextBox = new TextBox();
            ApellidotextBox = new TextBox();
            NombretextBox = new TextBox();
            DNItextBox = new TextBox();
            DisponibleLabel = new Label();
            EspecialidadLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(768, 245);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // TecnicosLabel
            // 
            TecnicosLabel.AutoSize = true;
            TecnicosLabel.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            TecnicosLabel.Location = new Point(12, 9);
            TecnicosLabel.Name = "TecnicosLabel";
            TecnicosLabel.Size = new Size(127, 40);
            TecnicosLabel.TabIndex = 1;
            TecnicosLabel.Text = "Tecnicos\r\n";
            // 
            // AgregarButton
            // 
            AgregarButton.Location = new Point(12, 303);
            AgregarButton.Name = "AgregarButton";
            AgregarButton.Size = new Size(117, 35);
            AgregarButton.TabIndex = 2;
            AgregarButton.Text = "button1";
            AgregarButton.UseVisualStyleBackColor = true;
            AgregarButton.Click += AgregarButton_Click;
            // 
            // DisponiblecomboBox
            // 
            DisponiblecomboBox.FormattingEnabled = true;
            DisponiblecomboBox.Location = new Point(405, 310);
            DisponiblecomboBox.Name = "DisponiblecomboBox";
            DisponiblecomboBox.Size = new Size(121, 23);
            DisponiblecomboBox.TabIndex = 3;
            // 
            // ModificarButton
            // 
            ModificarButton.Location = new Point(12, 344);
            ModificarButton.Name = "ModificarButton";
            ModificarButton.Size = new Size(117, 35);
            ModificarButton.TabIndex = 4;
            ModificarButton.Text = "button2";
            ModificarButton.UseVisualStyleBackColor = true;
            ModificarButton.Click += ModificarButton_Click;
            // 
            // EliminarButton
            // 
            EliminarButton.Location = new Point(12, 385);
            EliminarButton.Name = "EliminarButton";
            EliminarButton.Size = new Size(117, 35);
            EliminarButton.TabIndex = 5;
            EliminarButton.Text = "button3";
            EliminarButton.UseVisualStyleBackColor = true;
            EliminarButton.Click += EliminarButton_Click;
            // 
            // DNILabel
            // 
            DNILabel.AutoSize = true;
            DNILabel.Location = new Point(142, 310);
            DNILabel.Name = "DNILabel";
            DNILabel.Size = new Size(27, 15);
            DNILabel.TabIndex = 6;
            DNILabel.Text = "DNI";
            // 
            // NombreLabel
            // 
            NombreLabel.AutoSize = true;
            NombreLabel.Location = new Point(142, 349);
            NombreLabel.Name = "NombreLabel";
            NombreLabel.Size = new Size(51, 15);
            NombreLabel.TabIndex = 7;
            NombreLabel.Text = "Nombre";
            // 
            // ApellidoLabel
            // 
            ApellidoLabel.AutoSize = true;
            ApellidoLabel.Location = new Point(142, 388);
            ApellidoLabel.Name = "ApellidoLabel";
            ApellidoLabel.Size = new Size(51, 15);
            ApellidoLabel.TabIndex = 8;
            ApellidoLabel.Text = "Apellido";
            // 
            // TelefonoLabel
            // 
            TelefonoLabel.AutoSize = true;
            TelefonoLabel.Location = new Point(142, 423);
            TelefonoLabel.Name = "TelefonoLabel";
            TelefonoLabel.Size = new Size(53, 15);
            TelefonoLabel.TabIndex = 9;
            TelefonoLabel.Text = "Telefono";
            // 
            // EspecialidadcomboBox
            // 
            EspecialidadcomboBox.FormattingEnabled = true;
            EspecialidadcomboBox.Location = new Point(405, 369);
            EspecialidadcomboBox.Name = "EspecialidadcomboBox";
            EspecialidadcomboBox.Size = new Size(121, 23);
            EspecialidadcomboBox.TabIndex = 10;
            // 
            // TelefonotextBox
            // 
            TelefonotextBox.Location = new Point(201, 420);
            TelefonotextBox.Name = "TelefonotextBox";
            TelefonotextBox.Size = new Size(100, 23);
            TelefonotextBox.TabIndex = 11;
            // 
            // ApellidotextBox
            // 
            ApellidotextBox.Location = new Point(201, 385);
            ApellidotextBox.Name = "ApellidotextBox";
            ApellidotextBox.Size = new Size(100, 23);
            ApellidotextBox.TabIndex = 12;
            // 
            // NombretextBox
            // 
            NombretextBox.Location = new Point(201, 346);
            NombretextBox.Name = "NombretextBox";
            NombretextBox.Size = new Size(100, 23);
            NombretextBox.TabIndex = 13;
            // 
            // DNItextBox
            // 
            DNItextBox.Location = new Point(201, 307);
            DNItextBox.Name = "DNItextBox";
            DNItextBox.Size = new Size(100, 23);
            DNItextBox.TabIndex = 14;
            // 
            // DisponibleLabel
            // 
            DisponibleLabel.AutoSize = true;
            DisponibleLabel.Location = new Point(336, 315);
            DisponibleLabel.Name = "DisponibleLabel";
            DisponibleLabel.Size = new Size(63, 15);
            DisponibleLabel.TabIndex = 15;
            DisponibleLabel.Text = "Disponible";
            // 
            // EspecialidadLabel
            // 
            EspecialidadLabel.AutoSize = true;
            EspecialidadLabel.Location = new Point(327, 372);
            EspecialidadLabel.Name = "EspecialidadLabel";
            EspecialidadLabel.Size = new Size(72, 15);
            EspecialidadLabel.TabIndex = 16;
            EspecialidadLabel.Text = "Especialidad";
            // 
            // FormTecnicos_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(791, 461);
            Controls.Add(EspecialidadLabel);
            Controls.Add(DisponibleLabel);
            Controls.Add(DNItextBox);
            Controls.Add(NombretextBox);
            Controls.Add(ApellidotextBox);
            Controls.Add(TelefonotextBox);
            Controls.Add(EspecialidadcomboBox);
            Controls.Add(TelefonoLabel);
            Controls.Add(ApellidoLabel);
            Controls.Add(NombreLabel);
            Controls.Add(DNILabel);
            Controls.Add(EliminarButton);
            Controls.Add(ModificarButton);
            Controls.Add(DisponiblecomboBox);
            Controls.Add(AgregarButton);
            Controls.Add(TecnicosLabel);
            Controls.Add(dataGridView1);
            Name = "FormTecnicos_234TL";
            Text = "FormTecnicos_234TL";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label TecnicosLabel;
        private Button AgregarButton;
        private ComboBox DisponiblecomboBox;
        private Button ModificarButton;
        private Button EliminarButton;
        private Label DNILabel;
        private Label NombreLabel;
        private Label ApellidoLabel;
        private Label TelefonoLabel;
        private ComboBox EspecialidadcomboBox;
        private TextBox TelefonotextBox;
        private TextBox ApellidotextBox;
        private TextBox NombretextBox;
        private TextBox DNItextBox;
        private Label DisponibleLabel;
        private Label EspecialidadLabel;
    }
}