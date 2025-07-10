namespace GUI_234TL
{
    partial class FormMessageBox_234TL
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
            Boton1 = new Button();
            Boton2 = new Button();
            Boton3 = new Button();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Mensaje = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Boton1
            // 
            Boton1.Location = new Point(143, 11);
            Boton1.Name = "Boton1";
            Boton1.Size = new Size(79, 24);
            Boton1.TabIndex = 0;
            Boton1.Text = "Boton1";
            Boton1.UseVisualStyleBackColor = true;
            Boton1.Click += Boton1_Click;
            // 
            // Boton2
            // 
            Boton2.Location = new Point(228, 12);
            Boton2.Name = "Boton2";
            Boton2.Size = new Size(75, 23);
            Boton2.TabIndex = 1;
            Boton2.Text = "Boton2";
            Boton2.UseVisualStyleBackColor = true;
            Boton2.Click += Boton2_Click;
            // 
            // Boton3
            // 
            Boton3.Location = new Point(309, 12);
            Boton3.Name = "Boton3";
            Boton3.Size = new Size(75, 23);
            Boton3.TabIndex = 2;
            Boton3.Text = "Boton3";
            Boton3.UseVisualStyleBackColor = true;
            Boton3.Click += Boton3_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(Boton1);
            panel1.Controls.Add(Boton2);
            panel1.Controls.Add(Boton3);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 114);
            panel1.Name = "panel1";
            panel1.Size = new Size(521, 47);
            panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(Mensaje, 0, 0);
            tableLayoutPanel1.Location = new Point(0, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(537, 102);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // Mensaje
            // 
            Mensaje.Dock = DockStyle.Fill;
            Mensaje.Location = new Point(3, 0);
            Mensaje.Name = "Mensaje";
            Mensaje.Size = new Size(531, 102);
            Mensaje.TabIndex = 3;
            Mensaje.Text = "Mensaje";
            Mensaje.TextAlign = ContentAlignment.MiddleCenter;
            Mensaje.Click += Mensaje_Click;
            // 
            // FormMessageBox_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 161);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            Name = "FormMessageBox_234TL";
            Text = "FormMessageBox_234TL";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button Boton1;
        private Button Boton2;
        private Button Boton3;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label Mensaje;
    }
}