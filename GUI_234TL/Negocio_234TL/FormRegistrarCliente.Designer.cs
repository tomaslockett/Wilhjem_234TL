namespace GUI_234TL
{
    partial class FormRegistrarCliente
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
            ClientesLabel = new Label();
            DNILabel = new Label();
            NombreLabel = new Label();
            ApellidoLabel = new Label();
            NumeroTelefonicoLabel = new Label();
            DNITextBox = new TextBox();
            NombreTextBox = new TextBox();
            ApellidoTextBox = new TextBox();
            TelefonoTextBox = new TextBox();
            RegistrarClientebutton = new Button();
            EliminarClienteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 44);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(725, 273);
            dataGridView1.TabIndex = 0;
            // 
            // ClientesLabel
            // 
            ClientesLabel.AutoSize = true;
            ClientesLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ClientesLabel.Location = new Point(12, 9);
            ClientesLabel.Name = "ClientesLabel";
            ClientesLabel.Size = new Size(90, 30);
            ClientesLabel.TabIndex = 1;
            ClientesLabel.Text = "Clientes";
            // 
            // DNILabel
            // 
            DNILabel.AutoSize = true;
            DNILabel.Location = new Point(30, 338);
            DNILabel.Name = "DNILabel";
            DNILabel.Size = new Size(27, 15);
            DNILabel.TabIndex = 2;
            DNILabel.Text = "DNI";
            // 
            // NombreLabel
            // 
            NombreLabel.AutoSize = true;
            NombreLabel.Location = new Point(30, 367);
            NombreLabel.Name = "NombreLabel";
            NombreLabel.Size = new Size(51, 15);
            NombreLabel.TabIndex = 3;
            NombreLabel.Text = "Nombre";
            // 
            // ApellidoLabel
            // 
            ApellidoLabel.AutoSize = true;
            ApellidoLabel.Location = new Point(30, 401);
            ApellidoLabel.Name = "ApellidoLabel";
            ApellidoLabel.Size = new Size(51, 15);
            ApellidoLabel.TabIndex = 4;
            ApellidoLabel.Text = "Apellido";
            // 
            // NumeroTelefonicoLabel
            // 
            NumeroTelefonicoLabel.AutoSize = true;
            NumeroTelefonicoLabel.Location = new Point(30, 425);
            NumeroTelefonicoLabel.Name = "NumeroTelefonicoLabel";
            NumeroTelefonicoLabel.Size = new Size(109, 15);
            NumeroTelefonicoLabel.TabIndex = 5;
            NumeroTelefonicoLabel.Text = "Numero Telefonico";
            // 
            // DNITextBox
            // 
            DNITextBox.Location = new Point(145, 335);
            DNITextBox.Name = "DNITextBox";
            DNITextBox.Size = new Size(143, 23);
            DNITextBox.TabIndex = 6;
            // 
            // NombreTextBox
            // 
            NombreTextBox.Location = new Point(145, 364);
            NombreTextBox.Name = "NombreTextBox";
            NombreTextBox.Size = new Size(143, 23);
            NombreTextBox.TabIndex = 7;
            // 
            // ApellidoTextBox
            // 
            ApellidoTextBox.Location = new Point(144, 393);
            ApellidoTextBox.Name = "ApellidoTextBox";
            ApellidoTextBox.Size = new Size(143, 23);
            ApellidoTextBox.TabIndex = 8;
            // 
            // TelefonoTextBox
            // 
            TelefonoTextBox.Location = new Point(145, 422);
            TelefonoTextBox.Name = "TelefonoTextBox";
            TelefonoTextBox.Size = new Size(143, 23);
            TelefonoTextBox.TabIndex = 9;
            // 
            // RegistrarClientebutton
            // 
            RegistrarClientebutton.Location = new Point(293, 335);
            RegistrarClientebutton.Name = "RegistrarClientebutton";
            RegistrarClientebutton.Size = new Size(184, 65);
            RegistrarClientebutton.TabIndex = 10;
            RegistrarClientebutton.Text = "RegistrarCliente";
            RegistrarClientebutton.UseVisualStyleBackColor = true;
            RegistrarClientebutton.Click += RegistrarClientebutton_Click;
            // 
            // EliminarClienteButton
            // 
            EliminarClienteButton.Location = new Point(483, 335);
            EliminarClienteButton.Name = "EliminarClienteButton";
            EliminarClienteButton.Size = new Size(184, 65);
            EliminarClienteButton.TabIndex = 11;
            EliminarClienteButton.Text = "EliminarCliente";
            EliminarClienteButton.UseVisualStyleBackColor = true;
            EliminarClienteButton.Click += EliminarClienteButton_Click;
            // 
            // FormRegistrarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(754, 469);
            Controls.Add(EliminarClienteButton);
            Controls.Add(RegistrarClientebutton);
            Controls.Add(TelefonoTextBox);
            Controls.Add(ApellidoTextBox);
            Controls.Add(NombreTextBox);
            Controls.Add(DNITextBox);
            Controls.Add(NumeroTelefonicoLabel);
            Controls.Add(ApellidoLabel);
            Controls.Add(NombreLabel);
            Controls.Add(DNILabel);
            Controls.Add(ClientesLabel);
            Controls.Add(dataGridView1);
            Name = "FormRegistrarCliente";
            Text = "FormRegistrarCliente";
            FormClosed += FormRegistrarCliente_FormClosed;
            Load += FormRegistrarCliente_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label ClientesLabel;
        private Label DNILabel;
        private Label NombreLabel;
        private Label ApellidoLabel;
        private Label NumeroTelefonicoLabel;
        private TextBox DNITextBox;
        private TextBox NombreTextBox;
        private TextBox ApellidoTextBox;
        private TextBox TelefonoTextBox;
        private Button RegistrarClientebutton;
        private Button EliminarClienteButton;
    }
}