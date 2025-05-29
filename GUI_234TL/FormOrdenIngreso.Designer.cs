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
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            Clientes = new Label();
            Equipos = new Label();
            Equipobutton = new Button();
            Clientebutton = new Button();
            Ordenbutton = new Button();
            dataGridView3 = new DataGridView();
            Tecnicos = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(37, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(592, 231);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(37, 322);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(592, 231);
            dataGridView2.TabIndex = 1;
            // 
            // Clientes
            // 
            Clientes.AutoSize = true;
            Clientes.Location = new Point(37, 304);
            Clientes.Name = "Clientes";
            Clientes.Size = new Size(49, 15);
            Clientes.TabIndex = 2;
            Clientes.Text = "Clientes";
            // 
            // Equipos
            // 
            Equipos.AutoSize = true;
            Equipos.Location = new Point(37, 10);
            Equipos.Name = "Equipos";
            Equipos.Size = new Size(49, 15);
            Equipos.TabIndex = 3;
            Equipos.Text = "Equipos";
            // 
            // Equipobutton
            // 
            Equipobutton.Location = new Point(635, 228);
            Equipobutton.Name = "Equipobutton";
            Equipobutton.Size = new Size(165, 31);
            Equipobutton.TabIndex = 4;
            Equipobutton.Text = "Ingreso Equipo";
            Equipobutton.UseVisualStyleBackColor = true;
            // 
            // Clientebutton
            // 
            Clientebutton.Location = new Point(635, 522);
            Clientebutton.Name = "Clientebutton";
            Clientebutton.Size = new Size(165, 31);
            Clientebutton.TabIndex = 5;
            Clientebutton.Text = "Registrar Cliente";
            Clientebutton.UseVisualStyleBackColor = true;
            // 
            // Ordenbutton
            // 
            Ordenbutton.Location = new Point(1086, 394);
            Ordenbutton.Name = "Ordenbutton";
            Ordenbutton.Size = new Size(131, 81);
            Ordenbutton.TabIndex = 6;
            Ordenbutton.Text = "Crear Orden";
            Ordenbutton.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(849, 28);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(592, 231);
            dataGridView3.TabIndex = 7;
            // 
            // Tecnicos
            // 
            Tecnicos.AutoSize = true;
            Tecnicos.Location = new Point(849, 10);
            Tecnicos.Name = "Tecnicos";
            Tecnicos.Size = new Size(53, 15);
            Tecnicos.TabIndex = 8;
            Tecnicos.Text = "Tecnicos";
            // 
            // FormOrdenIngreso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1453, 591);
            Controls.Add(Tecnicos);
            Controls.Add(dataGridView3);
            Controls.Add(Ordenbutton);
            Controls.Add(Clientebutton);
            Controls.Add(Equipobutton);
            Controls.Add(Equipos);
            Controls.Add(Clientes);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Name = "FormOrdenIngreso";
            Text = "FormOrdenIngreso";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label Clientes;
        private Label Equipos;
        private Button Equipobutton;
        private Button Clientebutton;
        private Button Ordenbutton;
        private DataGridView dataGridView3;
        private Label Tecnicos;
    }
}