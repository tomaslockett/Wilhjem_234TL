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
            Clientes = new Label();
            Dni = new Label();
            Nombre = new Label();
            label2 = new Label();
            NumeroTelefonico = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            RegistrarClientebutton = new Button();
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
            // Clientes
            // 
            Clientes.AutoSize = true;
            Clientes.Location = new Point(12, 26);
            Clientes.Name = "Clientes";
            Clientes.Size = new Size(49, 15);
            Clientes.TabIndex = 1;
            Clientes.Text = "Clientes";
            // 
            // Dni
            // 
            Dni.AutoSize = true;
            Dni.Location = new Point(30, 338);
            Dni.Name = "Dni";
            Dni.Size = new Size(25, 15);
            Dni.TabIndex = 2;
            Dni.Text = "Dni";
            Dni.Click += Dni_Click;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.Location = new Point(30, 367);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(51, 15);
            Nombre.TabIndex = 3;
            Nombre.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 401);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 4;
            label2.Text = "Apellido";
            // 
            // NumeroTelefonico
            // 
            NumeroTelefonico.AutoSize = true;
            NumeroTelefonico.Location = new Point(30, 425);
            NumeroTelefonico.Name = "NumeroTelefonico";
            NumeroTelefonico.Size = new Size(109, 15);
            NumeroTelefonico.TabIndex = 5;
            NumeroTelefonico.Text = "Numero Telefonico";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(145, 335);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(143, 23);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(145, 364);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(143, 23);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(144, 393);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(143, 23);
            textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(145, 422);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(143, 23);
            textBox4.TabIndex = 9;
            // 
            // RegistrarClientebutton
            // 
            RegistrarClientebutton.Location = new Point(376, 336);
            RegistrarClientebutton.Name = "RegistrarClientebutton";
            RegistrarClientebutton.Size = new Size(184, 65);
            RegistrarClientebutton.TabIndex = 10;
            RegistrarClientebutton.Text = "Registrar Cliente";
            RegistrarClientebutton.UseVisualStyleBackColor = true;
            // 
            // FormRegistrarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(951, 524);
            Controls.Add(RegistrarClientebutton);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(NumeroTelefonico);
            Controls.Add(label2);
            Controls.Add(Nombre);
            Controls.Add(Dni);
            Controls.Add(Clientes);
            Controls.Add(dataGridView1);
            Name = "FormRegistrarCliente";
            Text = "FormRegistrarCliente";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label Clientes;
        private Label Dni;
        private Label Nombre;
        private Label label2;
        private Label NumeroTelefonico;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button RegistrarClientebutton;
    }
}